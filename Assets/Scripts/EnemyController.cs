using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject holeEmpty;
    public GameObject holeAcorn;
    public GameObject acornPrefab;
    private ScoreWindow scoreWindowScript;
    private Animator enemyAnimator;
    private bool isHoldingAcorn = false;
    private GameManager gameManagerInstance;
    private GameObject hole;
    private GameObject acorn;
    private bool firstHoleDug = false;
    private GameObject player;
    private Vector2 moveDir;
    private float moveSpeed = 4.5f;
    private bool canRun = false;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;

    private void Awake(){
        enemyAnimator = gameObject.GetComponent<Animator>();
        gameManagerInstance = GameManager.instance;
        scoreWindowScript = ScoreWindow.instance.gameObject.GetComponent<ScoreWindow>();
        player = FindObjectOfType<PlayerController>().gameObject;
        
    }

    private void Update(){
        if(canRun){
            moveDir = gameObject.transform.position - player.transform.position;
            transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.World);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<Hole>() != null){
            hole = other.gameObject;
            if(hole.CompareTag("hole_covered") || hole.CompareTag("hole_with_acorn_covered")){
                if(!firstHoleDug){
                    firstHoleDug = true;
                    DigHole();
                    StartCoroutine(RunAwayAfterTime());
                }
            }
        }

        if(other.gameObject.CompareTag("hole_with_acorn")){
            acorn = other.gameObject.transform.GetChild(0).gameObject;
            StartCoroutine(GrabAcornAftertTime());
        }
    }

    private IEnumerator RunAwayAfterTime(){
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        canRun = true;
        Destroy(gameObject, 30f);
    }

    private IEnumerator GrabAcornAftertTime(){
        yield return new WaitForSeconds(0.5f);
        GrabAcorn(acorn);
    }

    private void GrabAcorn(GameObject acorn){
        if(acorn != null && !isHoldingAcorn){
            acorn.transform.parent = gameObject.transform;
            acorn.SetActive(true);
            isHoldingAcorn = true;
            gameManagerInstance.AddLostAcorn();
            gameManagerInstance.SubtractScore();
            Instantiate(holeEmpty, hole.transform.position, Quaternion.identity);     
            Destroy(hole);      
        }
    }
    private void DigHole(){
        if(hole == null){
            Debug.LogError("No hole to look for acorns!");
            return;          
        }
        if(hole != null){
            if(hole.CompareTag("hole_covered")){
                enemyAnimator.SetTrigger("dig");
                Instantiate(holeEmpty, hole.transform.position, Quaternion.identity);  
                Destroy(hole);
            }        
        
            if(hole.CompareTag("hole_with_acorn_covered")){
                enemyAnimator.SetTrigger("dig");
                GameObject holeWithAcorn = (GameObject) Instantiate(holeAcorn, hole.transform.position, Quaternion.identity);  
                GameObject buriedAcorn = (GameObject) Instantiate(acornPrefab, hole.transform.position, Quaternion.identity);
                buriedAcorn.SetActive(false);
                buriedAcorn.transform.parent = holeWithAcorn.transform;
                Destroy(hole);
            }        
        }  
    }
}
