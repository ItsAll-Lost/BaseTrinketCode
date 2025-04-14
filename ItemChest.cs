using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChestScript : MonoBehaviour
{

    public GameObject itemPrefab1;
    public GameObject itemPrefab2;
    public GameObject itemPrefab3;
    public float detectionRange = 0.8f;
    public Transform spawnPoint;

    private bool isOpened = false;

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider collider in colliders)
        {
            if (!isOpened && collider.CompareTag("Player"))
            {
                OpenChest();
                break; 

            }
        }
    }

    private void OpenChest()
    {
        isOpened = true;

        int randomRoll = Random.Range(1, 4);

        switch (randomRoll)
        {
            case 1:
                if (itemPrefab1 != null)
                {
                    SpawnItem(itemPrefab1);
                }
                break;
            case 2:
                if (itemPrefab2 != null)
                {
                    SpawnItem(itemPrefab2);
                }
                break;
            case 3:
                if (itemPrefab3 != null)
                {
                    SpawnItem(itemPrefab3);
                }
                break;
            default:
                Debug.LogError("Invalid random roll: " + randomRoll);
                break;
        }

        Destroy(gameObject);
    }

    private void SpawnItem(GameObject prefab)
    {
        if (spawnPoint != null && prefab != null)
        {
            Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
