using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimulateDamage : MonoBehaviour
{
    [Header("Easy Settings")]
    [SerializeField] int minEasyDungeonBaseDamage               = 0;
    [SerializeField] int maxEasyDungeonBaseDamage               = 0;

    [Header("Medium Settings")]
    [SerializeField] int minMediumDungeonBaseDamage             = 0;
    [SerializeField] int maxMediumDungeonBaseDamage             = 0;

    [Header("Hard Settings")]
    [SerializeField] int minHardDungeonBaseDamage               = 0;
    [SerializeField] int maxHardDungeonBaseDamage               = 0;

    [Header("BrutalSettings")]
    [SerializeField] int minBrutalDungeonBaseDamage             = 0;
    [SerializeField] int maxBrutalDungeonBaseDamage             = 0;

    public void CalculateDamage(CharacterData _character)
    {
        int damage = 0;
        if(_character.currentDungeon.DungeonLevel == DungeonLevel.EASY)
        {
            damage += Random.Range(minEasyDungeonBaseDamage, maxEasyDungeonBaseDamage+1);
        }
        else if(_character.currentDungeon.DungeonLevel == DungeonLevel.MEDIUM)
        {
            damage += Random.Range(minMediumDungeonBaseDamage, maxMediumDungeonBaseDamage+1);
        }
        else if(_character.currentDungeon.DungeonLevel == DungeonLevel.HARD)
        {
            damage += Random.Range(minHardDungeonBaseDamage, maxHardDungeonBaseDamage+1);
        }
        else if(_character.currentDungeon.DungeonLevel == DungeonLevel.BRUTAL)
        {
            damage += Random.Range(minBrutalDungeonBaseDamage, maxBrutalDungeonBaseDamage+1);
        }

        // if dungeon has 2 attribute (fire, impact) and base damage is 10, it will deal 5 fire and 5 impact damage
        int seperatedDamage = damage / _character.currentDungeon.DungeonAttributes.Count; 
        foreach (var item in _character.currentDungeon.DungeonAttributes)
        {
            if(item == DungeonAttribute.FIRE)
            {
                seperatedDamage += (seperatedDamage * 20) / 100; // fire will give additional 20% damage
                seperatedDamage -= _character.heatResistance;
                Debug.Log("heat damage" + seperatedDamage);
            }
            else if(item == DungeonAttribute.ICE)
            {
                seperatedDamage -= _character.coldResistance;
                Debug.Log("ice damage" + seperatedDamage);
            }
            else if(item == DungeonAttribute.POISON)
            {
                seperatedDamage -= _character.poisonResistance;
                Debug.Log("poison damage" + seperatedDamage);
            }
            else if(item == DungeonAttribute.SHOCK)
            {
                seperatedDamage -= _character.shockResistance;
                Debug.Log("shock damage" + seperatedDamage);
            }
            else if(item == DungeonAttribute.PIERCING)
            {
                seperatedDamage -= _character.piercingResistance;
                seperatedDamage -= _character.armor/3;
                Debug.Log("piercing damage" + seperatedDamage);
            }
            else if(item == DungeonAttribute.IMPACT)
            {
                seperatedDamage -= _character.impactResistance;
                seperatedDamage -= _character.armor/3;
                Debug.Log("impact damage" + seperatedDamage);
            }
            // give damage to character
            if(seperatedDamage > 0 )
            {
                _character.UpdateStat(CharacterModificationEnum.CURRENT_HEALTH, -seperatedDamage);
            }
            Debug.Log("you got " + seperatedDamage + "damage");
        }
    }
}
