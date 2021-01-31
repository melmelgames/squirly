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
        Hide();
    }

    #endregion
    public Text scoreText;
    public Text timeText;
    private GameManager gameManagerInstance;
    private float timeElapsed;

    private void Start() {
        gameManagerInstance = GameManager.instance;   
        timeElapsed = 0;
    }

    private void Update(){
        timeElapsed += Time.deltaTime;
        timeText.text = "TIME: " + Mathf.FloorToInt(timeElapsed);

        scoreText.text = "ACORNS: " + gameManagerInstance.GetScore().ToString();
    }

    public void UpdateScoreText(){
        scoreText.text = "ACORNS: " + gameManagerInstance.GetScore().ToString();
    }

    public void EndForageButtonClicked(){
        int total = gameManagerInstance.GetTotalAcornsFound();
        int lost = gameManagerInstance.GetLostAcorns();
        int remain = gameManagerInstance.GetScore();
        GameOverWindow.instance.UpdateGameOverText(total, lost, remain);
        gameManagerInstance.EndGame();
        GameOverWindow.instance.Show();
        Hide();
    }

    public void Hide(){
        instance.gameObject.SetActive(false);
    }
    public void Show(){
        instance.gameObject.SetActive(true);
    }

    public void ResetTime(){
        timeElapsed = 0;
    }
}
