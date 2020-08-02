using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToWanderingSimulation : MonoBehaviour
{
    [SerializeField]
    private List<Wanderer> titleSceneWanderers = null;
    [SerializeField]
    private WandererSimulation wandererSimulation = null;

    private void OnEnable()
    {
        foreach (Wanderer wanderer in titleSceneWanderers)
        {
            wandererSimulation.AddWanderingPoints(wanderer);
            wandererSimulation.AddCharacter(wanderer);
        }
    }
}