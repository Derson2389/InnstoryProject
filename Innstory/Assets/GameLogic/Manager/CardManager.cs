
using System;
using System.Collections.Generic;
using UnityEngine;
using Innstory;
using Object = UnityEngine.Object;
using UnityEngine.UI;

public class CardManager : Singleton<CardManager>
{
    public List<SkillCard>      allSkillCards = new List<SkillCard>();
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
            charCard.Init<CharacterCardsCfg>(charCardCfg);
            allCharCards.Add(charCard);
        }

    }

    public List<Card> GetEnvCardsType2Pool(CardType _carType)
    {
        List<Card> envList = new List<Card>();
        switch (_carType)
        {                
            case CardType.CounterCard:
                {
               
                    for (int i = 0; i< allCounterCards.Count; i++)
                    {
                        var cd = allCounterCards[i];
                        for (int a = 0; a < cd.OriginalNumb; a++)
                        {
                            envList.Add(cd);
                        }
                    }
                    return envList;
                }

            case CardType.ItemCard:
                {

                    for (int i = 0; i < allItemCards.Count; i++)
                    {
                        var cd = allItemCards[i];
                        for (int a = 0; a < cd.OriginalNumb; a++)
                        {
                            envList.Add(cd);
                        }
                    }
                    return envList;
                }
            case CardType.SkillCard:
                {
                    for (int i = 0; i < allSkillCards.Count; i++)
                    {
                        var cd = allSkillCards[i];
                        for (int a = 0; a < cd.OriginalNumb; a++)
                        {
                            envList.Add(cd);
                        }
                    }
                    return envList;
                }

            default:
                throw new Exception("Invalid card type or card type not found for " + _carType);
        }

    }

    public Card GetCardByType(CardType _carType, int cardID)
    {
        switch (_carType)
        {
            case CardType.CharacterCard:
                {
                    return instance.allCharCards.Find(c => c.ID == cardID);
                }
                ;
            case CardType.CounterCard:
                {
                    return instance.allCounterCards.Find(c => c.ID == cardID);
                }

            case CardType.ItemCard:
                {
                    return instance.allItemCards.Find(c => c.ID == cardID);
                }
 
            case CardType.MercenaryCard:
                {
                    return instance.allMercenaryCards.Find(c => c.ID == cardID);
                }

            case CardType.MissionCard:
                {
                    return instance.allMissionCards.Find(c => c.ID == cardID);
                }

            case CardType.SkillCard:
                {
                    return instance.allSkillCards.Find(c => c.ID == cardID);
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
            mc.Init<MissionCardsCfg>(mcCfg);
            allMissionCards.Add(mc);
        }
    }

    public void LoadAllItemCards()
    {
        for (int i = 0; i < ItemCardsCfgMgr.Instance.List.Count; i++)
        {
            ItemCard ic = new ItemCard();
            var icCfg = ItemCardsCfgMgr.Instance.List[i];
            ic.Init<ItemCardsCfg>(icCfg);
            allItemCards.Add(ic);
        }
    }

    public void LoadAllMercenaryCards()
    {
        for (int i = 0; i < MercenaryCardsCfgMgr.Instance.List.Count; i++)
        {
            MercenaryCard merCard = new MercenaryCard();
            var merCardCfg = MercenaryCardsCfgMgr.Instance.List[i];
            merCard.Init<MercenaryCardsCfg>(merCardCfg);
            allMercenaryCards.Add(merCard);
        }
    }

    public void LoadAllCounterCards()
    {
        for (int i = 0; i < CounterCardsCfgMgr.Instance.List.Count; i++)
        {
            CounterCard ctc = new CounterCard();
            var ctcCfg = CounterCardsCfgMgr.Instance.List[i];
            ctc.Init<CounterCardsCfg>(ctcCfg);

            allCounterCards.Add(ctc);
        }
    }

    public void LoadAllSkillCards()
    {
        for (int i =0; i< SkillCardsCfgMgr.Instance.List.Count; i++)
        {
            SkillCard sc = new SkillCard();
            var scCfg = SkillCardsCfgMgr.Instance.List[i];
            sc.Init<SkillCardsCfg>(scCfg);
            allSkillCards.Add(sc);
        }
    }

    public Transform CreateCardPrefab(Card card, String prefabName, bool belongsToPlayer)
    {

        ///Debug.LogError("Card Prefab :" + prefabName);
        GameObject go = (GameObject)Resources.Load(prefabName);
        Transform transform = Object.Instantiate(go.transform);

        // link the prefab to its "model" card
        var cardLink = transform.GetComponent<CardBehaviour>();
        cardLink.Card = card;

        var name = (Text)transform.Find("Name").GetComponent(typeof(Text));
        name.text = card.Name;

        var cardText = (Text)transform.Find("CardText").GetComponent(typeof(Text));
        cardText.text = card.descInfo;


        return transform;

    }

}
