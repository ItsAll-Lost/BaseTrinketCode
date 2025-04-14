using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGateScript : MonoBehaviour
{
    public Transform player, destination;
    public GameObject playerg;
    public GameObject oldWorld;
    public GameObject newWorld;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerg.SetActive(false);
            player.position = destination.position;
            playerg.SetActive(true);
            newWorld.SetActive(true);
            oldWorld.SetActive(false);

        }
    }
}
