using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu( menuName = "Cafe/Ingredient")]
public class Ingredient : ScriptableObject
{
  [SerializeField]
  private string ingredientName = System.String.Empty;
  [SerializeField]
  private string description = System.String.Empty;
  [SerializeField]
  private Sprite image = null;
  [SerializeField]
  private List<CharacterModifier> characterModifiers = null;

  public string IngredientName => ingredientName;
  public string Description => description;
  public Sprite Image => image;
  public List<CharacterModifier> CharacterModifiers => characterModifiers;
}
