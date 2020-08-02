using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour
{
  [SerializeField] private Ingredient ingredient = null;
  [SerializeField] private Image ingredientImage = null;
  [SerializeField] private TextMeshProUGUI text = null;
  [SerializeField] private bool debug = false;

  void OnEnable()
  {
    if (ingredient != null && debug) SetIngredient(ingredient);
  }

  public void SetIngredient(Ingredient _ingredient)
  {
    ingredient = _ingredient;
    ingredientImage.sprite = ingredient.Image;
    text.text = ingredient.Description;
  }

  public void RemoveIngredient()
  {
    ingredient = null;
    ingredientImage.sprite = null;
    text.text = "";
  }
}
