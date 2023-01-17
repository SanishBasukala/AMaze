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

    //displaying score
    public int keyCount = 0;
    public Text keyCounter;

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

    public Chest chest;

    // Update is called once per frame
    private void Start()
    {
        StartCoroutine(placeNameCo());
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
            if (change != Vector3.zero)
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
        forConversation();
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
        Vector2 tempDirection = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
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
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
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
        if (collision.gameObject.tag == "Walls") { }
        // Get keys
        if (collision.gameObject.tag == "Keys")
        {
            collision.gameObject.SetActive(false);

            keyCount++;
            keyCounter.text = "Score: " + keyCount;
        }
        if (keyCount >= 1 && collision.gameObject.tag == "Door")
        {
            Destroy(collision.gameObject);
            keyCount--;
        }
    }

    //Level text display
    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(1.3f);
        text.SetActive(false);
    }

    //For conversation 
    private void forConversation()
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

