using System.Collections.Generic;
using System.Text;
using System;

[Serializable()]
public class Faction {

    public string FactionName { get; private set; }
    public int StartingHandSize { get; private set; }
    public int StartingCredits { get; private set; }
    public int ClicksPerTurn { get; private set; }
    public List<MissionCard> MissionCards { get; private set; }
    public CharacterCard Character { get; private set; }

    public Faction(string factionName, int startingHandSize, int startingCredits, int clicksPerTurn, List<MissionCard> _missionCards, CharacterCard character)
    {
        FactionName = factionName;
        StartingHandSize = startingHandSize;
        StartingCredits = startingCredits;
        ClicksPerTurn = clicksPerTurn;
        MissionCards = _missionCards;
        Character = character;
    }

    public Faction(string dataStr)
    {
        string[] data = dataStr.Split('|');
         
        FactionName = data[0];
        StartingHandSize = int.Parse(data[1]);
        StartingCredits = int.Parse(data[2]);
        ClicksPerTurn = int.Parse(data[3]);
        Character = (CharacterCard)CardManager.instance.GetCardByType((CardType)Enum.Parse(typeof(CardType), data[4]), int.Parse( data[5]));
        MissionCards = new List<MissionCard>();
        int numMissionCards = int.Parse(data[6]);
        for (int i = 0; i < numMissionCards; i++)
        {
            MissionCard missionCard = (MissionCard)CardManager.instance.GetCardByType((CardType)Enum.Parse(typeof(CardType), data[7 + (i * 3)]), int.Parse(data[8 + (i * 3)]));
            MissionCards.Add(missionCard);
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(FactionName);
        sb.Append("|");
        sb.Append(StartingHandSize);
        sb.Append("|");
        sb.Append(StartingCredits);
        sb.Append("|");
        sb.Append(ClicksPerTurn);
        sb.Append("|");
        sb.Append(Character.cardType);
        sb.Append("|");
        sb.Append(Character.ID);
        sb.Append("|");
        sb.Append(MissionCards.Count);
        sb.Append("|");

        foreach (var mCard in MissionCards)
        {
            sb.Append(mCard.cardType);
            sb.Append("|");
            sb.Append(mCard.ID);
            sb.Append("|");
            sb.Append(mCard.Name);
            sb.Append("|"); 
        }

        return sb.ToString();
    }
}
