using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Persistence : MonoBehaviour
{
    private static Persistence _instance;

    [SerializeField]
    private List<DeckConfig> deckConfigs;

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

}
