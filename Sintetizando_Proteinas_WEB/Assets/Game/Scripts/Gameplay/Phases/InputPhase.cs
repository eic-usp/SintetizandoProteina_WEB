using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhasePart{
    public class InputPhase : PhaseManagerMono{
        private int actualInput = -1;

        public void SetActualInput(int input){
            actualInput = input;
        }

        void OnEnable(){
            TextWithInput.SetOnwer(this); 
            StartCoroutine(ChangeInputField());//Easier way to start the components
        }
        public IEnumerator ChangeInputField(){
            int input;

            while(actualInput == -1){
                yield return null;
            }
            
            while(true){
                input = (int) Input.GetAxisRaw("Horizontal"); //Joystick only
                    
                if(input != 0 && (actualInput + input < this.transform.childCount && actualInput + input > -1)){
                    //Get the input based on the index of siblings
                    this.transform.GetChild(actualInput).GetComponent<TextWithInput>().DeactivateInput(); //Deactivates the actual
                    //print("INPUT = " + input);
                    actualInput += input;
                    this.transform.GetChild(actualInput).GetComponent<TextWithInput>().ActivateInput(); //Activates the "next"
                    //print(actualInput);
                    yield return new WaitForSeconds(0.2f); //Input too fast, so wait for some time
                }
                yield return null;
            }
        }
    }
}
