using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class Letter : MonoBehaviour{
    public void Setup(string value){
        this.transform.GetComponentInChildren<TextMeshProUGUI>().text = value;
    }

    public string GetValue(){
        return this.transform.GetComponentInChildren<TextMeshProUGUI>().text;
    }
}
