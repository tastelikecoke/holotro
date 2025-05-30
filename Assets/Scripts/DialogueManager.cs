using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text dialogueText;
    [SerializeField]
    Image yabaiRyS;
    [SerializeField]
    Image noRyS;

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
        yabaiRyS.gameObject.SetActive(true);
        dialogueText.text = "YabairyS\n\nNot so fast!";
        yield return new WaitUntil(() => IsTriggered);
        IsTriggered = false;
        dialogueText.text = "IryS\n\nYabairyS! Where did you come from?";
        yield return new WaitUntil(() => IsTriggered);
        IsTriggered = false;
        dialogueText.text = "YabairyS\n\nI came inside of you!";
        yield return new WaitUntil(() => IsTriggered);
        IsTriggered = false;
        noRyS.gameObject.SetActive(true);
        dialogueText.text = "IryS\n\n*Shock* How dare you say that on stream!";
        yield return new WaitUntil(() => IsTriggered);
        IsTriggered = false;
        dialogueText.text = "YabairyS\n\nI will say more once I take over your body~";
        yield return new WaitUntil(() => IsTriggered);
        IsTriggered = false;
        dialogueText.text = "IryS\n\nYou can't do that!";
        yield return new WaitUntil(() => IsTriggered);
        IsTriggered = false;
        dialogueText.text = "YabairyS\n\nNot if I win against you on a card game duel!";

        yield return new WaitUntil(() => IsTriggered);
        IsTriggered = false;
        SceneManager.LoadScene("Game");
    }

}
