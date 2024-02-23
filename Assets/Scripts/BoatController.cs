using System;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float speed = 5f; // Adjust this to set the speed of the boat
    public float rotationSpeed = 100f; // Adjust this to set the rotation speed of the boat

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Get input axes
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Apply forward or backward force based on vertical input
        rb.AddForce(transform.up * verticalInput * speed);

        Vector2 vec = Vector2.Reflect(rb.velocity * -1, transform.up) * 0.02f;
        rb.velocity += vec;

        // Apply rotation based on horizontal input
        float rotation = -horizontalInput * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotation);
    }
}
