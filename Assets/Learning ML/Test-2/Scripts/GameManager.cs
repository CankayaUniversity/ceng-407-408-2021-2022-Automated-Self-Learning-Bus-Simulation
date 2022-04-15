using System;
using System.Collections;
using System.Collections.Generic;
using Learning_ML.Test_2.Scripts;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform checkpointsParent;
    public List<CheckpointSingle> checkpoints;
    public TrackCheckpoints trackCheckpoints;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        checkpoints = new List<CheckpointSingle>();
    }
}
