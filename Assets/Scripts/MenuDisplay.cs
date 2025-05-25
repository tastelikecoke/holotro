using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDisplay : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
