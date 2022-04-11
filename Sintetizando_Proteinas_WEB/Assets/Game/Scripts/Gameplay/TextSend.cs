using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using TMPro;
using System;

public class TextSend : MonoBehaviour{
    [SerializeField] TMP_InputField thisInput;

    [SerializeField] List<TextEvent> textLis;

    [System.Serializable]

    private class TextEvent{
        public UnityEvent m_MyEvent;
        public TextMeshProUGUI textField;
    }


    void Start(){
        SendAllText();
    }

    public void SendAllText(){
        int i;

        for(i = 0; i < textLis.Count; i++){
            //textLis[i].textField.text = textLis[i].m_MyEvent.Invoke();
        }
    }
    /*
    public void SendTextToObject(){
        if(m_MyEvent == null) return;
        if(thisInput.text.Length == 0) return;  

        print("texto = " + this.thisInput.text + " " + thisInput.text.Length);

        Util.UnityEventInvokeAllListenersTheSame
            (m_MyEvent, new object[] {this.thisInput.text}, new Type [] {typeof(string)});

        dependence.Invoke();
    }*/
}
