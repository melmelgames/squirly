using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : MonoBehaviour
{
    #region SINGLETON PATTERN
    public static MainMenuWindow instance;

    private void Awake(){
        if (instance !=  null){
            Destroy(instance);
            instance = this;
            return;
        }
        instance = this;
    }

    #endregion

    public void StartButtonClicked(){
        GameManager.instance.StartGame();
        ScoreWindow.instance.Show();
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
