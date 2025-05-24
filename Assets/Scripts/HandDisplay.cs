using System.Collections.Generic;
using UnityEngine;

public class HandDisplay : MonoBehaviour
{
    [SerializeField]
    private CardDisplay prefab;

    private List<CardDisplay> cardDisplays;

    public void Populate(List<Card> cards)
    {
        if(cardDisplays == null)
            cardDisplays = new List<CardDisplay>();

        prefab.gameObject.SetActive(false);
        foreach (var cardDisplay in cardDisplays)
        {
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
    }
}
