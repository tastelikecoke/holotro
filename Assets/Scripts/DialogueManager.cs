using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text dialogueText;

    private bool IsTriggered = false;

    private void Start()
    {
        StartCoroutine(StartCR());
    }
    public void Trigger()
    {
        IsTriggered = true;
    }

    private IEnumerator StartCR()
    {
        dialogueText.text = "IryS\n\nThis is it! I finally debuted.";
        yield return new WaitUntil(() => IsTriggered);
        IsTriggered = false;
        dialogueText.text = "IryS\n\nTime to start my seiso hololive career!";
        yield return new WaitUntil(() => IsTriggered);
        IsTriggered = false;
        dialogueText.text = "YabairyS\n\nNot so fast!";
        yield return new WaitUntil(() => IsTriggered);
        IsTriggered = false;
        dialogueText.text = "IryS\n\nYabairyS! Where did you come from?";
        yield return new WaitUntil(() => IsTriggered);
        IsTriggered = false;
        dialogueText.text = "YabairyS\n\nI came inside of you!";
        yield return new WaitUntil(() => IsTriggered);
        IsTriggered = false;
        dialogueText.text = "IryS\n\nBaeface.jpg";
        yield return new WaitUntil(() => IsTriggered);
        IsTriggered = false;

        SceneManager.LoadScene("Game");
    }

}
