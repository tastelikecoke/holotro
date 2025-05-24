using TMPro;
using UnityEngine;

public class GameDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text cardText;

    public void DisplayText(string text)
    {
        cardText.text = text;
    }
}
