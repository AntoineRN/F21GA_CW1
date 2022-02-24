using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public Transform player;

    public Transform respawnPoint;

    public int spawnDelay = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            FindObjectOfType<GameMaster>().EndGame();


        }
    }
}
