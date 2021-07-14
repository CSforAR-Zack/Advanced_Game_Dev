using UnityEngine;

public class Mover : MonoBehaviour
{  
    public float moveSpeed = 10f;

    Rigidbody rb = null;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float xValue = Input.GetAxis("Horizontal") * moveSpeed;
        float zValue = Input.GetAxis("Vertical") * moveSpeed;

        rb.velocity = new Vector3(xValue, 0, zValue);
    }
}
