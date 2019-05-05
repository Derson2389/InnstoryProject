
using System;
using System.Collections.Generic;
using UnityEngine;
using Innstory;

public class CardManager : Singleton<CardManager>
{
    public List<Card>           allSkillCards = new List<Card>();
    public List<MissionCard>    allMissionCards = new List<MissionCard>();
    public List<ItemCard>       allItemCards = new List<ItemCard>();
    public List<MercenaryCard>  allMercenaryCards = new List<MercenaryCard>();
    public List<CounterCard>    allCounterCards = new List<CounterCard>();
    public List<CharacterCard>  allCharCards = new List<CharacterCard>();

    public void Init()  
    {
        string configpath = Application.streamingAssetsPath + "/Config/Config.data";
        ConfigManager.LoadConfig(configpath);
        instance.LoadAllMissionCards();
        instance.LoadAllItemCards();
        instance.LoadAllCounterCards();
        instance.LoadAllMercenaryCards();
        instance.LoadAllCharacterCards();
        instance.LoadAllSkillCards();
    }

    public void LoadAllCharacterCards()
    {
        for (int i = 0; i< CharacterCardsCfgMgr.Instance.List.Count; i++)
        {
            CharacterCard charCard = new CharacterCard();
            var charCardCfg = CharacterCardsCfgMgr.Instance.List[i];
            charCard.cardType = CardType.CharacterCard;
            charCard.Name = charCardCfg.name;
        }

    }

    public Card GetCardByType(CardType _carType, int cardID)
    {
        switch (_carType)
        {
            case CardType.CharacterCard:
                {
                    return allCharCards.Find(c => c.ID == cardID);
                }
                ;
            case CardType.CounterCard:
                {
                    return allCounterCards.Find(c => c.ID == cardID);
                }

            case CardType.ItemCard:
                {
                    return allItemCards.Find(c => c.ID == cardID);
                }
 
            case CardType.MercenaryCard:
                {
                    return allMercenaryCards.Find(c => c.ID == cardID);
                }

            case CardType.MissionCard:
                {
                    return allMissionCards.Find(c => c.ID == cardID);
                }

            case CardType.SkillCard:
                {
                    return allSkillCards.Find(c => c.ID == cardID);
                } 

            default:
                throw new Exception("Invalid card type or card type not found for " + _carType);
        }

    }


    public void LoadAllMissionCards()
    {
        for (int i = 0; i < MissionCardsCfgMgr.Instance.List.Count; i++)
        {
            MissionCard mc = new MissionCard();
            var mcCfg = MissionCardsCfgMgr.Instance.List[i];
            mc.cardType = CardType.MissionCard;
            mc.descInfo = mcCfg.cardEffect;
        }
    }

    public void LoadAllItemCards()
    {
        for (int i = 0; i < ItemCardsCfgMgr.Instance.List.Count; i++)
        {
            ItemCard ic = new ItemCard();
            var icCfg = ItemCardsCfgMgr.Instance.List[i];
            ic.cardType = CardType.ItemCard;
            ic.descInfo = icCfg.cardEffect;

        }
    }

    public void LoadAllMercenaryCards()
    {
        for (int i = 0; i < MercenaryCardsCfgMgr.Instance.List.Count; i++)
        {
            MercenaryCard merCard = new MercenaryCard();
            var merCardCfg = MercenaryCardsCfgMgr.Instance.List[i];
            merCard.cardType = CardType.MercenaryCard;
        }
    }

    public void LoadAllCounterCards()
    {
        for (int i = 0; i < CounterCardsCfgMgr.Instance.List.Count; i++)
        {
            CounterCard ctc = new CounterCard();
            var ctcCfg = CounterCardsCfgMgr.Instance.List[i];
            ctc.cardType = CardType.CounterCard;
        }
    }

    public void LoadAllSkillCards()
    {
        for (int i =0; i< SkillCardsCfgMgr.Instance.List.Count; i++)
        {
            SkillCard sc = new SkillCard();
            var scCfg = SkillCardsCfgMgr.Instance.List[i];
            sc.cardType = CardType.SkillCard;

        }
    }

    public void  CreateCardPrefab(Card card, String prefabName, bool belongsToPlayer)
    {
        Transform transform = GameObject.Instantiate(); ;

    }


}
