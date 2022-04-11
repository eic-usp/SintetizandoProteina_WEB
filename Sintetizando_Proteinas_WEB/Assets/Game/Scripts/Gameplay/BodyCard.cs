using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCard : MonoBehaviour{
    [SerializeField] ContentCollapsable contentCollapsable;
    public void CheckEnd(){
        if(this.transform.childCount == 1){
            contentCollapsable.ContentOmission();
        }
    }

}
