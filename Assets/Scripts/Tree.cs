using UnityEngine;

public class Tree : MonoBehaviour
{
    public BoxCollider2D coll;
    public Animator anim;
    // Start is called before the first frame update
    private void Start()
    {
        coll = gameObject.GetComponent<BoxCollider2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
