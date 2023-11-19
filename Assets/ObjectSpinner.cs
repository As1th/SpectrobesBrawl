using UnityEngine;

public class ObjectSpinner : MonoBehaviour
{
    // Adjust this variable to control the rotation speed
    public float rotationSpeed = 50.0f;

    void Update()
    {
        // Rotate the object around its up (Y) axis
        transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
    }
}
