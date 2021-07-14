using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [Range(-500, 500)] public float xSpinSpeed = 0f;
    [Range(-500, 500)] public float ySpinSpeed = 0f;
    [Range(-500, 500)] public float zSpinSpeed = 0f;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(xSpinSpeed * Time.deltaTime, ySpinSpeed * Time.deltaTime, zSpinSpeed * Time.deltaTime);
    }
}
