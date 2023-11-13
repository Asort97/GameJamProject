using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour, IDamageble
{

    public float health = 100;
    private float timeToDie = 5f;
    [SerializeField] private Image vinetkaBlood;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Transform swordTransform;
    private Rigidbody2D rb;
    [SerializeField] private float movementSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vinetkaBlood = GameObject.Find("VIN").GetComponent<Image>();
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

            // swordTransform.eulerAngles = new Vector3(0f, 0f, -90f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector2(0, -movementSpeed * Time.deltaTime));

            // swordTransform.eulerAngles = new Vector3(0f, 0f, 90f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector2(movementSpeed * Time.deltaTime, 0));

            bodyTransform.eulerAngles = new Vector2(0, -180f);
            // swordTransform.eulerAngles = new Vector2(0, -180f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector2(-movementSpeed * Time.deltaTime, 0));

            bodyTransform.eulerAngles = new Vector2(0, 0);
            // swordTransform.eulerAngles = new Vector2(0, 0);
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
        health = Mathf.Clamp(health - damage, 0, 100);

        float alpha = 1 - (health / 100);

        vinetkaBlood.color = new Color(1f, 1f, 1f, alpha);

        if(health <= 0f)
        {
            Debug.Log($"Die");
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        ScreenFade.instance.FadeToBlack();
        yield return new WaitForSeconds(2f);
        ScreenFade.instance.FadeFromBlack();
        SceneManager.LoadScene("Menu");
    }
    
    // private void FixedUpdate()
    // {
    //     if (rb.transform.position.x > 17.3)
    //     {
    //         rb.transform.Translate(new Vector2(2, 2));
    //         bodyTransform.Rotate(0, 0, 5);
    //     }
    //     else if(rb.transform.position.x < -17.3)
    //     {
    //         rb.transform.Translate(new Vector2(-2, 2));
    //         bodyTransform.Rotate(0, 0, -5);
    //     }
    //     if(rb.transform.position.y > 12)
    //     {
    //         rb.transform.Translate(new Vector2(0, 2));
    //         bodyTransform.Rotate(0, 0, -5);
    //     }
    //     else if(rb.transform.position.y < - 12)
    //     {
    //         rb.transform.Translate(new Vector2(0, -2));
    //         bodyTransform.Rotate(0, 0, 5);
    //     }
    // }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("DEADZONE"))
        {
            timeToDie -= Time.deltaTime;

            if(timeToDie <= 0f)
            {
                TakeDamage(10000);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("DEADZONE"))
        {
            timeToDie = 5f;
        }
    }
}
