using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisualsController : MonoBehaviour
{
  private const int ORIGINAL_SORTORDER = 1, SELECTED_SORTORDER = 10;

  [SerializeField]
  private Image background, ingredient, ingredientBackground;
  [SerializeField]
  private TextMeshProUGUI descriptionText;

  public void BringToFront()
  {

  }

  public void ResetSortOrder()
  {

  }
}