using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    #region SINGLETON PATTERN
    public static GameOverWindow instance;

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

    public Text gameOverText;

    public void UpdateGameOverText(int score, int acornsLost, int remaining){
        gameOverText.text = "YOU FOUND " + score.ToString() + " ACORNS AND YOU LOST " + acornsLost.ToString() + " TO OTHER SQUIRRELS, SO YOU STILL HAVE " + remaining.ToString() + " STASHED AWAY FOR WINTER!";
    }

    public void RestartButtonClicked(){
        GameManager.instance.StartGame();
        ScoreWindow.instance.Show();
        Hide();
    }

    public void MainMenuButtonClicked(){
        MainMenuWindow.instance.Show();
        Hide();
    }

    public void Hide(){
        instance.gameObject.SetActive(false);
    }
    public void Show(){
        instance.gameObject.SetActive(true);
    }
}
