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
    public int goldenKeyCount = 0;
    public Text goldenKeyCounter;
    public int redKeyCount = 0;
    public Text redKeyCounter;
    public int blackKeyCount = 0;
    public Text blackKeyCounter;

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
    public bool dialogActive;

    //for projectile
    public GameObject projectile;

    public PlayerHealth playerHealth;

    // Update is called once per frame
    private void Start()
    {
        StartCoroutine(PlaceNameCo());
        myRigidbody = GetComponent<Rigidbody2D>();

        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();

        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);

        dialogBox.SetActive(true);
        dialogText.text = dialog;
    }
    void Update()
    {
        if (dialogActive != true)
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
        ForConversation();
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
        if (collision.gameObject.CompareTag("BlackKey"))
        {
            Destroy(collision.gameObject);
            blackKeyCount++;
            blackKeyCounter.text = "Score: " + blackKeyCount;
        }
        if (blackKeyCount >= 1 && collision.gameObject.CompareTag("BlackDoor"))
        {
            Destroy(collision.gameObject);
            blackKeyCount--;
        }
        // Get red keys
        if (collision.gameObject.CompareTag("RedKey"))
        {
            Destroy(collision.gameObject);
            redKeyCount++;
            redKeyCounter.text = "Score: " + redKeyCount;
        }
        if (redKeyCount >= 1 && collision.gameObject.CompareTag("RedDoor"))
        {
            Destroy(collision.gameObject);
            redKeyCount--;
        }
        // Get golden keys
        if (collision.gameObject.CompareTag("GoldenKey"))
        {
            Destroy(collision.gameObject);
            goldenKeyCount++;
            goldenKeyCounter.text = "Score: " + goldenKeyCount;
        }
        if (goldenKeyCount >= 1 && collision.gameObject.CompareTag("GoldenDoor"))
        {
            Destroy(collision.gameObject);
            goldenKeyCount--;
        }
        if (collision.gameObject.CompareTag("Heart"))
        {
            Destroy(collision.gameObject);
            playerHealth.GainHealth();
        }
    }

    //Level text display
    private IEnumerator PlaceNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(1.3f);
        text.SetActive(false);
    }

    //For conversation 
    private void ForConversation()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
                dialogActive = false;
            }
        }
    }

}

