using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmout = 1f;
    [SerializeField] float normalSpeed = 35f;
    [SerializeField] float boostedSpeed = 25f;
    Rigidbody2D rb2d;
    float currentSpeed;
    bool isGrounded = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    
    void Start()
    {
         rb2d = GetComponent<Rigidbody2D>();
         currentSpeed = normalSpeed;
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(torqueAmout);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddTorque(-torqueAmout);
        }
        else if (Input.GetKey(KeyCode.UpArrow) && isGrounded && SceneManager.GetActiveScene().name == "Level2")
        {
            rb2d.linearVelocity = new Vector2(35, 15);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Object"))
        {
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Object"))
        {
            currentSpeed = normalSpeed;
        }
    }
}
