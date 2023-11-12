using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDamageble
{

    private float health = 100;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Transform swordTransform;
    private Rigidbody2D rb;
    [SerializeField] private float movementSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector2(0, movementSpeed * Time.deltaTime));

            swordTransform.eulerAngles = new Vector3(0f, 0f, -90f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector2(0, -movementSpeed * Time.deltaTime));

            swordTransform.eulerAngles = new Vector3(0f, 0f, 90f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector2(movementSpeed * Time.deltaTime, 0));

            bodyTransform.eulerAngles = new Vector2(0, -180f);
            swordTransform.eulerAngles = new Vector2(0, -180f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector2(-movementSpeed * Time.deltaTime, 0));

            bodyTransform.eulerAngles = new Vector2(0, 0);
            swordTransform.eulerAngles = new Vector2(0, 0);
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
            rb.velocity = Vector2.zero;
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
