using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum PlayerState
{
    walk,
    attack,
    interact
}
public class Player : MonoBehaviour
{
    public PlayerState currentState;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    //Player speed
    private float speed = 20f;
    //displaying score
    public int score = 0;
    public Text ScoreBoard;
    //for animation
    private Animator animator;
    //For showing level at the start
    public string placeName;
    public GameObject text;
    public Text placeText;
    //For conversation
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    private bool dialogActive = true;

    // Update is called once per frame
    private void Start()
    {
        StartCoroutine(placeNameCo());
        myRigidbody = GetComponent<Rigidbody2D>();
        
        animator = GetComponent<Animator>();

        dialogBox.SetActive(true);
        dialogText.text = dialog;
    }
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
        else if(currentState == PlayerState.walk)
        {
            UpdateAnimationAndMove();
        }
            
        
        forConversation();
    }
    
    //attack animation wid delay
    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null; //wait one frame
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.1f);
        currentState = PlayerState.walk;
    }
    //Level text display
    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(2f);
        text.SetActive(false);
    }
    //character move animations
    void UpdateAnimationAndMove()
    {
        if (dialogActive != true)
        {
            if (change != Vector3.zero)
            {
                MoveCharacter();
                animator.SetFloat("moveX", change.x);
                animator.SetFloat("moveY", change.y);
                animator.SetBool("moving", true);
            }
            else
            {
                animator.SetBool("moving", false);
            }
        }
    }

    void MoveCharacter()
    {
        if (dialogActive != true)
        {
            myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            // Collision on wall
            if (collision.gameObject.tag == "Walls") { }
        // Get keys
        if (collision.gameObject.tag == "Keys")
        {
             Destroy(collision.gameObject); 
            
            score++;
            ScoreBoard.text = "Score: " + score;
        }
        if(collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (score >= 1 && collision.gameObject.tag == "Door")
        {
            Destroy(collision.gameObject);
                score--;
        }
        }
        catch (InvalidCastException) { print("key gone"); }
    }

    private void forConversation()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
                dialogActive = false;
            }
        }
    }
    
}

