using System.Collections;
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
    [SerializeField]
    private GameObject facedownCard;
    [SerializeField]
    private AnimationCurve curve;

    public void Populate(List<Card> cards)
    {
        deckText.text = cards.Count.ToString();
        deckImage.fillAmount = Mathf.Clamp01(cards.Count / 40f);
    }

    public IEnumerator GiveCard(HandDisplay handDisplay)
    {
        var facedownCardInstance = Instantiate(facedownCard, transform);
        facedownCardInstance.gameObject.SetActive(true);

        var originalPosition = transform.position;
        const float duration = 0.3f;
        for (float i = 0f; i < duration; )
        {
            i += Time.deltaTime;
            if (this == null)
            {
                yield break;
            }

            var curveValue = curve.Evaluate(i / duration);

            facedownCardInstance.transform.position = Vector3.Lerp(originalPosition, handDisplay.transform.position, curveValue);
            yield return new WaitForEndOfFrame();
        }
        facedownCardInstance.SetActive(false);
        Destroy(facedownCardInstance);
    }

}
