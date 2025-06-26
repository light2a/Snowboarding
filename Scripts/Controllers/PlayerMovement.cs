// using UnityEngine;

// public class PlayerMovement : MonoBehaviour
// {
//     public GameObject Ground;

//     public float currentSpeed;

//     public FloatVariable TorqueForce;

//     public FloatVariable SpeedNormalizationRate;

//     public FloatVariable MinSpeed;

//     public FloatVariable MaxSpeed;

//     public FloatVariable AccelerationRate;

//     public FloatVariable DefaultSpeed;

//     public BoolVariable IsAlive;

//     private Rigidbody2D rigidBody;
//     private GroundSpeed groundSpeed;

//     private float inputVertical;
//     private float inputHorizontal;
//     private float lastY;

//     private void Awake()
//     {
//         this.rigidBody = this.GetComponent<Rigidbody2D>();
//         // this.groundSpeed = this.Ground.GetComponent<GroundSpeed>();
//         this.IsAlive.SetValue(true);
//     }

//     private void Update()
//     {
//         if(this.IsAlive.Value) {
//             this.inputVertical = Input.GetAxisRaw("Vertical");
//             this.inputHorizontal = Input.GetAxisRaw("Horizontal");
//         }
//     }

//     private void FixedUpdate()
//     {
//         float currentY = transform.position.y;

//         if (IsAlive.Value)
//         {
//             // Detect downhill
//             if (currentY < lastY - 0.01f)
//             {
//                 // Going down — boost speed
//                 float oldSpeed = groundSpeed.GetGroundSpeed();
//                 float newSpeed = Mathf.Clamp(oldSpeed + 1f * Time.fixedDeltaTime, MinSpeed.Value, MaxSpeed.Value);
//                 groundSpeed.SetGroundSpeed(newSpeed);
//             }
//             // Detect uphill
//             else if (currentY > lastY + 0.01f)
//             {
//                 // Going up — slightly reduce speed (optional)
//                 float oldSpeed = groundSpeed.GetGroundSpeed();
//                 float newSpeed = Mathf.Clamp(oldSpeed - 1f * Time.fixedDeltaTime, MinSpeed.Value, MaxSpeed.Value);
//                 groundSpeed.SetGroundSpeed(newSpeed);
//             }
//         }

//         lastY = currentY;


//         float defaultSpeed = this.IsAlive.Value ? this.DefaultSpeed.Value : 0;

//         // Player rotation
//         this.rigidBody.AddTorque(this.inputHorizontal * this.TorqueForce.Value);

//         // User-input induced speed change
//         if (this.inputVertical != 0)
//         {
//             float oldSpeed = currentSpeed;
//             currentSpeed(this.getUpdatedSpeed(this.inputVertical, this.AccelerationRate.Value, currentSpeed, this.MinSpeed.Value, this.MaxSpeed.Value));

//         #if UNITY_EDITOR
//             Debug.Log(string.Format("PlayerMovement.getUpdatedSpeed user input speed change [from: {0}] [to: {1}]", oldSpeed, currentSpeed));
//         #endif
//         }

//         // Speed normalization
//         else if (!this.isDefaultSpeed(currentSpeed, defaultSpeed))
//         {
//             float oldSpeed = currentSpeed;
//             this.groundSpeed.SetGroundSpeed(getNormalizedSpeed(currentSpeed, defaultSpeed, this.SpeedNormalizationRate.Value));
//         #if UNITY_EDITOR
//             Debug.Log(string.Format("PlayerMovement.getNormalizedSpeed [from: {0}] [to: {1}]", oldSpeed, currentSpeed));
//         #endif
//         }

//         // Cleanup
//         this.inputVertical = 0;
//         this.inputHorizontal = 0;
//     }

//     /// <summary>
//     /// Calculate updated speed based on user input not to exceed maxSpeed or to fall below minSpeed.
//     /// </summary>
//     /// <param name="inputVertical"></param>
//     /// <param name="accelerationRate"></param>
//     /// <param name="currentSpeed"></param>
//     /// <param name="minSpeed"></param>
//     /// <param name="maxSpeed"></param>
//     /// <returns></returns>
//     private float getUpdatedSpeed(float inputVertical, float accelerationRate, float currentSpeed, float minSpeed, float maxSpeed)
//     {
//         return Mathf.Min(
//             Mathf.Max(
//                 minSpeed
//                 , currentSpeed + (inputVertical * Time.deltaTime * accelerationRate)
//             )
//             , maxSpeed
//         );
//     }

//     /// <summary>
//     /// Determines if currentSpeed is approximately equal to defaultSpeed.
//     /// </summary>
//     /// <param name="currentSpeed"></param>
//     /// <param name="defaultSpeed"></param>
//     /// <returns>True if the two speeds are approximately equal.</returns>
//     private bool isDefaultSpeed(float currentSpeed, float defaultSpeed)
//     {
//         return Mathf.Approximately(currentSpeed, defaultSpeed);
//     }

//     /// <summary>
//     /// Normalize player's speed back towards defaultSpeed
//     /// </summary>
//     /// /// <param name="currentSpeed"></param>
//     /// <param name="defaultSpeed"></param>
//     /// <param name="speedNormalizationRate"></param>
//     private float getNormalizedSpeed(float currentSpeed, float defaultSpeed, float speedNormalizationRate)
//     {
//         float speedDifferential = (currentSpeed - defaultSpeed) / speedNormalizationRate;
//         return Mathf.Approximately(defaultSpeed, currentSpeed - speedDifferential)
//             ? defaultSpeed
//             : currentSpeed - speedDifferential
//         ;
//     }
// }

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float currentSpeed;

    public FloatVariable TorqueForce;
    public FloatVariable SpeedNormalizationRate;
    public FloatVariable MinSpeed;
    public FloatVariable MaxSpeed;
    public FloatVariable AccelerationRate;
    public FloatVariable DefaultSpeed;

    public BoolVariable IsAlive;

    private Rigidbody2D rigidBody;
    private float inputVertical;
    private float inputHorizontal;
    private float lastY;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        IsAlive.SetValue(true);
        currentSpeed = DefaultSpeed.Value; // Start with default
    }

    private void Update()
    {
        if (IsAlive.Value)
        {
            inputVertical = Input.GetAxisRaw("Vertical");
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            float speed = rigidBody.velocity.magnitude;
        Debug.Log("Current Speed: " + speed); 
        }
    }

    private void FixedUpdate()
    {
        float currentY = transform.position.y;

        if (IsAlive.Value)
        {
            // Downhill = boost
            if (currentY < lastY - 0.01f)
            {
                currentSpeed = Mathf.Clamp(currentSpeed + 1f * Time.fixedDeltaTime, MinSpeed.Value, MaxSpeed.Value);
            }
            // Uphill = reduce
            else if (currentY > lastY + 0.01f)
            {
                currentSpeed = Mathf.Clamp(currentSpeed - 1f * Time.fixedDeltaTime, MinSpeed.Value, MaxSpeed.Value);
            }

            lastY = currentY;

            // Rotation from horizontal input
            rigidBody.AddTorque(-inputHorizontal * TorqueForce.Value);
            float velocityTorque = -rigidBody.velocity.x * 1f;
            rigidBody.AddTorque(velocityTorque);
            // Manual acceleration with vertical input
            if (inputVertical != 0)
            {
                float oldSpeed = currentSpeed;
                currentSpeed = getUpdatedSpeed(inputVertical, AccelerationRate.Value, currentSpeed, MinSpeed.Value, MaxSpeed.Value);

#if UNITY_EDITOR
                // Debug.Log($"[Input Accel] Speed: {oldSpeed:F2} → {currentSpeed:F2}");
#endif
            }
            // Normalize speed when idle
            else if (!isDefaultSpeed(currentSpeed, DefaultSpeed.Value))
            {
                float oldSpeed = currentSpeed;
                currentSpeed = getNormalizedSpeed(currentSpeed, DefaultSpeed.Value, SpeedNormalizationRate.Value);

#if UNITY_EDITOR
                // Debug.Log($"[Normalize] Speed: {oldSpeed:F2} → {currentSpeed:F2}");
#endif
            }

            // Apply movement
            transform.Translate(Vector2.right * currentSpeed * Time.fixedDeltaTime);
            // rigidBody.velocity = Vector2.right * currentSpeed * Time.fixedDeltaTime;

        }

        // Clear input
        inputVertical = 0;
        inputHorizontal = 0;
    }

    private float getUpdatedSpeed(float inputVertical, float accelerationRate, float currentSpeed, float minSpeed, float maxSpeed)
    {
        return Mathf.Clamp(currentSpeed + (inputVertical * Time.deltaTime * accelerationRate), minSpeed, maxSpeed);
    }

    private bool isDefaultSpeed(float currentSpeed, float defaultSpeed)
    {
        return Mathf.Approximately(currentSpeed, defaultSpeed);
    }

    private float getNormalizedSpeed(float currentSpeed, float defaultSpeed, float speedNormalizationRate)
    {
        float diff = (currentSpeed - defaultSpeed) / speedNormalizationRate;
        return Mathf.Approximately(defaultSpeed, currentSpeed - diff)
            ? defaultSpeed
            : currentSpeed - diff;
    }
}
