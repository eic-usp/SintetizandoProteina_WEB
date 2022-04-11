using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour{
    Rigidbody rb;

    void Start(){
        rb = this.GetComponent<Rigidbody>();
        if(rb == null) print("Bizarre");
        ChangeGravityRigidbody(false);
    }
   void VerifyHit(int value){
        FindObjectOfType<BowAndArrowMinigame>().SetHit(value);
   }

   void OnCollisionEnter(Collision other){
       Card3D cardVerify = other.transform.GetComponent<Card3D>();

       print("Collidiu");
       
       if(cardVerify != null){
           print("Carta");
           VerifyHit(cardVerify.GetValue());
           Destroy(this.gameObject);
           Destroy(other.gameObject);
       }else{
           print("???");
       }

        ChangeGravityRigidbody(false);
   }

   public Rigidbody GetRigidbody(){
       return this.rb;
   }

   public void ChangeGravityRigidbody(bool state){
       rb.useGravity = state;
   }
}
