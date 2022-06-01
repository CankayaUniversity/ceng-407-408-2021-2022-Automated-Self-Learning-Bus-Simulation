using UnityEngine;
using UnityEngine.Events;

namespace Learning_ML.Test_2.Scripts
{
    public static class EventManager 
    {
        public static event UnityAction<GameObject,int> OnPlayerCorrectCheckpoint;
        public static event UnityAction<GameObject> OnPlayerWrongCheckpoint;
        public static event UnityAction<GameObject> OnLapFinished;
        public static void PlayerCorrectCheckpoint(GameObject car,int index) => OnPlayerCorrectCheckpoint?.Invoke(car,index);
        public static void PlayerWrongCheckpoint(GameObject car) => OnPlayerWrongCheckpoint?.Invoke(car); 
        public static void PlayerLapFinished(GameObject car) => OnLapFinished?.Invoke(car);
    
    
    }
}
