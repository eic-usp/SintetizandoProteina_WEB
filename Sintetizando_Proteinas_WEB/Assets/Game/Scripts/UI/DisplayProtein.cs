using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

//Not used anymore

public class DisplayProtein : MonoBehaviour{
    [SerializeField] GameObject proteinPrefab;
    [SerializeField] List<Protein> proteinData;
    [SerializeField] Transform content;

    //[SerializeField] VideoPlayer vp = default;

    private int actualProtein;
    List<Protein> originalPosition = new List<Protein>();

    [SerializeField] int valueSides = 1;

    private void Start() {
        actualProtein = valueSides;
        Setup();
        //MoveFoward();
    }

    private void Setup(){
        Protein pt;
        int i;

        for(i = 0; i < proteinData.Count; i++){
            pt = Instantiate<Protein>(proteinData[i] , content);
            //pt.Setup(i); 
            pt.gameObject.name = i.ToString();
            originalPosition.Add(pt);
        }

        InstantaneousChange(actualProtein);
    }

    //Easier and more correct way above

    private void ChangeVisibility(bool visil, int childIndex){
        originalPosition[childIndex].gameObject.SetActive(visil);
    }

    public (Transform, int) GetRefForward(bool visil, int value){
        int pos = (value + valueSides) % proteinData.Count;
        //print("actual protein = " + value + "for = " + pos);

        ChangeVisibility(visil, pos);

        return (originalPosition[pos].transform, pos);
    }

    public void ChangeForward((Transform child , int pos) t){
        t.child.SetSiblingIndex(proteinData.Count - 1);
    }

    public (Transform, int) GetRefBackward(bool visil , int value){
        int pos = value - valueSides;
        //print("actual protein = " + value + "back = " + pos );

        if(pos < 0){
            pos = (proteinData.Count + pos);
        }

        ChangeVisibility(visil, pos);
        return (originalPosition[pos].transform , pos);
    }

    public void ChangeBackward((Transform child, int pos) t){
        t.child.SetSiblingIndex(0);
    }

    public void MoveForward(){
        OnlyMoveForward();
        ChangeVideo();
    } 

    private void OnlyMoveForward(){
        (Transform res, int pos) t = GetRefForward(true, actualProtein + 1);

        ChangeBackward(GetRefBackward(false, actualProtein));
        ChangeForward(t);

        actualProtein = (actualProtein + 1)% proteinData.Count;
    }

    public void MoveBackward(){
        OnlyMoveBackward();
        ChangeVideo();
    }

    private void OnlyMoveBackward(){
        (Transform res, int pos) t = GetRefBackward(true, actualProtein - 1);

        ChangeForward(GetRefForward(false, actualProtein));
        ChangeBackward(t);   

        actualProtein--;

        if(actualProtein < 0){
            actualProtein = proteinData.Count - 1;
        }
    } 

    public void InstantaneousChange(int newIndex){
        int i;

        actualProtein = newIndex;
        foreach(Protein item in FindObjectsOfType<Protein>()){
            item.gameObject.SetActive(false);
        }

        for(i = 0 ; i < valueSides ; i++){
            ChangeForward(GetRefForward(true, actualProtein + i ));
            ChangeBackward(GetRefBackward(true, actualProtein - i));
        }

        originalPosition[actualProtein].gameObject.SetActive(true);
        ChangeVideo();
    }

    private void ChangeVideo(){
        //vp.clip = proteinData[actualProtein].ReturnVideo(); ERROR because of code change
    }

    public void OnScroll(){
		if(Input.mouseScrollDelta.y > 0){
            print("Entrou");
            OnlyMoveForward();
            return;
        }   
        print("Passou");
        OnlyMoveBackward();
	}
    
    /*
    public void OnMouseDrag(){

    }*/

    /*
    private float widthPrefab;
    private int limit = 0;

    //50 and 55 are fixed values, need to change that
    //It does not make sense to less than 4, and if you do it gonna die from division
    void Start(){
        Setup();
        limit = proteinData.Count - 3;
        widthPrefab = proteinPrefab.GetComponent<RectTransform>().rect.width / 2;

        widthPrefab = (widthPrefab + ( (55)
            * (limit))) / limit; //This count is very strange, but its work
        limit = proteinData.Count  - proteinData.Count % 2; //For the side check, only for odd

        ChangeProteinOnScreen((proteinData.Count / 2)); //Makes the protein in the middle
    }
    
    public void ChangeProteinOnScreen(int change){
        actualProtein = change;
        print("Actual protein = " + actualProtein);
        print("GameObject = " + content.GetChild(actualProtein));
        Centralize();
        ChangeVideo();
    }


    private void Centralize(){
        Vector3 pos = new Vector3(widthPrefab * (actualProtein - (proteinData.Count / 2)), 0, 0);
        content.localPosition = pos;

        if(actualProtein > proteinData.Count - 1) actualProtein = proteinData.Count - 1;
    }

    public void MoveToSides(int side){

        if(actualProtein + side < 0 || actualProtein + side > limit){
            return;
        }

        ChangeProteinOnScreen(actualProtein + side);
        print("After " + actualProtein);
    }*/
}
