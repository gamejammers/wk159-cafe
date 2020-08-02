using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonSimulater : MonoBehaviour
{
    public enum Action
    {
        Nothing,
        Travel,
        Treasure,
        Fight,
        Death,
        Complete
    }

    public struct SimulationStepResult
    {
        public Action action;
        public string message;
        public float progress;
    }

    [System.Serializable]
    public struct CharacterSimulationData
    {
        public CharacterData character;
        public float startingTime; // only holds simulatino starting time
        public CharacterSimulationData(CharacterData _character)
        {
            character = _character;
            startingTime = Time.time;
        }
    }
    [SerializeField] SimulateDamage damageSimulator = null;
    [SerializeField] DungeonRewardCalculator rewardCalculator = new DungeonRewardCalculator();
    [SerializeField] DungeonRecords dungeonRecords = null;
    [SerializeField] DungeonFinder dungeonFinder = null;
    [SerializeField] DungeonLog dungeonLog = null;
    [SerializeField] List<Dungeon> availableDungeons = null; // All dungeons player can see and choose
    

    [SerializeField] List<CharacterSimulationData> charactersInSimulation = new List<CharacterSimulationData>();

    public CharacterSimulationData StartDungeonSimulation(CharacterData _character, Dungeon _dungeon) // send a character to dungeon
    {
        _character.currentDungeon = _dungeon;
        _character.lastReactionTime = Time.time; // I didnt want character start dungeon right away and react a little later
        var simdata = new CharacterSimulationData( _character );
        charactersInSimulation.Add(simdata);
        return simdata;
    }

    public SimulationStepResult SimulateDungeon(CharacterSimulationData simdata)
    {
        CharacterData character = simdata.character;

        SimulationStepResult result = new SimulationStepResult();
        result.action = Action.Nothing;

        float targetTime = simdata.startingTime + character.currentDungeon.Duration;
        float elapsed = Time.time - simdata.startingTime;
        result.progress = elapsed / character.currentDungeon.Duration;
        
        if(Time.time >= targetTime) 
        {
            result.action = Action.Complete;
        }
        else if(Time.time >= character.lastReactionTime + character.currentDungeon.ReactionTime)
        {
            character.lastReactionTime = Time.time;

            int move = new System.Random().Next(3); // randomly choose an option 

            if(move == 0) // Safely traveling 
            {
                result.action = Action.Travel;
                result.message = dungeonLog.ChooseRandomLog(0);
            }
            else if( move == 1) // find some treasures
            {
                result.action = Action.Treasure;
                result.message = dungeonLog.ChooseRandomLog(1);
                rewardCalculator.CalculateRewards(character);
            }
            else if( move == 2) // get damage
            {
                result.action = Action.Fight;
                result.message = dungeonLog.ChooseRandomLog(2);
                damageSimulator.CalculateDamage(character);
                
                if(character.currentHealth < 1) // if character dies end simulation
                {
                    result.action = Action.Death;
                    result.message = "PWND";
                }
            }
        }

        return result;
    }
    public void EndDungeonSimulation(CharacterSimulationData simulationData)
    {        
        Debug.Log("simulation ended");
        charactersInSimulation.Remove( simulationData );
        simulationData.character.previousDungeon =  simulationData.character.currentDungeon;

        charactersInSimulation.Remove( simulationData );
        
        SuccessfulnessRate characterRating = RateCharacterAccordingToHealth(simulationData.character.currentHealth, simulationData.character.maxHealth );
        if(characterRating == SuccessfulnessRate.GOOD || characterRating == SuccessfulnessRate.PERFECT)
        {
            dungeonFinder.FindNewDungeon();
        }

        dungeonRecords.AddRecord(new DungeonRecords.Record(
                simulationData.character,
                simulationData.character.currentDungeon,
                characterRating ));
    }
    
    SuccessfulnessRate RateCharacterAccordingToHealth(int health, int startingHealth)
    {
        if( (health / startingHealth) *100 <= 25)
        {
            return SuccessfulnessRate.LOW;
        }
        else if( (health / startingHealth) *100 > 25 && (health / startingHealth) *100 <= 50)
        {
            return SuccessfulnessRate.NORMAL;
        }
        else if( (health / startingHealth) *100 > 50 && (health / startingHealth) *100 <= 75)
        {
            return SuccessfulnessRate.GOOD;
        }
        else if( (health / startingHealth) *100 > 75 && (health / startingHealth) *100 <= 100)
        {
            return SuccessfulnessRate.PERFECT;
        }
        else
            return SuccessfulnessRate.GOOD;
    }

    public List<CharacterData> GetCharactersInDungeons()
    {
        List<CharacterData> characters = new List<CharacterData>();
        for (int i = 0; i < charactersInSimulation.Count; i++)
        {
            characters.Add(charactersInSimulation[i].character);
        }
        return characters;
    }
    public List<Dungeon> GetAvailableDungeons()
    {
        return availableDungeons;
    }
    public void AddNewDungeon(Dungeon d)
    {
        availableDungeons.Add(d);
    }
}
