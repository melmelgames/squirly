using System.Collections;
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
    }

    #endregion

    [SerializeField] private int score;

    private void Start(){
        score = 0;
    }

    public void AddScore(){
        score++;
    }

    public void SubtractScore(){
        score--;
    }

    public int GetScore(){
        return score;
    }


}
