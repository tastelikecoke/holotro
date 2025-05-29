using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDisplay : MonoBehaviour
{

    public void StartGame()
    {
        Persistence.Instance.ResetLevel();
        SceneManager.LoadScene("Game");
    }
    public void Tutorial()
    {
        Persistence.Instance.ResetLevel();
        SceneManager.LoadScene("Tutorial");
    }
}
