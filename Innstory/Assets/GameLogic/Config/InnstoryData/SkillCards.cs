
//-----------------------------------------------
//              生成代码不要修改
//-----------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SkillCardsCfg
{
    public int ID;    //		卡牌ID
    public string name;    //		卡牌名字
    public string cardEffect;    //		效果
    public string activeEffect;    //		激活效果
    public string description;    //		说明

    public void Deserialize (DynamicPacket packet)
    {
        ID = packet.PackReadInt32();
        name = packet.PackReadString();
        cardEffect = packet.PackReadString();
        activeEffect = packet.PackReadString();
        description = packet.PackReadString();
    }
}

public class SkillCardsCfgMgr
{
    private static SkillCardsCfgMgr mInstance;
    
    public static SkillCardsCfgMgr Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new SkillCardsCfgMgr();
            }
            
            return mInstance;
        }
    }

    private Dictionary<int, SkillCardsCfg> mDict = new Dictionary<int, SkillCardsCfg>();
    
    public Dictionary<int, SkillCardsCfg> Dict
    {
        get {return mDict;}
    }

    public void Deserialize (DynamicPacket packet)
    {
        int num = (int)packet.PackReadInt32();
        for (int i = 0; i < num; i++)
        {
            SkillCardsCfg item = new SkillCardsCfg();
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
    
    public SkillCardsCfg GetDataByID(int id)
    {
        if(mDict.ContainsKey(id))
        {
            return mDict[id];
        }
        
        return null;
    }
}
