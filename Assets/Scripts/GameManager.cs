﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    #region SINGLETON PATTERN
    public static GameManager instance;

    private void Awake(){
        if (instance !=  null){
            Destroy(instance);
            instance = this;
            return;
        }
        instance = this;

        Time.timeScale = 0.0f;
    }

    #endregion

    public GameObject acornPrefab;
    public Vector3 offset;
    private AcornTree[] trees;
    private int score;
    private int acornsLost;
    private int totalAcornsFound;

    public void AddScore(){
        score++;
        totalAcornsFound++;
    }

    public void SubtractScore(){
        score--;
    }

    public void AddLostAcorn(){
        acornsLost++;
    }

    public int GetScore(){
        return score;
    }

    public int GetLostAcorns(){
        return acornsLost;
    }
    public int GetTotalAcornsFound(){
        return totalAcornsFound;
    }

    private void SpawnAcorn(){
        int rdnIndex = Random.Range(0,trees.Length);
        GameObject newAcorn = (GameObject) Instantiate(acornPrefab, trees[rdnIndex].gameObject.transform.position + offset, Quaternion.identity);
    }

    public void StartGame(){
        Time.timeScale = 1.0f;
        score = 0;
        ScoreWindow.instance.ResetTime();
        trees = FindObjectsOfType<AcornTree>();
        InvokeRepeating("SpawnAcorn", 0f, 5.0f);
    }

    public void PauseGame(){
        Time.timeScale = 0.0f;
        CancelInvoke("SpawnAcorn");
    }

    public void UnpauseGame(){
        Time.timeScale = 1.0f;
        InvokeRepeating("SpawnAcorn", 0f, 5.0f);
    }

    public void EndGame(){
        Time.timeScale = 0.0f;
        CancelInvoke("SpawnAcorn");
    }


}
