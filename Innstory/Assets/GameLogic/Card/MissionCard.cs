using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 任务卡
/// </summary>
public class MissionCard : Card
{
    //装备卡发牌数目
    public int ItemCardNum { get; private set; }
    //诡计卡数目
    public int CounterCardNum { get; set; }
    //技能卡数目
    public int SkillCardNum { get; set; }
    //任务达成条件
    public string CardConditon { get; set; }
    //任务达成效果
    public string CardEffect { get; set; }

    public override void Init<T>(T t)
    {
        MissionCardsCfg cfg = t as MissionCardsCfg;

        this.cardType = CardType.MissionCard;
        this.Name = cfg.name;
        this.ID = cfg.ID;
        this.CounterCardNum = cfg.counterCardNum;
        this.SkillCardNum = cfg.skillCardNum;
        this.ItemCardNum = cfg.itemCardNum;
        this.CardConditon = cfg.cardConditon;
        this.CardEffect = cfg.cardEffect;
        this.EffectEventName = cfg.cardEffect;
    }
}
