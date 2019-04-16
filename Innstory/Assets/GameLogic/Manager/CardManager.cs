using System;
using System.Collections.Generic;
using UnityEngine;

public class CardManager  
{

    public  List<Card> allSkillCards = new List<Card>();
    public  List<Card> allMissionCards = new List<Card>();
    public  List<Card> allItemCards = new List<Card>();
    public  List<Card> allMercenaryCards = new List<Card>();
    public  List<Card> allCounterCards = new List<Card>();

    public  void  Init()
    {
        string configpath = Application.streamingAssetsPath + "/Config/Config.data";
        ConfigManager.LoadConfig(configpath);

    }

    public void LoadAllMissionCards()
    {
        MissionCardsCfgMgr.Instance.


    }




	
    
}
