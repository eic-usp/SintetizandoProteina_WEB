using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionDisplay : MonoBehaviour{
    [SerializeField] TextMeshProUGUI missionName;
    [SerializeField] TextMeshProUGUI missionDescription;
    [SerializeField] Transform additionalInformation;

    private int numberPhase;


    public void Setup(int numberPhase, string missionName, string missionDescription, GameObject information){
        this.missionName.text = numberPhase + "  " + missionName;
        this.missionDescription.text = missionDescription;
        Instantiate<GameObject>(information, this.additionalInformation);
    }

    public void OnClick(){
        print("Clickou");
        if(FindObjectOfType<GameplayManager>().Check(numberPhase)){
            Destroy(this.gameObject);
        }
    }
}
