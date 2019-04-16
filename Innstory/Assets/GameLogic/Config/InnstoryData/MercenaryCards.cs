
//-----------------------------------------------
//              生成代码不要修改
//-----------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class MercenaryCardsCfg
{
    public int ID;    //		卡牌ID
    public string name;    //		名字
    public string effectdesc;    //		效果说明
    public string activeEffect;    //		激活效果
    public string descriptipon;    //		说明

    public void Deserialize (DynamicPacket packet)
    {
        ID = packet.PackReadInt32();
        name = packet.PackReadString();
        effectdesc = packet.PackReadString();
        activeEffect = packet.PackReadString();
        descriptipon = packet.PackReadString();
    }
}

public class MercenaryCardsCfgMgr
{
    private static MercenaryCardsCfgMgr mInstance;
    
    public static MercenaryCardsCfgMgr Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new MercenaryCardsCfgMgr();
            }
            
            return mInstance;
        }
    }

    private List<MercenaryCardsCfg> mList = new List<MercenaryCardsCfg>();
    
    public List<MercenaryCardsCfg> List
    {
        get {return mList;}
    }

    public void Deserialize (DynamicPacket packet)
    {
        int num = (int)packet.PackReadInt32();
        for (int i = 0; i < num; i++)
        {
            MercenaryCardsCfg item = new MercenaryCardsCfg();
            item.Deserialize(packet);
            mList.Add(item);
        }
    }
}
