using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色卡
/// </summary>
public class CharacterCard : Card , IDamageable{

    //最大生命值
    public int MaxHealth { get; private set; }
    //当前生命值
    public int CurrentHealth { get; private set; }
    //理性值
    public int Reason { get; set; }
    //幸运值
    public int Lucky { get; set; }
    //力量值
    public int Power { get; set; }
    //洞察力
    public int Insight { get; set; }
    //体质值
    public int constitution { get; set; }
    //意志力
    public int determination { get; set; }


    public void DealDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Init<T>(T t)
    {
        CharacterCardsCfg cfg = t as CharacterCardsCfg;

        this.cardType = CardType.CharacterCard;
        this.Name = cfg.name;
        this.ID = cfg.ID;
        this.determination = cfg.determination;
        this.Insight = cfg.insight;
        this.Lucky = cfg.lucky;
        this.Reason = cfg.reason;
        this.constitution = cfg.physique;
        this.MaxHealth = cfg.hp;
        this.Power = cfg.power;
        this.prefabPath = cfg.prefabPath;

    }

}
