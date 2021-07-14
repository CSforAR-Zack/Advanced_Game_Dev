using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        //Debug.Log($"Ouch! The {this.gameObject.name} got in my way!");
        this.GetComponent<Renderer>().material.color = Color.red;
    }
}
