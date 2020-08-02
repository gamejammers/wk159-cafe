using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wanderer : MonoBehaviour
{
  [SerializeField]
  private NavMeshAgent navmeshAgent = null;
  private List<Transform> cafeWanderingPoints = null;
  private Transform cafeTillPoint = null;
  private Transform cafeEntryExit = null;
  [SerializeField, Range(1f, 5f)]
  private float minimumWanderingRate = 1f;
  [SerializeField, Range(5f, 10f)]
  private float maximumWanderingRate = 5f;

  private float wanderingRate = 0;
  private float lastWanderTime = 0;

  public float WanderingRate => wanderingRate;
  public float LastWanderTime => lastWanderTime;

  void OnEnable()
  {
    cafeWanderingPoints = new List<Transform>();
    wanderingRate = UnityEngine.Random.Range(minimumWanderingRate, maximumWanderingRate);
  }
  void Start()
  {
    RoamCafe();
  }
  public void SetWanderingPoints(Transform _cafeTill, Transform _cafeEntryExit, List<Transform> _cafeWanderPoints)
  {
    cafeTillPoint = _cafeTill;
    cafeEntryExit = _cafeEntryExit;
    cafeWanderingPoints = _cafeWanderPoints;
  }
  public void RoamCafe()
  {
    wanderingRate = UnityEngine.Random.Range(minimumWanderingRate, maximumWanderingRate);
    lastWanderTime = Time.time;
    int maxWanderingPoints = cafeWanderingPoints.Count;
    Vector3 randomWanderingPoint = cafeWanderingPoints[UnityEngine.Random.Range(0, maxWanderingPoints)].position;
    navmeshAgent.SetDestination(randomWanderingPoint);
  }
}