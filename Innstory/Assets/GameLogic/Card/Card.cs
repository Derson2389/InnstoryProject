using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Card {

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
    public CardType cardType;

    /// <summary>
    /// 战斗阶段能否烧卡标记
    /// </summary>
    public bool CanDestroyInBattle = false;

    /// <summary>
    /// 此卡属于哪个Player
    /// </summary>
    public int PlayerID;

    /// <summary>
    /// 卡牌ID
    /// </summary>
    public int ID;

    /// <summary> 
    ///当玩家拿起卡片时卡片所处的位置。
    ///用于当玩家进行无效放置时，将牌放回其原始手牌位置
    /// </summary>
    protected Vector3 cardHandPos;
    
    protected bool isDraggable;

    /// <summary>
    /// 卡牌事件名称
    /// </summary>
    public string EffectEventName;
    public string effectEventName
    {
        get { return EffectEventName; }
        set { EffectEventName = value; }
    }

    protected GameObject targetObject = null;

    //If the card is in the graveyard or not
    public bool inGraveyard;
    //If the card is in the summon zone
    public bool inSummonZone;

    public bool doneAddingToGraveyard = false;

      

}
