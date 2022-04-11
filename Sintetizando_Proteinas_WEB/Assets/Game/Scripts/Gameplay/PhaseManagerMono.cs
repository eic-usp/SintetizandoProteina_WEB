using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManagerMono : MonoBehaviour , PhaseManager{
    public void EndPhase(){
        FindObjectOfType<GameplayManager>().IncreacePhase();
    }
}
