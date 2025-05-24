using TMPro;
using UnityEngine;


[System.Serializable]
public class Card
{
    public string Description;
    public string Color;
    public string Face;
}

public class CardDisplay : MonoBehaviour
{
    private Card currentCard;

    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject red;
    [SerializeField]
    private GameObject yellow;
    [SerializeField]
    private GameObject green;
    [SerializeField]
    private GameObject blue;
    [SerializeField]
    private GameObject wildCard;

    [SerializeField]
    private TMP_Text cardText;
    [SerializeField]
    private TMP_Text faceText;
    public void Populate(Card card)
    {
        currentCard = card;
        cardText.text = currentCard.Description;
        faceText.text = currentCard.Face;
        red.SetActive(currentCard.Color == "Red");
        yellow.SetActive(currentCard.Color == "Yellow");
        green.SetActive(currentCard.Color == "Green");
        blue.SetActive(currentCard.Color == "Blue");
        wildCard.SetActive(currentCard.Color == "Wildcard");
    }

    public void OnClick()
    {
        gameManager.TakeCard(currentCard);
    }
}
