using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private HandDisplay yourHandDisplay;
    [SerializeField]
    private HandDisplay enemyHandDisplay;
    [SerializeField]
    private GameObject colorChooseDisplay;
    [SerializeField]
    private CardDisplay currentCardDisplay;
    [SerializeField]
    private DeckDisplay deckDisplay;
    [SerializeField]
    private GameDisplay gameDisplay;

    private DeckConfig currentDeckConfig;
    private Persistence persistence;
    private List<Card> deck;
    private List<Card> yourHand;
    private List<Card> enemyHand;
    private Card currentCard;

    private Card Pop(List<Card> cards)
    {
        int choice = Random.Range(0, cards.Count);
        Card chosen = cards[choice];
        cards.RemoveAt(choice);
        return chosen;
    }
    private void Start()
    {
        persistence = Persistence.Instance;
        currentDeckConfig = persistence.GetDeckConfig();

        BeginGame();
    }
    private void BeginGame()
    {
        deck = new List<Card>();
        deck.AddRange(currentDeckConfig.faceCards);
        deck.AddRange(currentDeckConfig.wildCards);

        string[] colors = {"Red", "Green", "Blue", "Yellow"};
        //string[] faces = {"Ace", "King", "Queen", "Jack", "Jester"};
        for (int i = 0; i < currentDeckConfig.gameColors.Count; i++)
        {
            for (int j = 1; j <= currentDeckConfig.gameFacesAmount; j++)
            {
                var card = new Card();
                card.Color = currentDeckConfig.gameColors[i].Color;
                card.Face = j.ToString();
                card.Description = currentDeckConfig.gameColors[i].Description;

                deck.Add(card);

            }
        }

        gameDisplay.DisplayDate(currentDeckConfig.deckDate);

        yourHand = new List<Card>();
        for (int i = 0; i < 5; i++)
        {
            yourHand.Add(Pop(deck));
        }
        enemyHand = new List<Card>();
        for (int i = 0; i < 5; i++)
        {
            enemyHand.Add(Pop(deck));
        }

        yourHandDisplay.Populate(yourHand);
        enemyHandDisplay.Populate(enemyHand);
        deckDisplay.Populate(deck);

        StartCoroutine(ProcessGameCR());
    }

    private Card takenWildCard = null;
    private Card takenCard = null;

    public bool IsYourTurn()
    {
        return takenCard == null && takenWildCard == null;
    }


    public IEnumerator ProcessGameCR()
    {
        for( int i = 0; i < 10000; i++)
        {
            deckDisplay.Populate(deck);
            takenCard = null;
            takenWildCard = null;
            gameDisplay.DisplayText("Start a stream!");
            yield return new WaitUntil(() => takenCard != null);

            if (currentCard == null || takenWildCard != null)
            {
                if(takenWildCard != null)
                    yourHand.Remove(takenWildCard);
                else
                    yourHand.Remove(takenCard);
                currentCard = takenCard;
                Debug.Log("Any match");
            }
            else if (takenCard.Color == currentCard.Color)
            {
                if(takenWildCard != null)
                    yourHand.Remove(takenWildCard);
                else
                    yourHand.Remove(takenCard);
                currentCard = takenCard;
                Debug.Log("Color match");
            }
            else if (takenCard.Face == currentCard.Face)
            {
                if(takenWildCard != null)
                    yourHand.Remove(takenWildCard);
                else
                    yourHand.Remove(takenCard);
                currentCard = takenCard;
                Debug.Log("Face match");
            }
            else
            {
                Debug.Log("No match");
                yourHand.Add(Pop(deck));
                if (deck.Count <= 0)
                {
                    yield return JudgeGameCR();
                    yield break;
                }
                yourHand.Add(Pop(deck));
                if (deck.Count <= 0)
                {
                    yield return JudgeGameCR();
                    yield break;
                }
            }

            currentCardDisplay.Populate(currentCard);
            currentCardDisplay.gameObject.SetActive(true);

            yourHandDisplay.Populate(yourHand);
            deckDisplay.Populate(deck);

            if (yourHand.Count <= 0)
            {
                yield return JudgeGameCR();
                yield break;
            }

            gameDisplay.DisplayText("Evilrys is thinking...");
            yield return new WaitForSeconds(3.0f);

            List<Card> enemyChoices = new List<Card>();
            foreach (Card enemyCard in enemyHand)
            {
                if (enemyCard.Face == currentCard.Face)
                {
                    enemyChoices.Add(enemyCard);
                }
                else if (enemyCard.Color == currentCard.Color)
                {
                    enemyChoices.Add(enemyCard);
                }
                else if (enemyCard.Color == "Wildcard")
                {
                    enemyChoices.Add(enemyCard);
                }
                else if (enemyCard.Face == "Wildcard")
                {
                    enemyChoices.Add(enemyCard);
                }
            }

            if (enemyChoices.Count <= 0)
            {
                Debug.Log("No match");
                enemyHand.Add(Pop(deck));
                if (deck.Count <= 0)
                {
                    yield return JudgeGameCR();
                    yield break;
                }
                enemyHand.Add(Pop(deck));
                if (deck.Count <= 0)
                {
                    yield return JudgeGameCR();
                    yield break;
                }
            }
            else
            {
                var enemyChoice = Pop(enemyChoices);
                enemyHand.Remove(enemyChoice);

                currentCard = enemyChoice;

                if (currentCard.Color == "Wildcard")
                {
                    string[] colors = {"Red", "Green", "Blue", "Yellow"};
                    currentCard.Color = colors[Random.Range(0, 4)];
                }
                if (currentCard.Face == "Wildcard")
                {
                    string[] faces = {"King", "Queen", "Jack", "Jester", "Ace"};
                    currentCard.Face = faces[Random.Range(0, 4)];
                }
                currentCardDisplay.Populate(currentCard);
            }

            gameDisplay.DisplayText("Evilrys posts a video!");
            enemyHandDisplay.PutCard(currentCard);
            yield return new WaitForSeconds(1.0f);
            enemyHandDisplay.Populate(enemyHand);

            if (enemyHand.Count <= 0)
            {
                yield return JudgeGameCR();
                yield break;
            }
        }
    }

    public IEnumerator JudgeGameCR()
    {
        deckDisplay.Populate(deck);

        if (yourHand.Count == 0)
        {
            gameDisplay.DisplayText("You win!!");
            gameDisplay.DisplayWin();
        }

        if (enemyHand.Count == 0)
        {
            gameDisplay.DisplayText("Evilrys wins...");
            gameDisplay.DisplayLose();
        }

        if (deck.Count <= 0)
        {
            if (enemyHand.Count > yourHand.Count)
            {
                gameDisplay.DisplayText("You win!!");
                gameDisplay.DisplayWin();
            }
            if (enemyHand.Count < yourHand.Count)
            {
                gameDisplay.DisplayText("Evilrys wins...");
                gameDisplay.DisplayLose();
            }
            if (enemyHand.Count == yourHand.Count)
            {
                gameDisplay.DisplayText("You win for now...");
                gameDisplay.DisplayWin();
            }
        }
        yield break;
    }

    public void TakeCard(Card card)
    {
        if (takenCard != null) return;

        if (card.Color == "Wildcard")
        {
            takenWildCard = card;
            colorChooseDisplay.SetActive(true);
        }
        else
        {
            takenCard = card;
        }
    }

    public void ChooseColor(string chosenColor)
    {
        takenWildCard.Color = chosenColor;

        takenCard = takenWildCard;
        colorChooseDisplay.SetActive(false);
    }

    public void NextGame()
    {
        if (persistence != null)
            persistence.NextGame();
    }
}
