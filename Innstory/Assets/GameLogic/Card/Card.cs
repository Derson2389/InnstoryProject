using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InnsCardType
{
    CharacterCard,//角色卡
    MissionCard,  //任务卡
    ItemCard,     //道具卡
    CounterCard,  //诡计卡
    SkillCard,    //技能卡

}

public abstract class Card : MonoBehaviour {

    /// <summary>
    /// 卡牌大图
    /// </summary>
    public Image cardArtImage;
    /// <summary>
    /// 卡牌描述信息（效果说明）
    /// </summary>
    public string descInfo;
    /// <summary>
    /// 卡牌类型
    /// </summary>
    public InnsCardType cardType;

    /// <summary>
    /// 战斗阶段能否烧卡标记
    /// </summary>
    public bool CanDestroyInBattle = false;

    /// <summary>
    /// 此卡属于哪个Player
    /// </summary>
    public int PlayerID;


}
