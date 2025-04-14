using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerInventoryScript : MonoBehaviour
{
    public TMP_Text alumiteCounterText;
    public TMP_Text oreCounterText;
    public TMP_Text flowerCounterText;
    public TMP_Text sealCounterText;
    public TMP_Text WarningText;
    public GameObject keyBulb;

    public float detectionRadius = 0.2f;
    public float drainingInterval = 1f; 

    private int alumiteCount = 0;
    private int oreCount = 0;
    private int flowerCount = 0;
    private int sealCount = 0;
    private bool keyObtained = false;
    private float drainingTimer = 0f;

    private void Start()
    {
        WarningText.text = "";
        UpdateCounters();
        keyBulb.SetActive(false);

    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        bool hasDrainingCollider = colliders.Any(collider => collider.CompareTag("Draining"));
        bool hasDraining2Collider = colliders.Any(collider => collider.CompareTag("Draining2"));


        if (hasDrainingCollider)
        {
            StartCoroutine(AlumiteDrain());
        }
        if (hasDraining2Collider)
        {
            StartCoroutine(BlossomDrain());
        }

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Red"))
            {
                alumiteCount += 1;
                Destroy(collider.gameObject);
            }
            else if (collider.CompareTag("Blue"))
            {
                alumiteCount += 5;
                Destroy(collider.gameObject);
            }
            else if (collider.CompareTag("Yellow"))
            {
                alumiteCount += 20;
                Destroy(collider.gameObject);
            }
            else if (collider.CompareTag("Green"))
            {
                alumiteCount += 50;
                Destroy(collider.gameObject);
            }
            else if (collider.CompareTag("Ore"))
            {
                oreCount += 1;
                Destroy(collider.gameObject);
            }
            else if (collider.CompareTag("Flower"))
            {
                flowerCount += 1;
                Destroy(collider.gameObject);
            }
            else if (collider.CompareTag("Key"))
            {
                keyObtained = true;
                keyBulb.SetActive(true);
            }
            else if (collider.CompareTag("Seal"))
            {
                sealCount += 1;
                Destroy(collider.gameObject);
            }
            else if (collider.CompareTag("Lock1"))
            {
                if (oreCount >= 10)
                {
                    ShowWarningText("Gate Unlocked!");
                    Destroy(collider.gameObject);
                    oreCount -= 10;
                }
                else
                {
                    ShowWarningText("Insufficient resources to unlock! (10 Dagelium)");
                }
            }
            else if (collider.CompareTag("Lock2"))
            {
                if (alumiteCount >= 500)
                {
                    ShowWarningText("Gate Unlocked!");
                    Destroy(collider.gameObject);
                    alumiteCount -= 500;
                }
                else
                {
                    ShowWarningText("Insufficient resources to unlock! (500 Alumite)");
                }
            }
            else if (collider.CompareTag("Lock3"))
            {
                if (flowerCount >= 8)
                {
                    ShowWarningText("Gate Unlocked!");
                    Destroy(collider.gameObject);
                    flowerCount -= 8;
                }
                else
                {
                    ShowWarningText("Insufficient resources to unlock! (8 Blossoms)");
                }
            }
            else if (collider.CompareTag("Lock4"))
            {
                if (keyObtained == true)
                {
                    ShowWarningText("Gate Unlocked!");
                    Destroy(collider.gameObject);
                    keyBulb.SetActive(false);
                }
                else
                {
                    ShowWarningText("Find the Key Bulb to unlock!");
                }
            }
            else if (collider.CompareTag("Unseal"))
            {
                if (sealCount >= 5)
                {
                    ShowWarningText("Alias The Mage of Destruction Has Been Unsealed!");
                    Destroy(collider.gameObject);
                    sealCount -= 5;
                }
                else
                {
                    ShowWarningText("Seems like it requires more to Summon this Boss! (5 Ancient Seals)");
                }
            }

            UpdateCounters();
        }
    }

    private void UpdateCounters()
    {
        alumiteCounterText.text = "Alumite: " + alumiteCount;
        oreCounterText.text = "Dagelium Ore: " + oreCount;
        flowerCounterText.text = "Blossoms: " + flowerCount;
        sealCounterText.text = "Ancient Seals: " + sealCount;

    }

    private void ShowWarningText(string message)
    {
        WarningText.text = message;
        StartCoroutine(HideWarningText());
    }

    private IEnumerator HideWarningText()
    {
        yield return new WaitForSeconds(10f);
        WarningText.text = "";
    }

    private IEnumerator AlumiteDrain()
   {
     drainingTimer += Time.deltaTime;
   
        if (drainingTimer >= drainingInterval)
        {
           drainingTimer = 0f;
       
            if (alumiteCount > 0)
            {
               alumiteCount -= 1;
               UpdateCounters();
            }
        }

        yield return null;
    }

    private IEnumerator BlossomDrain()
    {
        drainingTimer += Time.deltaTime;

        if (drainingTimer >= drainingInterval)
        {
            drainingTimer = 0f;

            if (flowerCount > 0)
            {
                flowerCount -= 1;
                UpdateCounters();
            }
        }

        yield return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
