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
    [SerializeField] TextMeshProUGUI totalMessages; //Information in the display
    [SerializeField] TextMeshProUGUI actualMessage;
    [SerializeField] TextMeshProUGUI messageContent;
    private int actualMessageIndex;

    [SerializeField] GameObject waitManager; //Appear between phases, phaseInstruction basically
    private bool onAwait = false;

    private void Start(){
        //SetRandomCardPosition(); //Not used anymore
        SpawnAllGoals();
        IncreacePhase(); //actualPhase always just increace, so starting with -1 is correct
    }

    public void IncreacePhase(){
        actualPhase++;
        //DestroyAllInstantiated(); //I make the null treatment already
        ManagerWait(); //This will make something that wait for the player interaction
        WaitFor();
    }

    private void ManagerWait(){
        objRef = waitManager;
        PoolObject(objRef);

        PhaseDescription aux = gamePhases[actualPhase].pd;
        objRef.GetComponent<MissionDisplay>().Setup(actualPhase, aux.namePhase, 
            aux.descriptionPhase, aux.additionalInfo);
    }

    private void SpawnAllGoals(){
        int i;

        for(i = 0; i < gamePhases.Count; i++){
            marking.SpawnGoal(gamePhases[i].textInstructions);
        }
    }

    public bool Check(int numberPhase){
        if(onAwait && numberPhase == actualPhase){
            PoolObject(objRef);
            //DestroyAllInstantiated();
            //objRef = Instantiate<GameObject>(gamePhases[actualPhase].manager.gameObject , this.transform);
            objRef =  gamePhases[actualPhase].manager.gameObject;
            PoolObject(objRef);

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
        if(objRef == null){ return;}

        Destroy(objRef);
    }

    private void PoolObject(GameObject pool){
        if(pool == null) return;

        pool.SetActive(!pool.activeSelf); //Will change to pool
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
