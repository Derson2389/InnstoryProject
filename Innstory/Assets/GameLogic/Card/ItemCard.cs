using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 道具卡
/// </summary>
public class ItemCard : Card
{
    //效果
    public string cardEffect { get; set; }   //		效果
    //激活效果
    public string activeEffect { get; set; } //		激活效果
    //耐久力
    public int Durability { get; set; }

    public override void Init<T>(T t)
    {
        ItemCardsCfg cfg = t as ItemCardsCfg;

        this.cardType = CardType.ItemCard;
        this.Name = cfg.name;
        this.ID = cfg.ID;
        this.Durability = cfg.Durability;
        this.descInfo = cfg.description;
        this.cardEffect = cfg.cardEffect;
        this.activeEffect = cfg.activeEffect;

    }
}
