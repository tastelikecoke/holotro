using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameColor
{
    public string Color;
    public string Description;
}

[CreateAssetMenu(fileName = "Deck", menuName = "Deck")]
public class DeckConfig : ScriptableObject
{

    public string deckDate;
    public List<string> facesAvailable;
    public List<Card> faceCards;
    public List<Card> karaokeCards;
    public List<Card> wildCards;
    public List<Card> otherCards;
    public int gameFacesAmount;
    public List<GameColor> gameColors;
}
