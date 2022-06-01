using System;
using System.Collections.Generic;
using UnityEngine;

namespace Learning_ML.Test_2.Scripts
{
    public class TrackCheckpoints : MonoBehaviour
    {


        [SerializeField] private Transform checkpointsTransform;
        [SerializeField] private List<Transform> carTransformList;

        public List<CheckpointSingle> checkpointSingleList;
        public List<int> nextCheckpointSingleIndexList;

        private void Awake() {
        
        }


        public void ShowAllCheckpoints()
        {
            foreach (var VARIABLE in checkpointSingleList)
            {
                VARIABLE.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        private void Start()
        {
            Initialize();
        }

        public void ResetCheckpoint(Transform car)
        {
            nextCheckpointSingleIndexList[carTransformList.IndexOf(car)] = 0;
        }

        public GameObject GetNextCheckpoint(Transform car)
        {
            int nextCheckPointIndex = nextCheckpointSingleIndexList[carTransformList.IndexOf(car)];
            return checkpointSingleList[nextCheckPointIndex].gameObject;
        }
        private void Initialize()
        {
            checkpointSingleList = GameManager.Instance.checkpoints;
            nextCheckpointSingleIndexList = new List<int>();
            foreach (Transform carTransform in carTransformList) {
                nextCheckpointSingleIndexList.Add(0);
            }
        }

        public void CarThroughCheckpoint(CheckpointSingle checkpointSingle, Transform carTransform) {
            int nextCheckpointSingleIndex = nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)];
            
            if (checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex) {

                if (checkpointSingleList.IndexOf(checkpointSingle) == checkpointSingleList.Count - 1)
                {
                    EventManager.PlayerLapFinished(carTransform.gameObject);
                    
                }
                Debug.Log("Correct");
                CheckpointSingle correctCheckpointSingle = checkpointSingleList[nextCheckpointSingleIndex];
               correctCheckpointSingle.Hide();

                nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)]
                    = (nextCheckpointSingleIndex + 1) % checkpointSingleList.Count;
                EventManager.PlayerCorrectCheckpoint(carTransform.gameObject,checkpointSingleList.IndexOf(checkpointSingle));
            } else {
                Debug.Log("Wrong");
                EventManager.PlayerWrongCheckpoint(carTransform.gameObject);

                CheckpointSingle correctCheckpointSingle = checkpointSingleList[nextCheckpointSingleIndex];
               
            }
        }


    }
}
