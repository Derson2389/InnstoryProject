
//-----------------------------------------------
//              生成代码不要修改
//-----------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CharacterCardsCfg
{
    public int ID;    //		卡牌ID
    public string name;    //		姓名
    public string title;    //		称号
    public int power;    //		力量
    public int insight;    //		洞察
    public int determination;    //		意志
    public int hp;    //		生命
    public int reason;    //		理性
    public int lucky;    //		幸运

    public void Deserialize (DynamicPacket packet)
    {
        ID = packet.PackReadInt32();
        name = packet.PackReadString();
        title = packet.PackReadString();
        power = packet.PackReadInt32();
        insight = packet.PackReadInt32();
        determination = packet.PackReadInt32();
        hp = packet.PackReadInt32();
        reason = packet.PackReadInt32();
        lucky = packet.PackReadInt32();
    }
}

public class CharacterCardsCfgMgr
{
    private static CharacterCardsCfgMgr mInstance;
    
    public static CharacterCardsCfgMgr Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new CharacterCardsCfgMgr();
            }
            
            return mInstance;
        }
    }

    private Dictionary<int, CharacterCardsCfg> mDict = new Dictionary<int, CharacterCardsCfg>();
    
    public Dictionary<int, CharacterCardsCfg> Dict
    {
        get {return mDict;}
    }

    public void Deserialize (DynamicPacket packet)
    {
        int num = (int)packet.PackReadInt32();
        for (int i = 0; i < num; i++)
        {
            CharacterCardsCfg item = new CharacterCardsCfg();
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
    
    public CharacterCardsCfg GetDataByID(int id)
    {
        if(mDict.ContainsKey(id))
        {
            return mDict[id];
        }
        
        return null;
    }
}