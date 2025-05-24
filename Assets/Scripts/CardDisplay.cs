using System;
using System.Collections;
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
    [NonSerialized]
    public Card currentCard;

    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private Transform center;
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
        transform.SetParent(center.transform, true);
        StartCoroutine(OnUseCard(() =>
            gameManager.TakeCard(currentCard)));
    }
    public IEnumerator OnUseCard(Action callback = null)
    {
        var originalPosition = transform.position;
        for (int i = 0; i < 120; i++)
        {
            transform.position = Vector3.Lerp(transform.position, center.position, (float)i/120.0f);
            yield return new WaitForEndOfFrame();
        }
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
        callback?.Invoke();
    }
}
