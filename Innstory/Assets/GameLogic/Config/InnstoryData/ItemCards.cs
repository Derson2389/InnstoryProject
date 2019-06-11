
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
    public int orginalNum;    //		卡牌数量
    public string description;    //		卡牌说明
    public string prefabPath;    //		预制体

    public void Deserialize (DynamicPacket packet)
    {
        ID = packet.PackReadInt32();
        name = packet.PackReadString();
        cardEffect = packet.PackReadString();
        activeEffect = packet.PackReadString();
        Durability = packet.PackReadInt32();
        orginalNum = packet.PackReadInt32();
        description = packet.PackReadString();
        prefabPath = packet.PackReadString();
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

    private List<ItemCardsCfg> mList = new List<ItemCardsCfg>();
    
    public List<ItemCardsCfg> List
    {
        get {return mList;}
    }

    public void Deserialize (DynamicPacket packet)
    {
        int num = (int)packet.PackReadInt32();
        for (int i = 0; i < num; i++)
        {
            ItemCardsCfg item = new ItemCardsCfg();
            item.Deserialize(packet);
            mList.Add(item);
        }
    }
}
