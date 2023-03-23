using UnityEngine;
using Vector2 = UnityEngine.Vector2;
public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    //public Transform playerTransform;
    //public bool isChasing;
    //public float chaseDistance;

    // Update is called once per frame
    void Update()
    {
        //if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
        //{
        //    isChasing = true;
        //    if (isChasing)
        //    {
        //        if (transform.position.x > playerTransform.position.x)
        //        {
        //            transform.localScale = new Vector3(-1, 1, 1);
        //            transform.position += moveSpeed * Time.deltaTime * Vector3.left;
        //        }
        //        if (transform.position.x < playerTransform.position.x)
        //        {
        //            transform.localScale = new Vector3(1, 1, 1);
        //            transform.position += moveSpeed * Time.deltaTime * Vector3.right;
        //        }
        //    }
        //}
        //else
        //{
        //isChasing = false;
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
        //}

    }
}

