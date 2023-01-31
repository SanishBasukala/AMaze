using System.Collections;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Tree tree;
    //public tree tree2;
    public bool isActive;
    public bool playerInRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerInRange)
        {

            if (!isActive)
            {
                StartCoroutine(StartMechanism());
            }
            else if (isActive)
            {
                StartCoroutine(MechanismActive());
            }
        }
    }

    private IEnumerator StartMechanism()
    {
        print("started");
        tree.anim.SetBool("isGrowing", false);
        tree.coll.enabled = false;
        //tree2.coll.enabled = false;
        isActive = true;
        yield return new WaitForSeconds(1);
    }

    private IEnumerator MechanismActive()
    {
        print("ended");
        tree.anim.SetBool("isGrowing", true);
        tree.coll.enabled = true;
        //tree2.coll.enabled = true;
        isActive = false;
        yield return new WaitForSeconds(1);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isActive)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isActive)
        {
            playerInRange = false;
        }
    }
}
