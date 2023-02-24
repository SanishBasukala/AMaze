using UnityEngine;

public class Tree : MonoBehaviour
{
    public BoxCollider2D coll;
    public Animator anim;
    public bool isActive;
    // Start is called before the first frame update
    private void Start()
    {
        coll = gameObject.GetComponent<BoxCollider2D>();
        anim = gameObject.GetComponent<Animator>();

        if (isActive)
        {
            anim.SetBool("Idle", true);
            coll.enabled = true;
        }
        else if (isActive)
        {
            anim.SetBool("Idle", false);
            coll.enabled = false;
        }
    }
}
