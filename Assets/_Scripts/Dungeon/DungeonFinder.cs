using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DungeonFinder : MonoBehaviour
{
    [SerializeField] DungeonSimulater dungeonSimulater = null;
    [SerializeField] List<Dungeon> allDungeons = new List<Dungeon>();
    [SerializeField] float newDungeonFindingChance = 15;

    public void FindNewDungeon()
    {
        if(Random.Range(0,101) > newDungeonFindingChance ) return; // chance to find

        Dungeon[] findableDungeons = allDungeons.Except(dungeonSimulater.GetAvailableDungeons()).ToArray();

        if(findableDungeons.Length > 0)
        {
            dungeonSimulater.AddNewDungeon(findableDungeons[ Random.Range(0,findableDungeons.Length) ]);
            Debug.Log("You found new dungeon");
        }
    }
    
}
