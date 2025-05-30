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
    private FaceChoiceDisplay faceChoiceDisplay;
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
        currentDeckConfig = Instantiate(persistence.GetDeckConfig());

        BeginGame();
    }
    private void BeginGame()
    {
        deck = new List<Card>();
        deck.AddRange(currentDeckConfig.faceCards);
        deck.AddRange(currentDeckConfig.karaokeCards);
        deck.AddRange(currentDeckConfig.wildCards);

        string[] colors = {"Red", "Green", "Blue", "Yellow"};
        //string[] faces = {"Ace", "King", "Queen", "Jack", "Jester"};
        for (int i = 0; i < currentDeckConfig.gameColors.Count; i++)
        {
            for (int j = 2; j <= currentDeckConfig.gameFacesAmount; j++)
            {
                var card = new Card();
                card.Color = currentDeckConfig.gameColors[i].Color;
                card.Face = j.ToString();
                card.Description = currentDeckConfig.gameColors[i].Description;

                deck.Add(card);

            }
        }

        gameDisplay.DisplayDate(currentDeckConfig.deckDate, persistence.GetLevel());

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
        return IsNull(takenCard) && IsNull(takenWildCard);
    }

    public bool IsNull(Card card)
    {
        if (card is null) return true;
        if (string.IsNullOrEmpty(card.Color)) return true;
        return false;
    }

    public IEnumerator ProcessGameCR()
    {
        for( int i = 0; i < 10000; i++)
        {
            yourHandDisplay.SetPassPanel(!IsNull(currentCard));

            deckDisplay.Populate(deck);
            takenCard = null;
            takenWildCard = null;
            yourHandDisplay.Populate(yourHand);
            gameDisplay.DisplayText("Start streaming! Choose a card.");
            yield return new WaitUntil(() => !IsNull(takenCard));

            if (IsNull(currentCard) || !IsNull(takenWildCard))
            {
                if(!IsNull(takenWildCard))
                    yourHand.Remove(takenWildCard);
                else
                    yourHand.Remove(takenCard);
                currentCard = takenCard;
                Debug.Log("Any match");
                Persistence.Instance.CardsCount++;
            }
            else if (takenCard.Color == currentCard.Color)
            {
                if(!IsNull(takenWildCard))
                    yourHand.Remove(takenWildCard);
                else
                    yourHand.Remove(takenCard);
                currentCard = takenCard;
                Debug.Log("Color match");
                Persistence.Instance.CardsCount++;
            }
            else if (takenCard.Face ==  currentCard.Face)
            {
                if(!IsNull(takenWildCard))
                    yourHand.Remove(takenWildCard);
                else
                    yourHand.Remove(takenCard);
                currentCard = takenCard;
                Debug.Log("Face match");
                Persistence.Instance.CardsCount++;
            }
            else
            {
                Debug.Log("No match");

                yield return deckDisplay.GiveCard(yourHandDisplay);
                yourHand.Add(Pop(deck));
                yourHandDisplay.Populate(yourHand);
                if (deck.Count <= 0)
                {
                    yield return JudgeGameCR();
                    yield break;
                }

                yield return deckDisplay.GiveCard(yourHandDisplay);
                yourHand.Add(Pop(deck));
                yourHandDisplay.Populate(yourHand);
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

            gameDisplay.DisplayText("YabairyS is thinking...");
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
                yield return deckDisplay.GiveCard(enemyHandDisplay);
                enemyHand.Add(Pop(deck));
                enemyHandDisplay.Populate(enemyHand);
                if (deck.Count <= 0)
                {
                    yield return JudgeGameCR();
                    yield break;
                }
                yield return deckDisplay.GiveCard(enemyHandDisplay);
                enemyHand.Add(Pop(deck));
                enemyHandDisplay.Populate(enemyHand);
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
                    int faceCount = currentDeckConfig.facesAvailable.Count;
                    currentCard.Face = currentDeckConfig.facesAvailable[Random.Range(0, faceCount)];
                }
                currentCardDisplay.Populate(currentCard);
            }

            gameDisplay.DisplayText("YabairyS finished her turn!");
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
            gameDisplay.DisplayText("YabairyS wins...");
            gameDisplay.DisplayLose();

            bool ogMute = Persistence.Instance.GetMute();
            Persistence.Instance.SetMute(true);
            yield return new WaitForSeconds(7.0f);
            Persistence.Instance.SetMute(ogMute);

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
                gameDisplay.DisplayText("YabairyS wins...");
                gameDisplay.DisplayLose();
                bool ogMute = Persistence.Instance.GetMute();
                Persistence.Instance.SetMute(true);
                yield return new WaitForSeconds(7.0f);
                Persistence.Instance.SetMute(ogMute);
            }
            if (enemyHand.Count == yourHand.Count)
            {
                gameDisplay.DisplayText("You win for now...");
                gameDisplay.DisplayWin();
            }
        }
        persistence.SetLastCard(currentCard);
        yield break;
    }

    public void TakeCard(Card card)
    {
        if (!IsNull(takenCard)) return;

        if (card.Color == "Wildcard")
        {
            takenWildCard = card;
            colorChooseDisplay.SetActive(true);
        }
        else if (card.Face == "Wildcard")
        {
            takenWildCard = card;
            faceChoiceDisplay.gameObject.SetActive(true);
            faceChoiceDisplay.Populate(currentDeckConfig.facesAvailable);
        }
        else
        {
            takenCard = card;
        }
    }

    public void ChooseColor(string chosenColor)
    {
        takenCard = takenWildCard.Clone();
        takenCard.Color = chosenColor;
        colorChooseDisplay.SetActive(false);
    }
    public void ChooseFace(string chosenFace)
    {
        takenCard = takenWildCard.Clone();
        takenCard.Face = chosenFace;
        faceChoiceDisplay.gameObject.SetActive(false);
    }

    public void NextGame()
    {
        if (persistence != null)
            persistence.NextGame();
    }
}
