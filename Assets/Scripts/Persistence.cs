using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Persistence : MonoBehaviour
{
    private static Persistence _instance;

    [SerializeField]
    private List<DeckConfig> deckConfigs;
    public int CardsCount
    {
        get;
        set;
    }

    private int deckLevel;
    private Card lastCard = null;

    public static Persistence Instance
    {
        get
        {
            if(_instance == null) _instance = GameObject.FindObjectOfType<Persistence>();
            return _instance;
        }
    }

    public void ToggleMute()
    {
        GetComponent<AudioSource>().mute = !GetComponent<AudioSource>().mute;
    }

    public void SetMute(bool isMute = true)
    {
        GetComponent<AudioSource>().mute = isMute;
    }
    public bool GetMute()
    {
        return GetComponent<AudioSource>().mute;
    }

    public void Awake()
    {
        if(_instance == null) _instance = this;
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
        deckLevel = 0;
    }

    public DeckConfig GetDeckConfig()
    {
        return deckConfigs[deckLevel];
    }
    public int GetLevel()
    {
        return deckLevel;
    }
    public void SetLastCard(Card card)
    {
        lastCard = card.Clone();
    }

    public void NextGame()
    {
        deckLevel++;
        SceneManager.LoadScene("Game");
    }

    public void ResetLevel()
    {
        deckLevel = 0;
        CardsCount = 0;
    }


}
