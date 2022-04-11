using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Videos_scrip : MonoBehaviour
{
    public GameObject video1;
    public bool aux1 = false;
    public Button botao1;
    public GameObject tela_proteina_1;

    public GameObject video2;
    public bool aux2 = false;
    public Button botao2;
    public GameObject tela_proteina_2;

    public GameObject video3;
    public bool aux3 = false;
    public Button botao3;
    public GameObject tela_proteina_3;

    public GameObject video4;
    public bool aux4 = false;
    public Button botao4;
    public GameObject tela_proteina_4;

    void Start(){ }

    void Update()
    {
        botao1.onClick.AddListener(B1);
        botao2.onClick.AddListener(B2);
        botao3.onClick.AddListener(B3);
        botao4.onClick.AddListener(B4);

        if (video1.GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying == false && aux1==true){
            video1.SetActive(false);
            tela_proteina_1.SetActive(true);
            aux1 = false;}

        if (video2.GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying == false && aux2 == true)
        {
            video2.SetActive(false);
            tela_proteina_2.SetActive(true);
            aux2 = false;
        }

        if (video3.GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying == false && aux3 == true)
        {
            video3.SetActive(false);
            tela_proteina_3.SetActive(true);
            aux3 = false;
        }

        if (video4.GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying == false && aux4 == true)
        {
            video4.SetActive(false);
            tela_proteina_4.SetActive(true);
            aux4 = false;
        }
    }
    public void B1() { aux1 = true; }
    public void B2() { aux2 = true; }
    public void B3() { aux3 = true; }
    public void B4() { aux4 = true; }


}
