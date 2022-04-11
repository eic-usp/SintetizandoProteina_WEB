using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card3D : MonoBehaviour{
    //Each card has a number attached to the phase
    //This will be handled in the "gameplayManager"
    [SerializeField] int numberPhase;

    public Card3D(){}
    private void OnDestroy() {
        //this.transform.parent.GetComponent<BodyCard>().CheckEnd();
    }

    public int GetValue(){
        return numberPhase;
    }
}
