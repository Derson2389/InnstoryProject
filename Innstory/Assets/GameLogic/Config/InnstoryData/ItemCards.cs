
//-----------------------------------------------
//              生成代码不要修改
//-----------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ItemCardsCfg
{
    public int ID;    //		卡牌ID
    public string name;    //		卡牌名称
    public string cardEffect;    //		效果
    public string activeEffect;    //		激活效果
    public int Durability;    //		耐久
    public string description;    //		卡牌说明

    public void Deserialize (DynamicPacket packet)
    {
        ID = packet.PackReadInt32();
        name = packet.PackReadString();
        cardEffect = packet.PackReadString();
        activeEffect = packet.PackReadString();
        Durability = packet.PackReadInt32();
        description = packet.PackReadString();
    }
}

public class ItemCardsCfgMgr
{
    private static ItemCardsCfgMgr mInstance;
    
    public static ItemCardsCfgMgr Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new ItemCardsCfgMgr();
            }
            
            return mInstance;
        }
    }

    private Dictionary<int, ItemCardsCfg> mDict = new Dictionary<int, ItemCardsCfg>();
    
    public Dictionary<int, ItemCardsCfg> Dict
    {
        get {return mDict;}
    }

    public void Deserialize (DynamicPacket packet)
    {
        int num = (int)packet.PackReadInt32();
        for (int i = 0; i < num; i++)
        {
            ItemCardsCfg item = new ItemCardsCfg();
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
    
    public ItemCardsCfg GetDataByID(int id)
    {
        if(mDict.ContainsKey(id))
        {
            return mDict[id];
        }
        
        return null;
    }
}
