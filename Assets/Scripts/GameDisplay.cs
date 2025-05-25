using TMPro;
using UnityEngine;

public class GameDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text cardText;
    [SerializeField]
    private TMP_Text dateText;

    [SerializeField]
    private GameObject thinkRys;
    [SerializeField]
    private GameObject winRys;
    [SerializeField]
    private GameObject sadRys;

    public void DisplayDate(string text)
    {
        dateText.text = text;
    }
    public void DisplayText(string text)
    {
        cardText.text = text;
    }
    public void DisplayWin()
    {
        thinkRys.gameObject.SetActive(false);
        winRys.gameObject.SetActive(true);
        sadRys.gameObject.SetActive(false);
    }
    public void DisplayLose()
    {
        thinkRys.gameObject.SetActive(false);
        winRys.gameObject.SetActive(false);
        sadRys.gameObject.SetActive(true);
    }
}
