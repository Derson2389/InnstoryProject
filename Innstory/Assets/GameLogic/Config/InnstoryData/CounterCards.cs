
//-----------------------------------------------
//              生成代码不要修改
//-----------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CounterCardsCfg
{
    public int ID;    //		卡牌ID
    public string name;    //		卡牌名称
    public string activeEffect;    //		激活效果
    public string signTxt;    //		符号
    public int signID;    //		符号ID
    public int orginalNum;    //		卡牌数量

    public void Deserialize (DynamicPacket packet)
    {
        ID = packet.PackReadInt32();
        name = packet.PackReadString();
        activeEffect = packet.PackReadString();
        signTxt = packet.PackReadString();
        signID = packet.PackReadInt32();
        orginalNum = packet.PackReadInt32();
    }
}

public class CounterCardsCfgMgr
{
    private static CounterCardsCfgMgr mInstance;
    
    public static CounterCardsCfgMgr Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new CounterCardsCfgMgr();
            }
            
            return mInstance;
        }
    }

    private List<CounterCardsCfg> mList = new List<CounterCardsCfg>();
    
    public List<CounterCardsCfg> List
    {
        get {return mList;}
    }

    public void Deserialize (DynamicPacket packet)
    {
        int num = (int)packet.PackReadInt32();
        for (int i = 0; i < num; i++)
        {
            CounterCardsCfg item = new CounterCardsCfg();
            item.Deserialize(packet);
            mList.Add(item);
        }
    }
}
