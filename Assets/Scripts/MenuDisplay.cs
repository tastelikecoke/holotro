using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDisplay : MonoBehaviour
{

    public void StartGame()
    {
        Persistence.Instance.ResetLevel();
        SceneManager.LoadScene("Game");
    }
}
