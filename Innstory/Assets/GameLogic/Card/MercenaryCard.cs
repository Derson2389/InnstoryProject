using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 佣兵卡
/// </summary>
public class MercenaryCard : Card
{
    public string Effectdesc;    //		效果说明
    public override void Init<T>(T t)
    {
        MercenaryCardsCfg cfg = t as MercenaryCardsCfg;

        this.cardType = CardType.MercenaryCard;
        this.Name = cfg.name;
        this.ID = cfg.ID;
        this.EffectEventName = cfg.activeEffect;
        this.Effectdesc = cfg.effectdesc;
        this.descInfo = cfg.descriptipon;
        this.prefabPath = cfg.prefabPath;
    }
}
