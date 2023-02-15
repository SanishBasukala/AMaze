using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    idle,
    walk,
    attack,
    interact,
    stagger
}
public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;

    public PlayerState currentState;


    //displaying key counts
    public int yellowKeyCount = 0;
    public Text yellowKeyCounter;
    public int redKeyCount = 0;
    public Text redKeyCounter;
    public int blueKeyCount = 0;
    public Text blueKeyCounter;
    public int FinalDoorItemCount = 0;

    //for animation
    private Animator animator;

    //For conversation
    //public Message[] messages;
    //public Actor[] actors;

    //for projectile
    public GameObject projectile;

    public PlayerHealth playerHealth;

    public SaveHandler saveHandler;//replace

    [SerializeField] private AudioSource walkAudio;
    [SerializeField] private AudioSource gainHeartAudio;
    [SerializeField] private AudioSource attackAudio;
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        walkAudio = GetComponent<AudioSource>();
        gainHeartAudio = GetComponent<AudioSource>();
        attackAudio = GetComponent<AudioSource>();

        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();

        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }
    void Update()
    {
        if (change != Vector3.zero)
        {
            if (!walkAudio.isPlaying)
            {
                walkAudio.Play();
            }
        }
        else if (change == Vector3.zero)
        {
            walkAudio.Stop();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            gainHeartAudio.Play();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            attackAudio.Play();
            attackAudio.loop = true;
        }

        try
        {
            if (DialogManager.isActive == true || saveHandler.inCreate == true)
                return;

            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            if (change != Vector3.zero && currentState != PlayerState.stagger)
            {
                MoveCharacter();
            }
            if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
            {
                attackAudio.Play();
                StartCoroutine(AttackCo());
            }
            else if (Input.GetButtonDown("Second Weapon") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
            {
                StartCoroutine(SecondAttackCo());
            }
            else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
            {
                UpdateAnimationAndMove();
            }
            //}

            //conversation.ForConversation();
        }
        catch (NullReferenceException)
        {
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            if (change != Vector3.zero && currentState != PlayerState.stagger)
            {
                MoveCharacter();
            }
            if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
            {
                StartCoroutine(AttackCo());
            }
            else if (Input.GetButtonDown("Second Weapon") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
            {
                StartCoroutine(SecondAttackCo());
            }
            else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
            {
                UpdateAnimationAndMove();
            }
        }
    }

    //attack animation wid delay
    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);

        currentState = PlayerState.attack;
        yield return null; //wait one frame
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    private IEnumerator SecondAttackCo()
    {
        //animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null; //wait one frame
        MakeArrow();
        //animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    private void MakeArrow()
    {
        Vector2 tempDirection = new(animator.GetFloat("moveX"), animator.GetFloat("moveY")); //new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.Setup(tempDirection, ChooseArrowDirection());
    }

    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }

    //character move animations
    void UpdateAnimationAndMove()
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

    //Character movement
    void MoveCharacter()
    {


        change.Normalize();
        // check this ____________________________________________________________________________________________________
        myRigidbody.MovePosition(transform.position + speed * Time.deltaTime * change);
    }

    public void Knock(float knockTime)
    {
        StartCoroutine(KnockCo(knockTime));
    }
    private IEnumerator KnockCo(float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collision on wall
        //if (collision.gameObject.CompareTag("Walls")) { }
        // Get black keys
        if (collision.gameObject.CompareTag("BlueKey"))
        {
            Destroy(collision.gameObject);
            blueKeyCount++;
            blueKeyCounter.text = "" + blueKeyCount;
        }
        if (blueKeyCount >= 1 && collision.gameObject.CompareTag("BlueDoor"))
        {
            Destroy(collision.gameObject);
            blueKeyCount--;
        }
        // Get red keys
        if (collision.gameObject.CompareTag("RedKey"))
        {
            Destroy(collision.gameObject);
            redKeyCount++;
            redKeyCounter.text = "" + redKeyCount;
        }
        if (redKeyCount >= 1 && collision.gameObject.CompareTag("RedDoor"))
        {
            Destroy(collision.gameObject);
            redKeyCount--;
        }
        // Get golden keys
        if (collision.gameObject.CompareTag("YellowKey"))
        {
            Destroy(collision.gameObject);
            yellowKeyCount++;
            yellowKeyCounter.text = "" + yellowKeyCount;
        }
        if (yellowKeyCount >= 1 && collision.gameObject.CompareTag("YellowDoor"))
        {
            Destroy(collision.gameObject);
            yellowKeyCount--;
        }
        if (collision.gameObject.CompareTag("Heart"))
        {
            gainHeartAudio.Play();
            Destroy(collision.gameObject);
            playerHealth.GainHealth();
        }
        if (collision.gameObject.CompareTag("FinalDoorItem"))
        {
            Destroy(collision.gameObject);
            FinalDoorItemCount++;
        }
        if (collision.gameObject.CompareTag("FinalDoor") && FinalDoorItemCount == 1)
        {
            Destroy(collision.gameObject);
        }

    }
}
