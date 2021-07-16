using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine = null;
    [SerializeField] ParticleSystem mainThrustParticles = null;
    [SerializeField] ParticleSystem leftThrustParticles = null;
    [SerializeField] ParticleSystem rightThrustParticles = null;


    // CACHE
    Rigidbody rb = null;
    AudioSource audioSource = null;

    // STATE

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StopRotating()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainThrustParticles.isPlaying) mainThrustParticles.Play();
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);

        if (!leftThrustParticles.isPlaying) leftThrustParticles.Play();
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);

        if (!rightThrustParticles.isPlaying) rightThrustParticles.Play();
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainThrustParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing rotation so we can manually rotate

        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);

        // Reset constraints to original
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        
    }
}
