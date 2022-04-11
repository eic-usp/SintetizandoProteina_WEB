using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoChoice : MonoBehaviour{
    [SerializeField] List<VideoClip> videoClips = new List<VideoClip>();
    [SerializeField] Transform screens;

    private VideoPlayer videoPlayer;
    private int actualVideoClip;

    //public RawImage rawImage;
 

    private void Start(){
        Protein.Setup(this);
        videoPlayer = GetComponent<VideoPlayer>();
    }

    public void ChooseProtein(int index){
        videoPlayer.clip = videoClips[index];
        actualVideoClip = index;
        this.transform.GetChild(0).gameObject.SetActive(true); //The buttons
        //rawImage.texture = videoPlayer.texture;//----------
        PlayVideo();
    }

    private IEnumerator FinishCheck(){
        print("Entrou aqui");
        while(videoPlayer.isPlaying){
            yield return null;
        }

        ShowScreen();
        yield return null;
    }

    private void ShowScreen(){
        this.gameObject.SetActive(false); //Instanteneous stop all coroutine
        screens.GetChild(actualVideoClip).gameObject.SetActive(true);
    }

    public void StopVideo(){
        if(!videoPlayer.isPlaying) return;

        StopCoroutine(FinishCheck());
        videoPlayer.Stop();
    }

    public void PlayVideo(){
        if(videoPlayer.isPlaying) return;

        videoPlayer.Play();
        StartCoroutine(FinishCheck());
    }

    public void SkipVideo(){
        ShowScreen();
    }


}
