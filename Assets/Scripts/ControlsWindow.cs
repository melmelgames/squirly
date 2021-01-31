using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsWindow : MonoBehaviour
{
    #region SINGLETON PATTERN
    public static ControlsWindow instance;

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

    public void CloseButtonClicked(){
        Hide();
    }

    public void Hide(){
        instance.gameObject.SetActive(false);
    }
    public void Show(){
        instance.gameObject.SetActive(true);
    }
}
