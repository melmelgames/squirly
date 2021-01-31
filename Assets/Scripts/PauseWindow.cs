using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : MonoBehaviour
{
    #region SINGLETON PATTERN
    public static PauseWindow instance;

    private void Awake(){
        if (instance !=  null){
            Destroy(instance);
            instance = this;
            return;
        }
        instance = this;
        Hide();
    }

    #endregion

    public void ResumeButtonClicked(){
        GameManager.instance.UnpauseGame();
        Hide();
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
