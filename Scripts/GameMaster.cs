using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    static bool gameHasEnded = false;

    public int score = 0;

    public float restartDelay = 1f;

    public GameObject completeLevelUI;

    public void LevelEnd()
    {
        completeLevelUI.SetActive(true);
    }

    public void EndGame ()
    {

        {
            if (gameHasEnded == false)
            {
                gameHasEnded = true;
                Debug.Log("Game Over");
                Invoke("Restart",restartDelay);
            }
        }
    }


    void Restart ()
    {
        SceneManager.LoadScene(6);
    }



}
