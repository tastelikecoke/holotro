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

    public void SetPassPanel(bool value)
    {
        passPrefab.SetActive(value);
    }

    public void Populate(List<Card> cards)
    {
        if(cardDisplays == null)
            cardDisplays = new List<CardDisplay>();

        prefab.gameObject.SetActive(false);
        for (int i = cardDisplays.Count-1; i >= 0; i--)
        {
            if (cardDisplays[i] == null)
            {
                cardDisplays.RemoveAt(i);
            }
        }

        for (int i = 0; i < cards.Count; i++)
        {
            CardDisplay cardDisplay = null;
            if (cardDisplays.Count > i)
            {
                cardDisplay = cardDisplays[i];
                cardDisplays[i].gameObject.SetActive(true);
            }
            else
            {
                cardDisplay = Instantiate(prefab, transform);
                cardDisplays.Add(cardDisplay);
                cardDisplay.gameObject.SetActive(true);
                var animator = cardDisplay.GetComponentInChildren<Animator>();

                if (animator != null)
                    animator.speed = Random.Range(0.80f, 1.2f);
            }
            cardDisplays[i].GetComponent<CanvasGroup>().alpha = 1;
            cardDisplay.Populate(cards[i]);
        }

        for (int i = cardDisplays.Count - 1; i >= cards.Count; i--)
        {
            cardDisplays[i].gameObject.SetActive(false);
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
