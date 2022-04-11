using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProteinAnim : MonoBehaviour{
    Animator anim;

    int rotationHash = Animator.StringToHash("Rotation");

    void Start(){
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    public void OnMouseEnter(){
        anim.enabled = true;
        //anim.SetTrigger(rotationHash);
    }

    public void OnMouseExit(){
        anim.enabled = false;
    }
}
