using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : MonoBehaviour
{
    #region SINGLETON PATTERN
    public static MainMenuWindow instance;
    private AudioSource sourceOfAudio;

    private void Awake(){
        if (instance !=  null){
            Destroy(instance);
            instance = this;
            return;
        }
        instance = this;
        sourceOfAudio = gameObject.AddComponent<AudioSource>();
    }

    #endregion

    public AudioClip startSound;

    private IEnumerator StartWithDelay(){
        sourceOfAudio.PlayOneShot(startSound);
        GameManager.instance.StartGame();
        yield return new WaitForSeconds(1f);
        ScoreWindow.instance.Show();
        Hide();
        
    }

    public void StartButtonClicked(){
        StartCoroutine(StartWithDelay());
    }

    public void ControlsButtonClicked(){
        ControlsWindow.instance.Show();
    }

    public void Hide(){
        instance.gameObject.SetActive(false);
    }
    public void Show(){
        instance.gameObject.SetActive(true);
    }
}
