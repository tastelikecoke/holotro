using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckDisplay : MonoBehaviour
{

    [SerializeField]
    private TMP_Text deckText;

    public void Populate(List<Card> cards)
    {
        deckText.text = cards.Count.ToString();
    }

}
