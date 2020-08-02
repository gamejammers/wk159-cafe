using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonRewardCalculator 
{
    struct RewardChanceData
    {
        public DungeonReward reward;
        public float startChance, endChance;
        public RewardChanceData(DungeonReward _reward, float _startChance, float _endChance)
        {
            reward = _reward;
            startChance = _startChance;
            endChance = _endChance;
        }
    }
    public void CalculateRewards(CharacterData _character)
    {
        List<RewardChanceData> datas = new List<RewardChanceData>();
        float totalChance = 0;
        foreach (var item in _character.currentDungeon.Rewards)
        {
            datas.Add( new RewardChanceData(item, totalChance, totalChance + item.DropChance));
            totalChance += item.DropChance;
        }
        float chance = Random.Range(0,totalChance);

        foreach (var item in datas)
        {
            if(chance >= item.startChance && chance < item.endChance)// this is the selected item
            {
                int amount = Random.Range(item.reward.MinimumAmount, item.reward.MaximumAmount);
                if(_character.ingredients.Count > 0)
                {
                    if( _character.ingredients.Any(s => s.ingredient == item.reward.Ingredient) ) // if there is same ingredient with player add amount
                    {
                        _character.ingredients.First(s => s.ingredient == item.reward.Ingredient ).AddIngredients(amount);
                        break;
                    }
                    else // if character dont exist this type of ingredint then add new 
                    {
                        _character.ingredients.Add( new CharacterIngredients(item.reward.Ingredient, amount));
                        break;
                    }
                }
                else // if character dont exist this type of ingredint then add new 
                {
                    _character.ingredients.Add( new CharacterIngredients(item.reward.Ingredient, amount));
                    break;
                }
            }
        }
    }
}
