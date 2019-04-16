
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

    private List<MissionCardsCfg> mList = new List<MissionCardsCfg>();
    
    public List<MissionCardsCfg> List
    {
        get {return mList;}
    }

    public void Deserialize (DynamicPacket packet)
    {
        int num = (int)packet.PackReadInt32();
        for (int i = 0; i < num; i++)
        {
            MissionCardsCfg item = new MissionCardsCfg();
            item.Deserialize(packet);
            mList.Add(item);
        }
    }
}
