using UnityEngine;

public class CubeSwapper : MonoBehaviour
{
    GameObject target = null;
    GameObject current = null;
    Vector3 targetPosition = new Vector3();
    Vector3 currentPosition = new Vector3();
    float speed = 0f;

    public void Setup(GameObject targetObject, GameObject currentObject, float speedOfSwap)
    {
        this.target = targetObject;
        this.current = currentObject;
        this.speed = speedOfSwap;

        this.targetPosition = new Vector3(targetObject.transform.position.x, currentObject.transform.position.y, 0f);
        this.currentPosition = new Vector3(currentObject.transform.position.x, targetObject.transform.position.y, 0f);

    }    
    
    void Update()
    {
        float step = 20 * 1/speed * Time.deltaTime;

        target.transform.position = Vector3.MoveTowards(target.transform.position, currentPosition, step);
        current.transform.position = Vector3.MoveTowards(current.transform.position, targetPosition, step);

        if((current.transform.position.x - targetPosition.x) < 0.001f)
        {
            Destroy(this.gameObject);
        }        
    }
}
