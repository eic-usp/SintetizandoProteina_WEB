using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

//Use Pooling
public class BowAndArrowMinigame : PhaseManagerMono{
    [SerializeField] GameObject bow = default;
    [SerializeField] Vector3 thrownSpeed = default;

    [SerializeField] Arrow arrowPrefab = default;
    [SerializeField] Transform arrowSpawn = default;
    private Arrow reference;

    private int phaseNumber = -1;
    private bool arrowFlying = false;
    private bool endPhase = false;
    
    void Start(){
        reference = this.transform.GetComponentInChildren<Arrow>();
        BeginGame();
    }

    void Setup(int phaseNumber){
        this.phaseNumber = phaseNumber;
    }

    private async void BeginGame(){
        do{
            await ShootingStance();
            await OnDragAiming();
        }while(await WaitForArrow());

        this.gameObject.SetActive(false); //Change to pooling
        //EndPhase();
    }

    private async Task ShootingStance(){
        print("Entered");
        while(!Input.GetButtonDown("Fire1")){
            await Task.Yield();
        }
        print("Exit");
    }

    private async Task OnDragAiming(){
        //play animatiom

        while(Input.GetMouseButton(0)){
            print("While");
            //Rotate object
            await Task.Yield();
        }

        //if(!animation.isPlaying) ShootArow();
        ShootArrow();
    }

    private async Task<bool> WaitForArrow(){
        while(arrowFlying){
            await Task.Yield();
        }
        
        return !endPhase;
    }

    private void ShootArrow(){
        arrowFlying = true;
        
        reference.GetRigidbody().AddForce(thrownSpeed, ForceMode.Impulse);
        reference.ChangeGravityRigidbody(true);
    }

    public bool SetHit(int wantedTarget){
        arrowFlying = false;

        if(wantedTarget == phaseNumber){
            print("ENTROU");
            endPhase = true;
            return true;
        }

        reference = Instantiate<Arrow>(arrowPrefab, arrowSpawn);
        return false;
    }
}
