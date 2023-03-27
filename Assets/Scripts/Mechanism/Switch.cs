using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public List<BoxCollider2D> coll;
    public List<Animator> anim;
    //public tree tree2;
    public bool isActive;
    public bool playerInRange;
    [SerializeField]
    private AudioSource audioSource;

    private void Update()
    {
        for (int i = 0; i < coll.Count; i++)
        {
            AnimatorStateInfo state = anim[i].GetCurrentAnimatorStateInfo(0);
            string animationName = state.shortNameHash.ToString();
            if (animationName == "2081823275")
            {
                audioSource.Play();
                coll[i].enabled = true;
            }
            else if (animationName == "-1736577384")
            {
                audioSource.Play();
                coll[i].enabled = false;
            }
        }

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
        for (int i = 0; i < coll.Count; i++)
        {
            anim[i].SetTrigger("ChangeState");
            coll[i].enabled = false;
        }

        isActive = true;
        yield return new WaitForSeconds(1);
    }

    private IEnumerator MechanismActive()
    {
        for (int i = 0; i < coll.Count; i++)
        {
            anim[i].SetTrigger("ChangeState");
            coll[i].enabled = true;
        }
        isActive = false;
        yield return new WaitForSeconds(1);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            playerInRange = false;
        }
    }
}
