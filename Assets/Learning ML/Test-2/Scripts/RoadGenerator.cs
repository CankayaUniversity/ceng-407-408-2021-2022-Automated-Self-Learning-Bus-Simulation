using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
   [SerializeField] private GameObject straightRoad;
   [SerializeField] private GameObject spawnRoad;
   [SerializeField] private GameObject curveRoad;
   [SerializeField] private GameObject rightRoad;
   [SerializeField] private Direction[] roadPath;
   [SerializeField] private static float _roadLength=30;
   private enum Direction {Straight, Left, Right,Spawn,BothWays};

   private void Start()
   {
      GenerateRoad();
   }

   private void GenerateRoad()
   {
      bool isTurned = false;
      Vector3 spawnPosition=Vector3.zero;
      float rotationValue=0;
      Vector3 forwardDirection=Vector3.forward;
      for (var i = 0; i < roadPath.Length; i++)
      {
         GameObject newRoad = null;
         switch (roadPath[i])
         {
            case Direction.Spawn:
               newRoad = Instantiate(spawnRoad, spawnPosition, Quaternion.Euler(new Vector3(0,rotationValue,0)), this.transform);
               break;
            case Direction.Straight:
               newRoad = Instantiate(straightRoad, spawnPosition, Quaternion.Euler(new Vector3(0,rotationValue,0)), this.transform);
               break;
            case Direction.Left:
               rotationValue += -90;
               newRoad = Instantiate(curveRoad, spawnPosition, Quaternion.Euler(new Vector3(0,rotationValue,0)), this.transform);
               forwardDirection = Quaternion.AngleAxis(-90, transform.up) * forwardDirection;
               isTurned = !isTurned;
               break;
            case Direction.Right:
               rotationValue += 90;
               newRoad = Instantiate(rightRoad, spawnPosition, Quaternion.Euler(new Vector3(0,rotationValue+90,0)), this.transform);
               forwardDirection = Quaternion.AngleAxis(90, transform.up) * forwardDirection;
               isTurned = !isTurned;
               break;
            case Direction.BothWays:
               break;
            default:
               throw new ArgumentOutOfRangeException();
         }

         
         
         spawnPosition += forwardDirection* _roadLength;
      }
   }
}
