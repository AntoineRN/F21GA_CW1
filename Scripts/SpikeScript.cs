using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public Transform player;

    public Transform respawnPoint;

    public int spawnDelay = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            player.transform.position = respawnPoint.transform.position;
            Physics.SyncTransforms();
            FindObjectOfType<GameMaster>().EndGame();


        }
    }
}
