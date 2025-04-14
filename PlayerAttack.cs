using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttack : MonoBehaviour
{
    public GameObject weapon;
    public Slider cooldownSlider;

    public GameObject weapon2;
    public Slider cooldownSlider2;

    public GameObject weapon3;
    public Slider cooldownSlider3;

    public GameObject weapon4;
    public Slider cooldownSlider4;

    public Transform projectileSpawnPoint;
    public float maxRaycastDistance = 0.2f;

    private bool canFire = false;
    private bool canFire2 = false;
    private bool canFire3 = false;
    private bool canFire4 = false;


    private float spell1cooldown = 15f;
    private float spell1cooldown2 = 80f;
    private float spell1cooldown3 = 5f;
    private float spell1cooldown4 = 8f;


    private float weaponTimer = 5f;
    private float weaponTimer2 = 10f;



    public Slider destructibleCooldownSlider;
    public float destructibleCooldown = 5f;
    private bool canDestroy = false;

    private void Start()
    {
        cooldownSlider.maxValue = spell1cooldown;
        cooldownSlider2.maxValue = spell1cooldown2;
        cooldownSlider3.maxValue = spell1cooldown3;
        cooldownSlider4.maxValue = spell1cooldown4;

        destructibleCooldownSlider.maxValue = destructibleCooldown;
    }

    private void Update()
    {
        if (!canFire)
        {
            spell1cooldown += Time.deltaTime;
            cooldownSlider.value = spell1cooldown;
            if (spell1cooldown >= cooldownSlider.maxValue)
            {
                canFire = true;
                spell1cooldown = cooldownSlider.maxValue;
            }
        }

        if (Input.GetKey(KeyCode.E) && canFire)
        {
            FireWeapon();
        }

        if (!canFire2)
        {
            spell1cooldown2 += Time.deltaTime;
            cooldownSlider2.value = spell1cooldown2;
            if (spell1cooldown2 >= cooldownSlider2.maxValue)
            {
                canFire2 = true;
                spell1cooldown2 = cooldownSlider2.maxValue;
            }
        }

        if (!canDestroy)
        {
            destructibleCooldown += Time.deltaTime;
            destructibleCooldownSlider.value = Mathf.Clamp(destructibleCooldown, 0, destructibleCooldownSlider.maxValue);
            if (destructibleCooldown >= destructibleCooldownSlider.maxValue)
            {
                canDestroy = true;
                destructibleCooldown = destructibleCooldownSlider.maxValue;
            }
        }

        if (Input.GetKey(KeyCode.F) && canFire2)
        {
            FireWeapon2();
        }

        if (!canFire3)
        {
            spell1cooldown3 += Time.deltaTime;
            cooldownSlider3.value = spell1cooldown3;
            if (spell1cooldown3 >= cooldownSlider3.maxValue)
            {
                canFire3 = true;
                spell1cooldown3 = cooldownSlider3.maxValue;
            }
        }

        if (Input.GetKey(KeyCode.Q) && canFire3)
        {
            FireWeapon3();
        }

        if (Input.GetKey(KeyCode.V) && canDestroy)
        {
            DestroyDestructibleObject();
        }

        if (!canFire4)
        {
            spell1cooldown4 += Time.deltaTime;
            cooldownSlider4.value = spell1cooldown4;
            if (spell1cooldown4 >= cooldownSlider4.maxValue)
            {
                canFire4 = true;
                spell1cooldown4 = cooldownSlider4.maxValue;
            }
        }

        if (Input.GetKey(KeyCode.B) && canFire4)
        {
            FireWeapon4();
        }
    }



    private void FireWeapon()
    {
        weapon.SetActive(true);
        Invoke("HideWeapon", weaponTimer);
        canFire = false;
        spell1cooldown = 0f;
        cooldownSlider.value = spell1cooldown;
    }

    private void FireWeapon2()
    {
        weapon2.SetActive(true);
        Invoke("HideWeapon2", weaponTimer2);
        canFire2 = false;
        spell1cooldown2 = 0f;
        cooldownSlider2.value = spell1cooldown2;
    }

    private void FireWeapon3()
    {
        GameObject projectile = Instantiate(weapon3, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        Destroy(projectile, 8f); 
        canFire3 = false;
        spell1cooldown3 = 0f;
        cooldownSlider3.value = spell1cooldown3;
    }

    private void HideWeapon()
    {
        weapon.SetActive(false);
    }

    private void HideWeapon2()
    {
        weapon2.SetActive(false);
    }

 
    private void DestroyDestructibleObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxRaycastDistance))
        {
            if (hit.collider.CompareTag("Destructible"))
            {
                Destroy(hit.collider.gameObject);
                canDestroy = false;
                destructibleCooldown = 0f;
            }
        }
    }
    private void FireWeapon4()
    {
        GameObject projectile = Instantiate(weapon4, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        Destroy(projectile, 8f);
        canFire4 = false;
        spell1cooldown4 = 0f;
        cooldownSlider4.value = spell1cooldown4;

    }
}
