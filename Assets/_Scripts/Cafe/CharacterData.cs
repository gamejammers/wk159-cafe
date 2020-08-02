using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterData
{
  [Header("Stats")]
  public string characterName = System.String.Empty;
  public int currentHealth = 0;
  public int maxHealth = 0;
  public int armor = 0; 
  public int currentEnergy = 0;
  public int maxEnergy = 0; 

  [Header("Survival")]
  public int hunger = 0;
  public int thirst = 0;

  [Header("Resistances")]
  public int heatResistance = 0;
  public int coldResistance = 0;
  public int poisonResistance = 0;
  public int shockResistance = 0;
  public int impactResistance = 0;
  public int piercingResistance = 0;

  [Header("Dungeon")]
  public Dungeon previousDungeon = null;
  public Dungeon currentDungeon = null;

  [Header("Rewards")]
  public List<CharacterIngredients> ingredients = new List<CharacterIngredients>();

  [Header("Runtime")]
  public float lastReactionTime = 0f;
      
  public CharacterData(int _initialHealth, int _initialArmor, int _initialEnergy, string _name)
  {
    maxHealth = _initialHealth;
    currentHealth = maxHealth;
    characterName = _name;

    ingredients = new List<CharacterIngredients>();

    armor = _initialArmor;

    maxEnergy = _initialEnergy;
    currentEnergy = maxEnergy;
    
    hunger = 0;
    thirst = 0;
  }

  public void UpdateStat(CharacterModificationEnum _statToMod, int _modAmount)
  {
    switch (_statToMod)
    {
      case CharacterModificationEnum.CURRENT_HEALTH:
      // Current health cannot go beyond maximum health
      Cafe.Dbg.Log("currentHealth {0} , modAmount {1}", currentHealth, _modAmount);
      currentHealth = Mathf.Min(currentHealth + _modAmount, maxHealth);
      Cafe.Dbg.Log("result {0} ", currentHealth );
      break;
      case CharacterModificationEnum.MAX_HEALTH:
      // Max health cannot go below current health
      maxHealth += Mathf.Min(maxHealth + _modAmount, currentHealth);
      break;
      case CharacterModificationEnum.CURRENT_ENERGY:
      // Current energy cannot go beyond maximum energy
      currentEnergy = Mathf.Min(currentEnergy + _modAmount, maxEnergy);
      break;
      case CharacterModificationEnum.MAX_ENERGY:
      // Max energy cannot go below current energy
      maxEnergy += Mathf.Min(maxEnergy + _modAmount, currentEnergy);
      break;
      case CharacterModificationEnum.ARMOR:
      armor += _modAmount;
      break;
      case CharacterModificationEnum.HUNGER:
      hunger += _modAmount;
      break;
      case CharacterModificationEnum.THIRST:
      thirst += _modAmount;
      break;
      case CharacterModificationEnum.HEAT_RESISTANCE:
      heatResistance += _modAmount;
      break;
      case CharacterModificationEnum.COLD_RESISTANCE:
      coldResistance += _modAmount;
      break;
      case CharacterModificationEnum.POISON_RESISTANCE:
      poisonResistance += _modAmount;
      break;
      case CharacterModificationEnum.SHOCK_RESISTANCE:
      shockResistance += _modAmount;
      break;
      case CharacterModificationEnum.IMPACT_RESISTANCE:
      impactResistance += _modAmount;
      break;
      case CharacterModificationEnum.PIERCING_RESISTANCE:
      piercingResistance += _modAmount;
      break;
      default:
      break;
    }
  }
}
