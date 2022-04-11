using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoEditableComponent : InfoComponent{
    [SerializeField] List<TextMeshProUGUI> texts = default;

    public void Setup(List<string> texts, int start){
        int i;

        if(start + texts.Count > texts.Count){
            return;
        }

        for(i = 0; i < texts.Count; i++){
            this.texts[i + start].text = texts[i];
        }
    }
}