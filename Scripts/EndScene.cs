using UnityEngine;

public class EndScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void Quit ()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }
}