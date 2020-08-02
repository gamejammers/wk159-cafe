using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  [SerializeField]
  private int hoverAmount = 2;

  ///###hsmith $TODO unused/
  //[SerializeField]
  //private LayoutElement layout = null;

  [SerializeField]
  private Transform Visuals = null;

  //###hsmith $TODO unused
  //[SerializeField]
  //private BoxCollider2D physicsCollider = null;

  private Vector3 onEnterPosition;

  public void OnPointerEnter(PointerEventData eventData)
  {
    onEnterPosition = Visuals.position;
    Visuals.position += Vector3.up * hoverAmount;
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    Visuals.position = onEnterPosition;
  }
}
