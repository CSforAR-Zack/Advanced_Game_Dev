using UnityEngine;

public class Mover : MonoBehaviour
{  
    public float moveSpeed = 10f;

    void Update()
    {
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        this.transform.Translate(xValue, 0f, zValue);
    }
}
