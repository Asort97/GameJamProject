using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDamageble
{

    private float health = 100;
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private float movementSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.transform.Translate(new Vector2(0, movementSpeed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.transform.Translate(new Vector2(0, -movementSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.transform.Translate(new Vector2(movementSpeed * Time.deltaTime, 0));
           
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.transform.Translate(new Vector2(-movementSpeed * Time.deltaTime, 0));
            
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

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Debug.Log($"Die");
        }
    }

}
