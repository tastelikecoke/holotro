using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNewDisplay : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Menu");
    }

}
