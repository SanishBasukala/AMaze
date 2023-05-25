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

	//for projectile
	public GameObject projectile;

	public PlayerHealth playerHealth;

	public AudioSource audioSource;
	[SerializeField]
	private AudioSource walkAudio;
	[SerializeField]
	private AudioClip gainHeartClip;
	[SerializeField]
	private AudioClip attackClip;
	[SerializeField]
	private AudioClip keyClip;
	[SerializeField]
	private AudioClip doorClip;

	private bool canShoot = true;
	public float arrowCooldown = 0.5f;
	private void Start()
	{
		myRigidbody = GetComponent<Rigidbody2D>();

		currentState = PlayerState.walk;
		animator = GetComponent<Animator>();

		animator.SetFloat("moveX", 0);
		animator.SetFloat("moveY", -1);
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.U))
		{
			PopUpDialog.Instance.ShowDialog("this is it", () =>
			{
				Debug.Log("Yes");
			}, () =>
			{
				Debug.Log("No");
			});
		}
		if (change != Vector3.zero)
		{
			currentState = PlayerState.walk;
			if (!walkAudio.isPlaying)
			{
				walkAudio.Play();
			}
		}
		else if (change == Vector3.zero)
		{
			currentState = PlayerState.idle;
			walkAudio.Stop();
		}

		try
		{
			if (DialogManager.isActive == true)
				return;
			PlayerMovementAndAttack();
		}
		catch (NullReferenceException)
		{
			PlayerMovementAndAttack();
		}
	}

	private void PlayerMovementAndAttack()
	{
		change = Vector3.zero;
		change.x = Input.GetAxisRaw("Horizontal");
		change.y = Input.GetAxisRaw("Vertical");
		if (change != Vector3.zero && currentState != PlayerState.stagger)
		{
			MoveCharacter();
		}
		if (Input.GetButtonDown("attack") &&
			currentState != PlayerState.attack &&
			currentState != PlayerState.stagger &&
			currentState != PlayerState.walk)
		{

			StartCoroutine(AttackCo());
		}
		else if (Input.GetButtonDown("Second Weapon") &&
			currentState != PlayerState.attack &&
			currentState != PlayerState.stagger)
		{
			StartCoroutine(SecondAttackCo());
		}
		else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
		{
			UpdateAnimationAndMove();
		}
	}
	//attack animation wid delay
	private IEnumerator AttackCo()
	{
		animator.SetBool("attacking", true);
		audioSource.PlayOneShot(attackClip);
		Debug.Log(audioSource);
		currentState = PlayerState.attack;
		yield return null; //wait one frame
		animator.SetBool("attacking", false);
		yield return new WaitForSeconds(.3f);
		currentState = PlayerState.walk;
	}

	private IEnumerator SecondAttackCo()
	{
		if (!canShoot)
			yield break; // Exit the coroutine if shooting is on cooldown

		currentState = PlayerState.attack;
		yield return null; // Wait one frame
		MakeArrow();
		canShoot = false; // Disable shooting temporarily
		yield return new WaitForSeconds(arrowCooldown);
		canShoot = true; // Enable shooting again
		currentState = PlayerState.walk;
	}

	private void MakeArrow()
	{
		if (!canShoot)
			return; // Exit the method if shooting is on cooldown

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
		// increase key count and destroy key
		if (collision.gameObject.CompareTag("BlueKey"))
		{
			Destroy(collision.gameObject);
			PlayClip("key");
			blueKeyCount++;
			DisplayKeyCount(blueKeyCounter, blueKeyCount);
		}
		if (collision.gameObject.CompareTag("RedKey"))
		{
			Destroy(collision.gameObject);
			PlayClip("key");
			redKeyCount++;
			DisplayKeyCount(redKeyCounter, redKeyCount);
		}
		if (collision.gameObject.CompareTag("YellowKey"))
		{
			Destroy(collision.gameObject);
			PlayClip("key");
			yellowKeyCount++;
			DisplayKeyCount(yellowKeyCounter, yellowKeyCount);
		}

		// decrease key count and destroy door
		if (blueKeyCount >= 1 && collision.gameObject.CompareTag("BlueDoor"))
		{
			Destroy(collision.gameObject);
			PlayClip("door");
			blueKeyCount--;
			DisplayKeyCount(blueKeyCounter, blueKeyCount);
		}
		if (redKeyCount >= 1 && collision.gameObject.CompareTag("RedDoor"))
		{
			Destroy(collision.gameObject);
			PlayClip("door");
			redKeyCount--;
			DisplayKeyCount(redKeyCounter, redKeyCount);
		}
		if (yellowKeyCount >= 1 && collision.gameObject.CompareTag("YellowDoor"))
		{
			Destroy(collision.gameObject);
			PlayClip("door");
			yellowKeyCount--;
			DisplayKeyCount(yellowKeyCounter, yellowKeyCount);
		}

		// increase player hearts and destroy heart
		if (collision.gameObject.CompareTag("Heart"))
		{
			PlayClip("heart");
			Destroy(collision.gameObject);
			playerHealth.GainHealth();
		}
	}
	public void DisplayKeyCount(Text keyCounter, int keyCount) => keyCounter.text = keyCount.ToString();
	public void PlayClip(string objectName)
	{
		if (objectName == "key")
		{
			audioSource.PlayOneShot(keyClip);
		}
		else if (objectName == "door")
		{
			audioSource.PlayOneShot(doorClip);
		}
		else if (objectName == "heart")
		{
			audioSource.PlayOneShot(gainHeartClip);
		}
	}
}
