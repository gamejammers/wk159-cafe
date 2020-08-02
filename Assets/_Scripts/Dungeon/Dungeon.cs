using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Cafe/Dungeon")]
public class Dungeon : ScriptableObject
{
  [SerializeField]
  private float duration                                        = 0f;
  [SerializeField]
  private float reactionTime                                    = 0f;
  [SerializeField]
  private string dungeonName                                    = System.String.Empty;
  [SerializeField]
  private DungeonLevel dungeonLevel                             = default(DungeonLevel);
  [SerializeField]
  private List<DungeonAttribute> dungeonAttributes              = null;
  [SerializeField]
  private List<DungeonReward> rewards                           = null;
  [SerializeField]
  private Sprite icon                                           = null;

  public float Duration => duration;
  public float ReactionTime => reactionTime;
  public string DungeonName => dungeonName;
  public DungeonLevel DungeonLevel => dungeonLevel;
  public List<DungeonAttribute> DungeonAttributes => dungeonAttributes;
  public List<DungeonReward> Rewards => rewards;
  public Sprite Icon => icon;
}
