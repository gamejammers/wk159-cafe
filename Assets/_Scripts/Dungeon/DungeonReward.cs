using System;
using UnityEngine;
[Serializable]
public class DungeonReward
{
  [SerializeField]
  private Ingredient ingredient = null;
  [SerializeField, Range(0f, 1f)]
  private float dropChance = 0;
  [SerializeField, Range(0, 50)]
  private int minimumAmount = 0;
  [SerializeField, Range(0, 50)]
  private int maximumAmount = 0;

  public Ingredient Ingredient => ingredient;
  public float DropChance => dropChance;
  public int MinimumAmount => minimumAmount;
  public int MaximumAmount => maximumAmount;
}
