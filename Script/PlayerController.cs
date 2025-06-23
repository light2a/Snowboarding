using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmout = 1f;
    [SerializeField] float boostSpeed = 30f;

    [SerializeField] float baseSpeed = 20f;
    Rigidbody2D rb2d;
    SurfaceEffector2D surfaceEffector2D;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    void Update()
    {
        RotatePlayer();
        RespondToBoost();
    }

    private void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }
    
    private void RotatePlayer()
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
