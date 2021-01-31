using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour
{
        #region SINGLETON PATTERN
    public static ScoreWindow instance;

    private void Awake(){
        if (instance !=  null){
            Destroy(instance);
            instance = this;
            return;
        }
        instance = this;
    }

    #endregion
    public Text scoreText;
    private GameManager gameManagerInstance;

    private void Start() {
        gameManagerInstance = GameManager.instance;   
        UpdateScoreText(); 
    }

    public void UpdateScoreText(){
        scoreText.text = "ACORNS : " + gameManagerInstance.GetScore().ToString();
    }
}
