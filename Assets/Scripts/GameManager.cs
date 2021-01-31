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

    public GameObject acornPrefab;
    public Vector3 offset;
    private AcornTree[] trees;
    [SerializeField] private int score;

    private void Start(){
        score = 0;
        trees = FindObjectsOfType<AcornTree>();
        InvokeRepeating("SpawnAcorn", 0f, 5.0f);
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

    private void SpawnAcorn(){
        int rdnIndex = Random.Range(0,trees.Length);
        GameObject newAcorn = (GameObject) Instantiate(acornPrefab, trees[rdnIndex].gameObject.transform.position + offset, Quaternion.identity);
    }


}
