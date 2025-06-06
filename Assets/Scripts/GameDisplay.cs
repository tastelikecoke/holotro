using TMPro;
using UnityEngine;

public class GameDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text cardText;
    [SerializeField]
    private TMP_Text dateText;
    [SerializeField]
    private TMP_Text dateHoverText;
    [SerializeField]
    private TMP_Text finalStatsText;

    [SerializeField]
    private GameObject thinkRys;
    [SerializeField]
    private GameObject winRys;
    [SerializeField]
    private GameObject sadRys;
    [SerializeField]
    private GameObject nextMonthButton;

    public void DisplayDate(string text, int level)
    {
        dateText.text = text;
        dateHoverText.text = $"{level} out of 9 months has passed!";
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
        nextMonthButton.SetActive(true);
    }
    public void DisplayLose()
    {
        thinkRys.gameObject.SetActive(true);
        winRys.gameObject.SetActive(false);
        sadRys.gameObject.SetActive(true);

        if(finalStatsText != null)
            finalStatsText.text = $"Farthest Date: {dateText.text}\nAmount of cards used:{Persistence.Instance.CardsCount}";
    }
}
