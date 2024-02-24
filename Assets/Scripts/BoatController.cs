using MoreMountains.Tools;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [Header("Statistics")]
    public float acceleration = 5f;
    public float topForwardSpeed = 10f;
    public float reverseAcceleration = 1.0f;
    public float topReverseSpeed = 4f;
    public float maxRotSpeed = 100f;
    public float thresholdMaxRotSpeed = 1.0f;

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Get input axes
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration
        bool isForward = verticalInput > 0;
        if (isForward)
        {
            if (_rb.velocity.magnitude < topForwardSpeed)
                _rb.AddForce(acceleration * verticalInput * transform.up);
        }
        else
        {
            if (_rb.velocity.magnitude < topReverseSpeed)
                _rb.AddForce(reverseAcceleration * verticalInput * transform.up);
        }

        // Water resistance
        Vector2 vec = Vector2.Reflect(_rb.velocity * -1, transform.up) * 0.02f;
        _rb.velocity += vec;

        // Rotation
        float rotationSpeed = Mathf.Clamp01(_rb.velocity.magnitude / thresholdMaxRotSpeed) * maxRotSpeed;
        float rotation = -horizontalInput * rotationSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, 0, rotation);
    }
}
