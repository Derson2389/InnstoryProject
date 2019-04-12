
//-----------------------------------------------
//              生成代码不要修改
//-----------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class MissionCardsCfg
{
    public int ID;    //		卡牌ID
    public string name;    //		卡牌名字
    public int itemCardNum;    //		装备卡数目
    public int counterCardNum;    //		诡计卡数目
    public int skillCardNum;    //		技能卡数目
    public string cardConditon;    //		达成条件
    public string cardEffect;    //		达成效果

    public void Deserialize (DynamicPacket packet)
    {
        ID = packet.PackReadInt32();
        name = packet.PackReadString();
        itemCardNum = packet.PackReadInt32();
        counterCardNum = packet.PackReadInt32();
        skillCardNum = packet.PackReadInt32();
        cardConditon = packet.PackReadString();
        cardEffect = packet.PackReadString();
    }
}

public class MissionCardsCfgMgr
{
    private static MissionCardsCfgMgr mInstance;
    
    public static MissionCardsCfgMgr Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new MissionCardsCfgMgr();
            }
            
            return mInstance;
        }
    }

    private Dictionary<int, MissionCardsCfg> mDict = new Dictionary<int, MissionCardsCfg>();
    
    public Dictionary<int, MissionCardsCfg> Dict
    {
        get {return mDict;}
    }

    public void Deserialize (DynamicPacket packet)
    {
        int num = (int)packet.PackReadInt32();
        for (int i = 0; i < num; i++)
        {
            MissionCardsCfg item = new MissionCardsCfg();
            item.Deserialize(packet);
            if (mDict.ContainsKey(item.ID))
            {
                mDict[item.ID] = item;
            }
            else
            {
                mDict.Add(item.ID, item);
            }
        }
    }
    
    public MissionCardsCfg GetDataByID(int id)
    {
        if(mDict.ContainsKey(id))
        {
            return mDict[id];
        }
        
        return null;
    }
}