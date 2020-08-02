using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererSimulation : MonoBehaviour
{
    private List<Wanderer> characters;
    [SerializeField]
    private Transform cafeTillPoint = null;
    [SerializeField]
    private Transform cafeEntryExit = null;
    [SerializeField]
    private List<Transform> cafeWanderingPoints = null;

    private void OnEnable() => characters = new List<Wanderer>();

    void Update()
  {
    if (characters == null || characters.Count <= 0) return;
    foreach (Wanderer character in characters)
    {
      if (!(Time.time >= (character.LastWanderTime + character.WanderingRate))) return;
      character.RoamCafe();
    }
  }

  public void AddCharacter(Wanderer _character) =>  characters.Add(_character);

    public void AddWanderingPoints(Wanderer _wanderer) => _wanderer.SetWanderingPoints(cafeTillPoint, cafeEntryExit, cafeWanderingPoints);
  public void RemoveCharacter(Wanderer _character) => characters.Remove(_character);
}