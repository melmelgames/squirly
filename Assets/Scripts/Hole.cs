﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int chanceOfEnemySpawn = 5;

    private void OnTriggerEnter2D(Collider2D other) {
        if(gameObject.CompareTag("hole_covered") || gameObject.CompareTag("hole_with_acorn_covered")){
            if(other.gameObject.CompareTag("Player")){
                int rdnInt = Random.Range(1,101);
                if(rdnInt <= chanceOfEnemySpawn){
                    Instantiate(enemyPrefab, transform.position, Quaternion.identity);    
                }
            }
        }
    }

    
}
