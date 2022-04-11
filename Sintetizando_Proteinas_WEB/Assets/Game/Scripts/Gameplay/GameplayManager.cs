using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

//Need a few changes
//Mudar o componente de missão, todo bugado

//Colocar o Pooling
public sealed class GameplayManager : MonoBehaviour{
    [SerializeField] List<Phase> gamePhases;
    [SerializeField] GameObject phaseInstruction;
    
    [System.Serializable]
    private class Phase{
        public PhaseManagerMono manager;
        public GameObject instructions;
        public List<string> messages;
        public List<string> textInstructions = default; //Used in the Marking
        public PhaseDescription pd;
    } 

    [System.Serializable]
    private class PhaseDescription{
        public string namePhase;
        public string descriptionPhase;
        public GameObject additionalInfo;
    }

    private int actualPhase = -1;

    [SerializeField] Marking marking; //Denotates which is the current phase

    private GameObject objRef = null;
    [SerializeField] TextMeshProUGUI totalMessages;
    [SerializeField] TextMeshProUGUI actualMessage;
    [SerializeField] TextMeshProUGUI messageContent;
    private int actualMessageIndex;

    //[SerializeField] List<GameObject> cardsPrefabs;
    //[SerializeField] Transform cardContainer;
    private bool onAwait = false;

    private void Start(){
        //SetRandomCardPosition(); //Not used anymore
        SpawnAllGoals();
        //marking.SpawnGoals(gamePhases.Count);
        IncreacePhase(); //actualPhase always just increace, so starting with -1 is correct
    }

    public void IncreacePhase(){
        actualPhase++;
        DestroyAllInstantiated();

        if(!gamePhases[actualPhase].manager.gameObject.activeSelf){
            objRef = Instantiate<GameObject>(phaseInstruction , this.transform); 
        }else{
            //Pooling
        }
        WaitFor();
    }

    private void SpawnAllGoals(){
        int i;

        for(i = 0; i < gamePhases.Count; i++){
            marking.SpawnGoal(gamePhases[i].textInstructions);
        }
    }

    public bool Check(int numberPhase){
        if(onAwait && numberPhase == actualPhase){
            DestroyAllInstantiated();
            objRef = Instantiate<GameObject>(gamePhases[actualPhase].manager.gameObject , this.transform);
            RestartPhase();
            marking.ShowGoal(actualPhase);
            return true;
        }   

        return false;
    }
    public void SetDescriptionPhase(){
        PhaseDescription pdSetup = gamePhases[actualPhase].pd;
        objRef.GetComponent<MissionDisplay>().Setup(actualPhase, 
            pdSetup.namePhase, pdSetup.descriptionPhase, pdSetup.additionalInfo);
    }


    public void WaitFor(){
        onAwait = true;
    }
    private void RestartPhase(){
        actualMessageIndex = 0;
        totalMessages.text = (gamePhases[actualPhase].messages.Count).ToString();
        ShowText();
        
    }
    private void ShowText(){
        messageContent.text = gamePhases[actualPhase].messages[actualMessageIndex];
        actualMessage.text = (actualMessageIndex + 1).ToString();
    }
    public void IncreaceMessage(int increace){
        if(actualMessageIndex == gamePhases[actualPhase].messages.Count - 1){
            return;
        }

        actualMessageIndex += increace;
        ShowText();
    }
    public void DecreaceMessage(int increace){
        if(actualMessageIndex == 0){
            return;
        }

        actualMessageIndex -= increace;
        ShowText();
    }

    public void DestroyAllInstantiated(){
        if(objRef == null){return;}

        Destroy(objRef);
    }
    
}


    /*
    public void SetRandomCardPosition(){ //Not used anymore
        int[] availablePosition = new int[gamePhases.Count];
        int i;

        Util.SequencialFeed(availablePosition, gamePhases.Count);
        Util.ShuffleArray(availablePosition);
        //Util.PrintVector(availablePosition);

        for(i = 0 ; i < gamePhases.Count - 1; i++){
            cardContainer.GetChild(i).SetSiblingIndex(availablePosition[i]);
        }
    }*/
