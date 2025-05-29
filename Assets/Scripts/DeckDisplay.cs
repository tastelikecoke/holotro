using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckDisplay : MonoBehaviour
{

    [SerializeField]
    private TMP_Text deckText;
    [SerializeField]
    private Image deckImage;

    public void Populate(List<Card> cards)
    {
        deckText.text = cards.Count.ToString();
        deckImage.fillAmount = Mathf.Clamp01(cards.Count / 40f);
    }

}
