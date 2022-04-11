using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerInfo : MonoBehaviour{
    private string actualProtein;
    private string namePlayer;
    private float maxScore;
    private float lastScore;
    private float actualScore;

    //private float maxTime;
    //private float actualBestTime;
    public static PlayerInfo Instance { get; private set; }

    public PlayerInfo(){

    }
    private void Awake() { 
    // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        }else{ 
            Instance = this; 
        }
    }

    public void SetNamePlayer(string namePlayer){
        this.namePlayer = namePlayer;
        print(this.namePlayer);
    }

    public void SetProteinName(string nameProtein){
        actualProtein = nameProtein;
    }

    public string GetNamePlayer(){
        return namePlayer;
    }

    public string GetActualProtein(){
        return actualProtein;
    }
    
}
