using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using TMPro;

using System;
using System.Reflection;

//GeneralGetter generalgetter = new GeneralGetter();

public class TextSend : MonoBehaviour{
    [SerializeField] TMP_InputField thisInput;

    [SerializeField] List<TextEvent> textLis;
    
    [System.Serializable]
    private class TextEvent{
        public UnityEvent myEvent;

        public TextMeshProUGUI textField;
    }

    private string actualText;

    void Start(){
        SendAllText();
    }

    public void SendAllText(){
        int i, j;
        string finalText = "";
        object myGetter;
        GeneralGetter gg;

        UnityEvent m_MyEvent;

        for(i = 0; i < textLis.Count; i++){
            m_MyEvent = textLis[i].myEvent;
            for(j = 0; j < m_MyEvent.GetPersistentEventCount(); j++){
                myGetter = m_MyEvent.GetPersistentTarget(j);

                gg = (GeneralGetter) myGetter;
                gg.SetReceptorField(this, "actualText");   

                print("É : " + actualText);
                finalText = finalText + actualText;
            }

            textLis[i].textField.text = finalText;
            finalText = "";
        }
    }

    /*
    public static MyDelegate GetMyDelegateFromStringReflection(string methodName){
        MyDelegate function = MyFunctionsClass.CallMethodOne;

        Type inf = typeof(MyFunctionsClass);
        foreach (var method in inf.GetMethods())
        {
            if (method.Name == methodName)
            {
                function = (MyDelegate)Delegate.CreateDelegate(typeof(MyDelegate), method);
            }
        }

        return function;
    }*/

}
