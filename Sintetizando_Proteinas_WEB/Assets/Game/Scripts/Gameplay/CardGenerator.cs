using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour{
    int actualPhase = 0;
    int maxPhase = 5;
    [SerializeField] int initialLuck = default;
    private int actualLuck;
    [SerializeField] int maxNumbers = 3;

    private void Start() {
        actualLuck = initialLuck;
        RandomGenerateCard();
    }

    int RandomGenerateCard(){
        int i = 0;
        int res = -1;
        int[] numbers = new int[maxNumbers];
        numbers[0] = actualPhase;

        Util.RandomVectorFill(numbers, 1, 0, maxPhase);
        //PrintVector(numbers);
        //print("Numbers" + numbers);
        
        while(i < actualLuck && res != actualPhase){
            res = numbers[Random.Range(0 , maxNumbers)];
            print("Res "+ i + ": " + res);
            i++;
        }
        
        if(res == actualPhase){
            actualLuck = initialLuck;
        }else{
            actualLuck++;
        }

        return res;
    }
}
