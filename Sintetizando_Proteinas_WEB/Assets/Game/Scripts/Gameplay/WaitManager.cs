using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitManager : MonoBehaviour{
    private int numberPhase;

    protected void SetNumberPhase(int numberPhase){
        this.numberPhase = numberPhase;
    }

    protected bool WaitCheck(){
        return FindObjectOfType<GameplayManager>().Check(numberPhase);
    }
}   
