using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip grabSound;
    public AudioClip dropSound;
    public AudioClip digSound;
    private AudioSource sourceOfAudio;
    public GameObject holeEmpty;
    public GameObject holeAcorn;
    public GameObject holeWithAcornCovered;
    public GameObject holeCovered;
    public GameObject acornPrefab;
    private ScoreWindow scoreWindowScript;
    private GameObject acorn;
    private Rigidbody2D playerRB2D;
    private Animator playerAnimator;
    private Vector2 movementDir;
    private bool isHoldingAcorn = false;
    private GameManager gameManagerInstance;
    private GameObject hole;
    [SerializeField] private float moveSpeed;
    private bool gamePaused = false;

    private void Start(){
        acorn = null;
        playerRB2D = gameObject.GetComponent<Rigidbody2D>();
        playerAnimator = gameObject.GetComponent<Animator>();
        gameManagerInstance = GameManager.instance;
        scoreWindowScript = ScoreWindow.instance.gameObject.GetComponent<ScoreWindow>();
        sourceOfAudio = gameObject.AddComponent<AudioSource>();
    }


    private void Update(){

        if(Input.GetKeyDown(KeyCode.P)){
            if(gamePaused){
                gamePaused = false;
                PauseWindow.instance.Hide();
                gameManagerInstance.UnpauseGame();
            }
            if(!gamePaused){
                gamePaused = true;
                PauseWindow.instance.Show();
                gameManagerInstance.PauseGame();
            }

        }


        GetInput(); 
        // GRAB/DROP ACORNS
        if(Input.GetKeyDown(KeyCode.E)){
            if(!isHoldingAcorn){
                GrabAcorn(acorn);
                sourceOfAudio.PlayOneShot(grabSound);
                return;
            }
            if(isHoldingAcorn){
                DropAcorn();
                sourceOfAudio.PlayOneShot(dropSound);
                return;
            }
        }  
        // DIG/COVER HOLES 
        if(Input.GetKeyDown(KeyCode.R)){
            if(hole == null){
                DigHole();
                sourceOfAudio.PlayOneShot(digSound);
                return;
            }
            if(hole.CompareTag("hole_covered") || hole.CompareTag("hole_with_acorn_covered")){
                DigHole();
                sourceOfAudio.PlayOneShot(digSound);
                return;
            }
            if(hole.CompareTag("hole_empty") || hole.CompareTag("hole_with_acorn")){
                CoverHole();
                sourceOfAudio.PlayOneShot(digSound);
                return;
            }

            
        }      
    }

    private void FixedUpdate(){
        MovePlayer();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("acorn")){
            acorn = other.gameObject;                   
        }
        if(other.gameObject.GetComponent<Hole>() != null){
            hole = other.gameObject;
            if(other.gameObject.transform.childCount > 0){
                acorn = other.gameObject.transform.GetChild(0).gameObject;
            }                  
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("acorn")){
            acorn = null;
        }
        if(other.gameObject.GetComponent<Hole>() != null){
            hole = null;                   
        }
    }

    private void GetInput()
    {
        // Movement
        movementDir.x = Input.GetAxisRaw("Horizontal");
        movementDir.y = Input.GetAxisRaw("Vertical");

    }

    private void MovePlayer()
    {

        playerRB2D.MovePosition(playerRB2D.position + movementDir.normalized * moveSpeed * Time.fixedDeltaTime);

        playerAnimator.SetFloat("horizontalInput", movementDir.x);
        playerAnimator.SetFloat("verticalInput", movementDir.y);

    }

    private void GrabAcorn(GameObject acorn){
        if(acorn != null && !isHoldingAcorn){
            acorn.transform.parent = gameObject.transform;
            acorn.SetActive(true);
            isHoldingAcorn = true;
            if(hole != null && hole.CompareTag("hole_with_acorn")){
                gameManagerInstance.SubtractScore();
                Instantiate(holeEmpty, hole.transform.position, Quaternion.identity);
                Destroy(hole);
            }           
        }
    }

    private void DropAcorn(){
        if(hole == null){
            acorn = gameObject.transform.GetChild(0).gameObject;
            acorn.transform.parent = null;
            isHoldingAcorn = false;
            acorn = null;
        }
        if(hole != null && hole.CompareTag("hole_empty")){
            gameManagerInstance.AddScore();
            acorn = gameObject.transform.GetChild(0).gameObject;
            GameObject filledHole = (GameObject) Instantiate(holeAcorn, hole.transform.position, Quaternion.identity);
            Destroy(hole);
            acorn.transform.parent = filledHole.transform;
            acorn.SetActive(false);
            isHoldingAcorn = false;
            acorn = null;
            
        }
        
    }

    private void DigHole(){
        if(hole == null){
            playerAnimator.SetTrigger("dig");
            Instantiate(holeEmpty, transform.position, Quaternion.identity);          
        }
        if(hole != null){
            if(hole.CompareTag("hole_covered")){
                playerAnimator.SetTrigger("dig");
                Instantiate(holeEmpty, hole.transform.position, Quaternion.identity);  
                Destroy(hole);
                return;
            }        
        
            if(hole.CompareTag("hole_with_acorn_covered")){
                playerAnimator.SetTrigger("dig");
                GameObject holeWithAcorn = (GameObject) Instantiate(holeAcorn, hole.transform.position, Quaternion.identity);  
                GameObject buriedAcorn = (GameObject) Instantiate(acornPrefab, hole.transform.position, Quaternion.identity);
                buriedAcorn.SetActive(false);
                buriedAcorn.transform.parent = holeWithAcorn.transform;
                Destroy(hole);
                return;
            }        
        }
    }

    private void CoverHole(){
        if(hole.CompareTag("hole_empty")){
            playerAnimator.SetTrigger("dig");
            Instantiate(holeCovered, hole.transform.position, Quaternion.identity);
            Destroy(hole);
            return;
        }
        if(hole.CompareTag("hole_with_acorn")){
            playerAnimator.SetTrigger("dig");
            Instantiate(holeWithAcornCovered, hole.transform.position, Quaternion.identity); 
            Destroy(hole);
            return;
        }
    }

    
}
