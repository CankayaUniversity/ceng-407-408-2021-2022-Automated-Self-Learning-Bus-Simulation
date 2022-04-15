using System.Collections;
using System.Collections.Generic;
using Learning_ML.Test_2.Scripts;
using UnityEngine;

public class TrackCheckpointsUI : MonoBehaviour {

    [SerializeField] private TrackCheckpoints trackCheckpoints;

    private void Start() {
        
        Hide();
    }

    private void TrackCheckpoints_OnPlayerWrongCheckpoint(object sender, System.EventArgs e) {
        Show();
    }

    private void TrackCheckpoints_OnPlayerCorrectCheckpoint(object sender, System.EventArgs e) {
        Hide();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}
