using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : MonoBehaviour
{
    public float mainThrust = 1000f;
    public float rotationThrust = 100f;
    public ParticleSystem mainThrustParticles = null;

    Rigidbody rb = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();
        }
    }

    void StopThrusting()
    {
       // mainThrustParticles.Stop();
        mainThrustParticles.Emit(0);
    }

    void RotateLeft()
    {
        ApplyRotation(1);
    }

    void RotateRight()
    {
        ApplyRotation(-1);
    }

    void ApplyRotation(int direction)
    {
        rb.freezeRotation = true;

        this.transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime * direction);

        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        
        mainThrustParticles.Emit(1);
    }
}
