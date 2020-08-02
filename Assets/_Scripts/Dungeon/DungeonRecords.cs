using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRecords : MonoBehaviour
{
    [System.Serializable]
    public struct Record
    {
        public CharacterData character;
        public Dungeon dungeon;
        public SuccessfulnessRate successfulness;
        public Record(CharacterData _character, Dungeon _dungeon, SuccessfulnessRate _successfulness)
        {
            character = _character;
            dungeon = _dungeon;
            successfulness = _successfulness;
        }
    }
    [SerializeField] List<Record> records;
    void Start()
    {
        records = new List<Record>();
    }
    public void AddRecord(Record r)
    {
        records.Add(r);
    }
    public SuccessfulnessRate GetAvarageSuccessfulnessRate()
    {
        int point = 0;
        int maxPoint = 0;
        for (int i = records.Count-1; ( i >= 0 && i >= records.Count-6 ); i--)
        {
            maxPoint += 7;
            if(records[i].dungeon.DungeonLevel == DungeonLevel.EASY)
            {
                point += GivePointForSuccessfulnessRate(records[i]);
                point += 1;
            }   
            else if(records[i].dungeon.DungeonLevel == DungeonLevel.MEDIUM)
            {
                point += GivePointForSuccessfulnessRate(records[i]);
                point += 2;
            }
            else if(records[i].dungeon.DungeonLevel == DungeonLevel.HARD)
            {
                point += GivePointForSuccessfulnessRate(records[i]);
                point += 3;
            }
        }
        if( (point / maxPoint) *100 <= 25)
        {
            return SuccessfulnessRate.LOW;
        }
        else if( (point / maxPoint) *100 > 25 && (point / maxPoint) *100 <= 50)
        {
            return SuccessfulnessRate.NORMAL;
        }
        else if( (point / maxPoint) *100 > 50 && (point / maxPoint) *100 <= 75)
        {
            return SuccessfulnessRate.GOOD;
        }
        else if( (point / maxPoint) *100 > 75 && (point / maxPoint) *100 <= 100)
        {
            return SuccessfulnessRate.PERFECT;
        }
        else
            return SuccessfulnessRate.GOOD;
    }
    int GivePointForSuccessfulnessRate(Record r)
    {
        int point = 0;
        if( r.successfulness == SuccessfulnessRate.LOW )
        {
            point += 1;
        }
        else if( r.successfulness == SuccessfulnessRate.NORMAL )
        {
            point += 2;
        }
        else if( r.successfulness == SuccessfulnessRate.GOOD )
        {
            point += 3;
        }
        else if( r.successfulness == SuccessfulnessRate.PERFECT )
        {
            point += 4;
        }
        return point;
    }
    public List<Record> GetRecords()
    {
        return records;
    }
}
public enum SuccessfulnessRate{
    LOW, NORMAL, GOOD, PERFECT
}