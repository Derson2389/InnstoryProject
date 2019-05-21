using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 诡计卡
/// </summary>
public class CounterCard : Card
{
    //激活效果
    public string ActiveEffect { get; set; }
    //符号
    public string SignTxt { get; set; }
    //符号ID
    public int SignID { get; set; }
    //卡牌数量
    public int OriginalNumb { get; set; }

    public override void Init<T>(T t)
    {
        CounterCardsCfg cfg = t as CounterCardsCfg;

        this.cardType = CardType.CounterCard;
        this.Name = cfg.name;
        this.ID = cfg.ID;

        this.ActiveEffect = cfg.activeEffect;
        this.SignID = cfg.signID;
        this.SignTxt = cfg.signTxt;
        this.OriginalNumb = cfg.orginalNum;
        this.prefabPath = cfg.prefabPath;
    }
}
