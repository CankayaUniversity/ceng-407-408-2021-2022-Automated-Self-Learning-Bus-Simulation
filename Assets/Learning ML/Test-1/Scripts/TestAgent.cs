using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;


public class TestAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMR;
    [SerializeField] private Transform topLeftCorner, bottomRightCorner;
    public override void OnActionReceived(ActionBuffers actions)
    {
        var moveX = actions.ContinuousActions[0];
        var moveZ = actions.ContinuousActions[1];

        float moveSpeed = 5;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("x");
        var randomPos = new Vector3(Random.Range(topLeftCorner.localPosition.x, bottomRightCorner.localPosition.x), 0,
            Random.Range(bottomRightCorner.localPosition.z, topLeftCorner.localPosition.z));
        transform.localPosition = randomPos;
        var randomTargetPos = new Vector3(Random.Range(topLeftCorner.localPosition.x, bottomRightCorner.localPosition.x), 0,
            Random.Range(bottomRightCorner.localPosition.z, topLeftCorner.localPosition.z));
        targetTransform.localPosition = randomTargetPos;

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        //AddReward() for multiple goal rewards
        if (other.CompareTag("Target"))
        {
            SetReward(1f);
            EndEpisode();
            floorMR.sharedMaterial = winMaterial;
        }else if (other.CompareTag("Wall"))
        {
            SetReward(-1f);
            EndEpisode();
            floorMR.sharedMaterial = loseMaterial;
        }
    }
}
