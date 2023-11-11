using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private GameObject Character; 

    private Rigidbody2D rb;
    [SerializeField] private float movementSpeed;
    private void Awake() 
    { 
        Character = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        rb = Character.GetComponent<Rigidbody2D>();
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

    }
}
