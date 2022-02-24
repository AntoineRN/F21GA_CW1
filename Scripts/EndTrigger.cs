using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameMaster gameMaster;

    void OnTriggerEnter2D (Collider2D gem)
    {
        if (gem.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            gameMaster.LevelEnd();
        }
    }
}
