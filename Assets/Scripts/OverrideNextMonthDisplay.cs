using TMPro;
using UnityEngine;

public class OverrideNextMonthDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject somePanel;
    [SerializeField]
    private TMP_Text someText;
    [SerializeField]
    private TMP_Text finalStatsText;
    private void Start()
    {
        if (Persistence.Instance.IsLastGame())
        {
            somePanel.gameObject.SetActive(true);
            someText.text = "Start New Game";
            if(finalStatsText != null)
                finalStatsText.text = $"Farthest Date: March 2022 (Bday!)\nAmount of cards used:{Persistence.Instance.CardsCount}";
        }
    }
}
