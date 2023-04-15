using UnityEngine;
using Vector2 = UnityEngine.Vector2;
public class EnemyMovement : MonoBehaviour
{
	public Transform[] patrolPoints;
	public float moveSpeed;
	public int patrolDestination;

	void Update()
	{
		if (patrolDestination == 0)
		{
			transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
			if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
			{
				transform.localScale = new Vector3(1, 1, 1);
				patrolDestination = 1;
			}
		}
		if (patrolDestination == 1)
		{
			transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
			if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
			{
				transform.localScale = new Vector3(-1, 1, 1);
				patrolDestination = 0;
			}
		}
	}
}

