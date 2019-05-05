using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色卡
/// </summary>
public class CharacterCard : Card , IDamageable{


    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    public void DealDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}
