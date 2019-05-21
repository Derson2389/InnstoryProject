using UnityEngine;
using System.Collections.Generic;
using System;
using System.Text;

[Serializable()]
public class Deck {

    public Faction Faction { get; private set; }
    private List<Card> _cards;
    
    public Deck()
    {
        _cards = new List<Card>();

        // temp hard coded deck
        CharacterCard charCard = (CharacterCard)CardManager.instance.GetCardByType(CardType.CharacterCard, 1);
        List<MissionCard> dummyMissionCard = new List<MissionCard>();
        Faction = new Faction("DefaultFaction",6, 100, 10, dummyMissionCard, charCard);
        dummyMissionCard.Add((MissionCard)CardManager.instance.GetCardByType(CardType.MissionCard, 2));
        dummyMissionCard.Add((MissionCard)CardManager.instance.GetCardByType(CardType.MissionCard, 4));
        dummyMissionCard.Add((MissionCard)CardManager.instance.GetCardByType(CardType.MissionCard, 6));
        dummyMissionCard.Add((MissionCard)CardManager.instance.GetCardByType(CardType.MissionCard, 8));
        dummyMissionCard.Add((MissionCard)CardManager.instance.GetCardByType(CardType.MissionCard, 10));
        dummyMissionCard.Add((MissionCard)CardManager.instance.GetCardByType(CardType.MissionCard, 12));
        for (int i = 0; i < dummyMissionCard.Count; i++)
        {
            AddCard(dummyMissionCard[i]);
        }
    }

    public int GetCount()
    {
        return _cards.Count;
    }
    
    public void AddCard(Card _Card, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            _cards.Add(_Card);            
        }
    }    

    public Card Draw()
    {
        if (_cards.Count < 1)
        {
            Debug.Log("Tried to draw card from an empty deck");
            return null;
        }

        Card c = _cards[0];
        _cards.RemoveAt(0);
        return c;
    }

    public void Shuffle()
    {
        _cards.Shuffle();
    }

    public Deck(string dataStr)
    {
        string[] data = dataStr.Split('#');
        Faction = new Faction(data[0]);

        _cards = new List<Card>();
        string[] cardData = data[1].Split('|');
        int numCards = int.Parse(cardData[0]);

        for (int i = 0; i < numCards; i++)
        {
            Card card = (Card)CardManager.instance.GetCardByType(CardType.MissionCard, int.Parse(cardData[2 + (i * 2)]));
            _cards.Add(card);
        }
    }

    public override string ToString()
    {
        return ToString(false);
    }

    public string ToString(bool anonymize)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(Faction.ToString());
        sb.Append("#");

        sb.Append(_cards.Count);
        sb.Append("|");

        foreach (Card card in _cards)
        {
            sb.Append(anonymize ? "BBB": card.Name);
            sb.Append("|");
            sb.Append(card.ID.ToString());
            sb.Append("|");
        }

        return sb.ToString();
    }
}
