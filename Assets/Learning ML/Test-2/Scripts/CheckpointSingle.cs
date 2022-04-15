using System.Collections;
using System.Collections.Generic;
using Learning_ML.Test_2.Scripts;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour {
    
    private MeshRenderer meshRenderer;

    private void Awake() {
        transform.parent = GameManager.Instance.checkpointsParent;
        GameManager.Instance.checkpoints.Add(this.GetComponent<CheckpointSingle>());
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            GameManager.Instance.trackCheckpoints.CarThroughCheckpoint(this, other.transform);
        }
    }

    public void Hide()
    {
        meshRenderer.enabled = false;
    }
    

}
