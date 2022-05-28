using System;
using System.Drawing;
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
    [SerializeField] private GameObject busStop;
    [SerializeField] private GameObject rightIntersection;
    [SerializeField] private GameObject leftIntersection;
    [SerializeField] private GameObject forwardIntersection;
    [SerializeField] private Direction[] roadPath;
    [SerializeField] private static float _roadLength = 30;

    public  Dictionary<Tuple<int, int>, List<GameObject>> hash = new Dictionary<Tuple<int, int>, List<GameObject>>();
    public  int asd = 123;
    private enum Direction { Straight, Left, Right, Spawn, BothWays, BusStop, RightIntersection, LeftIntersection, Forwardintersection };

    

    private void Start()
    {
        GenerateRoad();
    }

    private void GenerateRoad()
    {
        
        bool isTurned = false;
        Vector3 spawnPosition = Vector3.zero;
        float rotationValue = 0;
        List<GameObject> roads = new List<GameObject>();
        Vector3 forwardDirection = Vector3.forward;
        for (var i = 0; i < roadPath.Length; i++)
        {
           
            roads.Clear();
            GameObject newRoad = null;
            switch (roadPath[i])
            {
                case Direction.Spawn:
                    newRoad = Instantiate(spawnRoad, spawnPosition, Quaternion.Euler(new Vector3(0, rotationValue, 0)), this.transform);
                    if (hash.ContainsKey(new Tuple<int,int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))))
                    {
                        
                        newRoad.SetActive(false);
                        hash[new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))].Add(newRoad);
                    }
                    else
                    {


                        hash.Add(new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z)), new List<GameObject>());
                        hash[new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))].Add(newRoad);
                    }

                    break;
                case Direction.Straight:
                    newRoad = Instantiate(straightRoad, spawnPosition, Quaternion.Euler(new Vector3(0, rotationValue, 0)), this.transform);
                    if (hash.ContainsKey(new Tuple<int,int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))))
                    {
                      
                        newRoad.SetActive(false);
                        hash[new Tuple<int,int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))].Add(newRoad);
                    }
                    else
                    {


                        hash.Add(new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z)), new List<GameObject>());
                        hash[new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))].Add(newRoad);
                    }
                    break;
                case Direction.BusStop:
                    newRoad = Instantiate(busStop, spawnPosition, Quaternion.Euler(new Vector3(0, rotationValue, 0)), this.transform);
                    if (hash.ContainsKey(new Tuple<int,int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))))
                    {
                        
                        newRoad.SetActive(false);
                        hash[new Tuple<int,int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))].Add(newRoad);
                    }
                    else
                    {

                        hash.Add(new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z)), new List<GameObject>());
                        hash[new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))].Add(newRoad);

                    }
                    break;
                case Direction.Left:
                    rotationValue += -90;
                    newRoad = Instantiate(curveRoad, spawnPosition, Quaternion.Euler(new Vector3(0, rotationValue, 0)), this.transform);
                    forwardDirection = Quaternion.AngleAxis(-90, transform.up) * forwardDirection;
                    isTurned = !isTurned;
                    if (hash.ContainsKey(new Tuple<int,int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))))
                    {
                     
                        newRoad.SetActive(false);
                        hash[new Tuple<int,int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))].Add(newRoad);
                    }
                    else
                    {

                        hash.Add(new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z)), new List<GameObject>());
                        hash[new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))].Add(newRoad);
                    }
                    break;
                case Direction.Right:
                    rotationValue += 90;
                    newRoad = Instantiate(rightRoad, spawnPosition, Quaternion.Euler(new Vector3(0, rotationValue + 90, 0)), this.transform);
                    forwardDirection = Quaternion.AngleAxis(90, transform.up) * forwardDirection;
                    isTurned = !isTurned;
                    if (hash.ContainsKey(new Tuple<int,int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))))
                    {
                       
                        newRoad.SetActive(false);
                        hash[new Tuple<int,int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))].Add(newRoad);
                    }
                    else
                    {

                        hash.Add(new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z)), new List<GameObject>());
                        hash[new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))].Add(newRoad);
                    }
                    break;
                case Direction.RightIntersection:
                    rotationValue += 90;
                    newRoad = Instantiate(rightIntersection, spawnPosition, Quaternion.Euler(new Vector3(0, rotationValue + 90, 0)), this.transform);
                    forwardDirection = Quaternion.AngleAxis(90, transform.up) * forwardDirection;
                    isTurned = !isTurned;
                    if (hash.ContainsKey(new Tuple<int,int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))))
                    {
                       
                        newRoad.SetActive(false);
                        hash[new Tuple<int,int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))].Add(newRoad);
                    }
                    else
                    {
                       
                        hash.Add(new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z)), new List<GameObject>());
                        hash[new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))].Add(newRoad);
                    }
                    break;

                case Direction.LeftIntersection:
                    rotationValue += -90;
                    newRoad = Instantiate(leftIntersection, spawnPosition, Quaternion.Euler(new Vector3(0, rotationValue, 0)), this.transform);
                    forwardDirection = Quaternion.AngleAxis(-90, transform.up) * forwardDirection;
                    isTurned = !isTurned;
                    if (hash.ContainsKey(new Tuple<int,int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))))
                    {
                        newRoad.SetActive(false);
                        hash[new Tuple<int,int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))].Add(newRoad);
                        
                    }
                    else
                    {
                     
                        hash.Add(new Tuple<int,int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z)), new List<GameObject>());
                        hash[new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))].Add(newRoad);
                 
                    }
                    break;
                case Direction.Forwardintersection:
                    newRoad = Instantiate(forwardIntersection, spawnPosition, Quaternion.Euler(new Vector3(0, rotationValue, 0)), this.transform);
                    if (hash.ContainsKey(new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))))
                    {

                        newRoad.SetActive(false);
                        hash[new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))].Add(newRoad);
                       
                    }
                    else
                    {
                       
                        hash.Add(new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z)), new List<GameObject>());
                        hash[new Tuple<int, int>(Convert.ToInt32(spawnPosition.x), Convert.ToInt32(spawnPosition.z))].Add(newRoad);
                    }
                    break;

                case Direction.BothWays:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();


            }



            spawnPosition += forwardDirection * _roadLength;
        }
    }
}
