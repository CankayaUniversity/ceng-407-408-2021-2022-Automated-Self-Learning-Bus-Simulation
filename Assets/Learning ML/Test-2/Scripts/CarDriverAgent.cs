using System;
using System.Collections;
using System.Collections.Generic;
using Learning_ML.Test_2.Scripts;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Random = UnityEngine.Random;

using DG.Tweening;
using Unity.Mathematics;

public class CarDriverAgent : Agent
    {
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private TrackCheckpoints trackCheckpoints;
        [SerializeField] private CarDriver carDriver;

        private bool isOnBusStop;
  

 
        public override void OnEpisodeBegin()
        {
            
            transform.position = spawnPosition.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
            transform.forward = spawnPosition.forward;
            carDriver.StopCompletely();
            trackCheckpoints.ResetCheckpoint(transform);
            trackCheckpoints.ShowAllCheckpoints();
        }
        
        public override void CollectObservations(VectorSensor sensor)
        {
            Vector3 checkpointForward = trackCheckpoints.GetNextCheckpoint(transform).transform.forward;
            float directionDot = Vector3.Dot(transform.forward, checkpointForward);
            sensor.AddObservation(directionDot);
        }
        public override void OnActionReceived(ActionBuffers actions)
        {
            if (isOnBusStop)
                return;
            
            float forwardAmount = 0f;
            float turnAmount = 0f;
          
            switch (actions.DiscreteActions[0])
            {
                case 0: forwardAmount = 0f;
                    break;
                case 1: forwardAmount = 1;
                    break;
                case 2: forwardAmount = -1;
                    break;
            }
            switch (actions.DiscreteActions[1])
            {
                case 0: turnAmount = 0f;
                    break;
                case 1: turnAmount = +1;
                    break;
                case 2: turnAmount = -1f;
                    break;
            }
         
            carDriver.SetInputs(forwardAmount,turnAmount);
        }
        
        public override void Heuristic(in ActionBuffers actionsOut)
        {
            int forwardAction = 0;
            if (Input.GetKey(KeyCode.UpArrow)) forwardAction = 1;
            if (Input.GetKey(KeyCode.DownArrow)) forwardAction = 2;
            int turnAction = 0;
            if (Input.GetKey(KeyCode.RightArrow)) turnAction = 1;
            if (Input.GetKey(KeyCode.LeftArrow)) turnAction = 2;

            ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
            discreteActions[0] = forwardAction;
            discreteActions[1] = turnAction;
        }



     

        private void OnCollisionStay(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                Debug.Log("Punished Because Wall");
                AddReward(-0.1f);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                AddReward(-1f);
            
            }

            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("BusStop"))
            {
                isOnBusStop = true;
                Debug.Log("Bus Stop Entered");
                OnBusStop();
                StartCoroutine(DisableBusStop(other.gameObject));
            }
        }

        private IEnumerator DisableBusStop(GameObject busStop)
        {
      
            yield return new WaitForSecondsRealtime(4);
            busStop.gameObject.SetActive(false);
            isOnBusStop = false;
        }

        private void OnBusStop()
        {
            carDriver.forwardAmount = 0;
            carDriver.turnAmount = 0;
           
            // DOTween.To(()=> carDriver.forwardAmount, x=> carDriver.forwardAmount = x, 0, .5f);
            //DOTween.To(()=> carDriver.turnAmount, x=> carDriver.turnAmount = x, 0, .5f);

        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Spawn"))
            {
                Debug.Log("SPAWNDA BEKLEMEYIN");
                AddReward(-0.1f);
            }
        }


        private void Start()
        {
            EventManager.OnPlayerCorrectCheckpoint+= TrackCheckpointsOnOnPlayerCorrectCheckpoint;
            EventManager.OnPlayerWrongCheckpoint += TrackCheckpointsOnOnPlayerWrongCheckpoint;
            EventManager.OnLapFinished += EventManagerOnOnLapFinished;
        }

        private void EventManagerOnOnLapFinished(GameObject car)
        {
            if (car.transform == transform)
            {
                Debug.LogWarning("LAP FINISHED BY"+car);
                AddReward(50f);
                EndEpisode();
            }
        }


        private void TrackCheckpointsOnOnPlayerWrongCheckpoint(GameObject car)
        {
            if (car.transform == transform)
            {
                Debug.Log("Punished Because Wrong Checkpoint");
                AddReward(-1f);
            }
        }

        private void TrackCheckpointsOnOnPlayerCorrectCheckpoint(GameObject car,int checkpointIndex)
        {
            if (car.transform == transform)
            {
                Debug.Log("Rewarded");
                AddReward(1+checkpointIndex);
            }
        }
    }

