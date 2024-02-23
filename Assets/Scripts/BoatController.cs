using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float speed = 5f; // Adjust this to set the speed of the boat
    public float maxRotSpeed = 100f; // Adjust this to set the rotation speed of the boat
    public float thresholdMaxRotSpeed = 1.0f;

    private float _currentRotSpeed = 0f;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        /// Get input axes
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Apply forward or backward force based on vertical input
        _rb.AddForce(transform.up * verticalInput * speed);

        Vector2 vec = Vector2.Reflect(_rb.velocity * -1, transform.up) * 0.02f;
        _rb.velocity += vec;

        Debug.Log(_rb.velocity.magnitude);

        _currentRotSpeed = Mathf.Clamp01(_rb.velocity.magnitude / thresholdMaxRotSpeed) * maxRotSpeed;

        // Apply rotation based on horizontal input
        float rotation = -horizontalInput * _currentRotSpeed * Time.fixedDeltaTime;
        _rb.MoveRotation(_rb.rotation + rotation);
    }
}
