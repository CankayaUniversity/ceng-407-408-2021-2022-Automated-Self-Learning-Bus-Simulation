using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System;
using System.Drawing;




public class Leave : MonoBehaviour
{
    public RoadGenerator road;
    
    void Start()
    {
        road = FindObjectOfType<RoadGenerator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject parentPrefab = this.transform.parent.gameObject;
            

            GameObject parentObj = this.transform.parent.gameObject;
            if (road.hash.ContainsKey(new Tuple<int, int>(Convert.ToInt32(parentObj.transform.position.x), Convert.ToInt32(parentObj.transform.position.z)))
                && road.hash[new Tuple<int, int>(Convert.ToInt32(parentObj.transform.position.x), Convert.ToInt32(parentObj.transform.position.z))].Count>1)
            {
                Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                road.hash[new Tuple<int, int>(Convert.ToInt32(parentObj.transform.position.x), Convert.ToInt32(parentObj.transform.position.z))][1].SetActive(true);
                road.hash[new Tuple<int, int>(Convert.ToInt32(parentObj.transform.position.x), Convert.ToInt32(parentObj.transform.position.z))][0].SetActive(false);

                road.hash[new Tuple<int, int>(Convert.ToInt32(parentObj.transform.position.x), Convert.ToInt32(parentObj.transform.position.z))].Add(road.hash[new Tuple<int, int>(Convert.ToInt32(parentObj.transform.position.x), Convert.ToInt32(parentObj.transform.position.z))][0]);
                road.hash[new Tuple<int, int>(Convert.ToInt32(parentObj.transform.position.x), Convert.ToInt32(parentObj.transform.position.z))].RemoveAt(0);
            }
        }
    }
}