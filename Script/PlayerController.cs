using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmout = 1f;
    Rigidbody2D rb2d;
    
    void Start()
    {
         rb2d = GetComponent<Rigidbody2D>();

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
    }
}
