using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private float movementSpeed;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            rb.transform.Translate(new Vector2(0, movementSpeed));
           
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.transform.Translate(new Vector2(0, -movementSpeed));
           
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.transform.Translate(new Vector2(movementSpeed, 0));
           
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.transform.Translate(new Vector2(-movementSpeed, 0));
            
        }
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

    }
}
