using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能卡
/// </summary>
public class SkillCard : Card
{
    public string cardEffect;    //		效果
    public string activeEffect;    //		激活效果

    public override void Init<T>(T t)
    {
        SkillCardsCfg cfg = t as SkillCardsCfg;

        this.cardType = CardType.SkillCard;
        this.Name = cfg.name;
        this.ID = cfg.ID;
        this.cardEffect = cfg.cardEffect;
        this.activeEffect = cfg.activeEffect;
        this.descInfo = cfg.description;
    }
}
