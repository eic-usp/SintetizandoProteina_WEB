using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace PhasePart.RNA{
    public class RNA : TextWithInput{ 
        [SerializeField] Image lightConfirm = default; 
        private bool valueInput = false;

        void Start(){

            //Adds a listener to the main input field and invokes a method when the value changes.
            GetMainInputField().onValueChanged.AddListener(delegate {ValueChangeCheck(); });
        }

        private void GenerateText(){
            RNASpawner RNAonwer = (RNASpawner)owner;

            int value = UnityEngine.Random.Range(0 , RNAonwer.GetValidationCount());
            Setup(RNAonwer.GetKeyByIndex(value)); //Inheritance from Letter
        }

        public void RandomStart(){
            GenerateText();
        }

        // Invoked when the value of the text field changes.
        void ValueChangeCheck(){
            RNASpawner RNAonwer = (RNASpawner)owner;
            
            string val = GetValueInputText().ToUpper(); //Easier to work
            RNAonwer.SetCorrespondentValidation(originalPosition, val);

            //Validates the input with the RNA
            if(RNAonwer.GetValueValidation(GetValue()) == val){
                RNAonwer.ChangeQuantityToNextPhase(Convert.ToInt32(!valueInput));
                valueInput = true;

                SetValue(val, RNAonwer.GetColorRight());
                return;
            }

            RNAonwer.ChangeQuantityToNextPhase(Convert.ToInt32(valueInput) * -1);

            valueInput = false;

            SetValue(val, RNAonwer.GetColorWrong());
        }

        public void SetValue(string valuePas, Color valueColor){
            SetValueInputText(valuePas);

            lightConfirm.color = valueColor;
        }

        public bool GetValueInputValidation(){
            return this.valueInput;
        }

        public string GetValueText(){
            return this.GetValue();
        }

        public int GetOriginalPosition(){
            return this.originalPosition;
        }
    }
}


    /*
    public void SetValue(string value, Color? valueColor){
        mainInputField.text = value;

        lightConfirm.color = valueColor ?? defColor;
    }*/
