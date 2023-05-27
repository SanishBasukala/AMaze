using System.Collections;
using UnityEngine;

public class Bat : Enemy
{
	private Rigidbody2D myrigidbody;
	private Transform target;
	public float chaseRadius;
	public float attackRadius;
	public Transform homePosition;
	public Animator anim;
	// Start is called before the first frame update
	void Start()
	{
		currentState = EnemyState.idle;
		myrigidbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		target = GameObject.FindWithTag("Player").transform;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		CheckDistance();
	}
	private void Update()
	{
		if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
		{
			if (transform.position.x > target.position.x)
			{
				transform.localScale = new Vector3(-1, 1, 1);
			}
			if (transform.position.x < transform.position.x)
			{
				transform.localScale = new Vector3(1, 1, 1);
			}
		}
	}

	void CheckDistance()
	{
		if (Vector3.Distance(target.position, transform.position) <= chaseRadius &&
			Vector3.Distance(target.position, transform.position) > attackRadius)
		{
			if (currentState == EnemyState.idle ||
				currentState == EnemyState.walk &&
				currentState != EnemyState.stagger)
			{
				Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
				ChangeAnim(temp - transform.position);
				myrigidbody.MovePosition(temp);
				ChangeState(EnemyState.walk);
				anim.SetBool("moving", true);
			}
		}
		else if (Vector3.Distance(target.position, transform.position) <= chaseRadius &&
			Vector3.Distance(target.position, transform.position) <= attackRadius)
		{
			if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
			{
				StartCoroutine(AttackCo());
			}
		}
	}

	private IEnumerator AttackCo()
	{
		ChangeState(EnemyState.attack);
		anim.SetBool("attacking", true);
		audioSource.PlayOneShot(attackClip);
		yield return new WaitForSeconds(1f);
		ChangeState(EnemyState.walk);
		anim.SetBool("attacking", false);
	}
	private void SetAnimFloat(Vector2 setVector)
	{
		anim.SetFloat("moveX", setVector.x);
		anim.SetFloat("moveY", setVector.y);

	}
	private void ChangeAnim(Vector2 direction)
	{
		if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
		{
			if (direction.x > 0)
			{
				SetAnimFloat(Vector2.right);
			}
			else if (direction.x < 0)
			{
				SetAnimFloat(Vector2.left);
			}
		}
	}

	private void ChangeState(EnemyState newState)
	{
		if (currentState != newState)
		{
			currentState = newState;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

			playerHealth.TakeDamage(1);
		}
	}
}
