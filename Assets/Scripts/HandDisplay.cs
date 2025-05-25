using System.Collections.Generic;
using UnityEngine;

public class HandDisplay : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private CardDisplay prefab;
    [SerializeField]
    private GameObject passPrefab;

    private List<CardDisplay> cardDisplays;

    public void Populate(List<Card> cards)
    {
        if(cardDisplays == null)
            cardDisplays = new List<CardDisplay>();

        prefab.gameObject.SetActive(false);
        foreach (var cardDisplay in cardDisplays)
        {
            if(cardDisplay != null)
                Destroy(cardDisplay.gameObject);
        }

        cardDisplays.Clear();

        for (int i = 0; i < cards.Count; i++)
        {
            var cardDisplay = Instantiate(prefab, transform);
            cardDisplay.gameObject.SetActive(true);
            cardDisplays.Add(cardDisplay);
            cardDisplay.Populate(cards[i]);
        }

        if(passPrefab != null)
            passPrefab.transform.SetAsLastSibling();
    }

    public void PutCard(Card card)
    {
        foreach (var cardDisplay in cardDisplays)
        {
            if (cardDisplay.currentCard == card)
            {
                StartCoroutine(cardDisplay.OnUseCard());
            }
        }
    }
    public void Pass()
    {
        var card = new Card();
        card.Color = "Pass";
        card.Face = "Pass";
        gameManager.TakeCard(card);

    }
}
