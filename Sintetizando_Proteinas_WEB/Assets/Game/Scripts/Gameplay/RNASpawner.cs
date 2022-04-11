using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class RNASpawner : InputPhase{
    static string DNAString; //Original DNA string of the protein
    const string DNAtranscriptionBeg = "TAC"; //Always the beg of the DNA
    string[] DNAtranscriptionEnd = {"ATT", "ATC", "ACT"}; //The end of the DNA

    static bool random = false; //Sets if the start is a random protein or not

    private int quantity; //Needs to be multiple of 3, and it will because of the AMN
    [SerializeField] RNA prefab = default;

    //Color for the player input
    [SerializeField] Color defColor = default;
    [SerializeField] Color whenRight = default;
    [SerializeField] Color whenWrong = default;
    
    private Dictionary<string, string> validation = new Dictionary<string, string>(){
        {"A", "U"},
        {"T", "A"},
        {"C", "G"},
        {"G", "C"}
    }; //DNA to RNA Correspondence

    private string[] anwsers; //Save of the anwsers of the RNA given by the player

    //Filters that make the gameEasier

    //[SerializeField] string defaultFilter = default;
    //[SerializeField] Transform filterSpawn = default;
    //[SerializeField] GameObject letterPrefab = default;
    //private string actualLetter;

    private int nextPhase = 0;

    private void Start() {
        //actualLetter = defaultFilter;
        quantity = AMNManager.GetNumberOfAMN() * AMNManager.GetSizeAMN();
        anwsers = new string[quantity];

        //GenerateFilters();
        if(!random){
            InstantiateAllRNABasedOnDNA();
            return;
        }

        InstantiateAllRNARandom();
    }

    private string CutDNAString(){
        string sub;

        do{
            //Cuts a part of the DNA to make the substring
            sub = Util.RandomSubString(DNAString, quantity, 0, (DNAString.Length - quantity));
            print("sub : " + sub);
        }while(Util.FindOcorrence(sub, DNAtranscriptionEnd, AMNManager.GetSizeAMN()));

        return sub;
    }

    private void InstantiateAllRNABasedOnDNA(){
        int i;
        string sub = CutDNAString();
        RNA hold;

        //sub = DNAtranscriptionBeg + sub + DNAtranscriptionEnd[Random.Range(0 , DNAtranscriptionEnd.Length)];

        for(i = 0 ; i < quantity ; i++){
            hold = Instantiate<RNA>(prefab, this.transform);
            hold.SetPosition(i); //Puts its original position, so i can build the "replic" vector
            hold.Setup(sub[i].ToString());
        }
    }


    private void InstantiateAllRNARandom(){
        int i;
        RNA hold;

        for(i = 0 ; i < quantity ; i++){
            hold = Instantiate<RNA>(prefab, this.transform);
            hold.SetPosition(i); //Puts its original position, so i can build the "replic" vector
            hold.RandomStart();
        }
    }

    private void DestroyAllRNA(){
        foreach(Transform child in this.transform){
            Destroy(child.gameObject);
        }
    }

    public void ResetValuesInRNA(){
        nextPhase = 0;
        
        foreach(Transform child in this.transform){
            child.GetComponent<RNA>().SetValue(" ", defColor);
        }
    }

    public void ChangeQuantityToNextPhase(int increace){
        nextPhase += increace;  
    }

    public new void EndPhase(){
        if(nextPhase == quantity){
            //Here its change phases
            AMNManager.SetRNAtoAMNString(ConvertToString(anwsers));
            base.EndPhase();
        }

        return;
    }

    public string ConvertToString(string[] phrase){
        string res = ""; 
        int i;
        for(i = 0; i < phrase.Length ; i++){
            res += phrase[i];
        }

        return res;
    }

    public void ConfirmPhase(){ //Can be used, but don make much sense
        RNA childComp;

        foreach(Transform child in this.transform){
            childComp = child.GetComponent<RNA>();
            anwsers[childComp.GetOriginalPosition()] = childComp.GetValueInputText();
        }

        EndPhase();
    }

    public void SetCorrespondentValidation(int index, string value){
        anwsers[index] = value;
    }

    public static void SetDNAString(string proteinDNA){
        DNAString = proteinDNA;
    }

    public Color GetColorRight(){
        return whenRight;
    }

    public Color GetColorWrong(){
        return whenWrong;
    }

    public string GetValueValidation(string keyPas){
        return validation[keyPas];
    }

    public int GetValidationCount(){
        return validation.Count;
    }
    
    public string GetKeyByIndex(int index){
        return validation.Keys.ElementAt(index);
    }

    public void FilterCorrectRNA(){ //Remove RNA already correct, put them in the right position on the vector too
        RNA childComp;
        foreach(Transform child in this.transform){
            childComp = child.GetComponent<RNA>();
            if(childComp.GetValueInputValidation()){
                anwsers[childComp.GetOriginalPosition()] = childComp.GetValueInputText();
                Destroy(child.gameObject);
            }
        }

        EndPhase();
    }

    public void StartNewWave(){ //First version
        DestroyAllRNA();
        InstantiateAllRNARandom();
    }

    public void StartNewWaveByActualSize(){ //SecondVersion
        int childCnt = this.transform.childCount; //Necessary reference
        DestroyAllRNA();

        for(; childCnt > 0 ; childCnt--){
            Instantiate<RNA>(prefab, this.transform);
        }

        //FilterRNABasedOnLetter(actualLetter);
    }

    public void StartNewWaveDontDestroy(){ //ThirdVersion, random
        foreach(Transform child in this.transform){
            child.GetComponent<RNA>().RandomStart();
        }

        //FilterRNABasedOnLetter(actualLetter);
    } //Best performance

    public void StartNewWaveDNAString(){ //Here we don't have the problem of "destroying" the DNA
        string sub = CutDNAString();
        int i = 0;

        foreach(Transform child in this.transform){
            child.GetComponent<RNA>().Setup(sub[i].ToString());
            i++;
        }
    }
}

/*
private Dictionary<string, string[]> validation = new Dictionary<string, string[]>(){
    {"A", new string[] {"U", "T"}},
    {"T", new string[] {"A"}},
    {"C", new string[] {"G"}},
    {"G", new string[] {"C"}}
};
*/

    /*
    public void GenerateFilters(){ //Spawn all the letter filters
        GameObject holder = Instantiate<GameObject>(letterPrefab , filterSpawn);
        holder.GetComponent<Letter>().Setup(defaultFilter);

        foreach(KeyValuePair<string, string> item in validation){
            holder = Instantiate<GameObject>(letterPrefab , filterSpawn);
            holder.GetComponent<Letter>().Setup(item.Key);
        }
    }

    public void FilterRNABasedOnLetter(string chosen){ //Separate RNA based on their Letter
        actualLetter = chosen;
        if(chosen == defaultFilter){
            foreach(Transform child in this.transform){
                child.gameObject.SetActive(true);
            }

            return;
        }

        foreach(Transform child in this.transform){
            if(child.GetComponent<RNA>().GetValueText() != chosen){
                child.gameObject.SetActive(false);
                continue;
            }
            child.gameObject.SetActive(true);
        }
    }*/