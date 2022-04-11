using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionDisplay : WaitManager{
    [SerializeField] string mission;
    [SerializeField] TextMeshProUGUI missionName;
    [SerializeField] TextMeshProUGUI missionDescription;
    [SerializeField] Transform additionalInformation;

    public void Setup(int numberPhase, string missionName, string missionDescription, GameObject information){
        this.missionName.text = mission + " " + (numberPhase + 1) + "  " + missionName;
        this.missionDescription.text = missionDescription;
        SetNumberPhase(numberPhase);

        if(information == null || additionalInformation == null){
            return;
        }

        Instantiate<GameObject>(information, this.additionalInformation);
    }

    public void OnClick(){
        print("Clickou");
        if(WaitCheck()){
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
