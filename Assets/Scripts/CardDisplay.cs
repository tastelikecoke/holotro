using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class Card : IEquatable<Card>
{
    public string Description;
    public string Color;
    public string Face;

    public Card Clone()
    {
        return new Card() { Description = Description, Color = Color, Face = Face, };
    }

    public bool Equals(Card other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return Description == other.Description && Color == other.Color && Face == other.Face;
    }
    public override bool Equals(object obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj.GetType() != GetType())
            return false;
        return Equals((Card)obj);
    }
    public override int GetHashCode() => HashCode.Combine(Description, Color, Face);
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

    [SerializeField]
    private AnimationCurve curve;

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
        if (!gameManager.IsYourTurn()) return;

        transform.SetParent(center.transform, true);
        Card card = currentCard.Clone();
        StartCoroutine(OnUseCard(() =>
            gameManager.TakeCard(card)));
    }
    public IEnumerator OnUseCard(Action callback = null)
    {
        var originalPosition = transform.position;
        const float duration = 0.5f;
        for (float i = 0f; i < duration; )
        {
            i += Time.deltaTime;
            if (this == null)
            {
                callback?.Invoke();
                yield break;
            }

            var curveValue = curve.Evaluate(i / duration);

            transform.position = Vector3.Lerp(originalPosition, center.position, curveValue);
            yield return new WaitForEndOfFrame();
        }
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
        callback?.Invoke();
    }
}
