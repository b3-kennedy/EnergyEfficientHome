using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 10.0f; 
    public float verticalRotationLimit = 715.0f; 

    private float rotationX = 0.0f;
    private bool onPhone = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            onPhone=true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onPhone = false;
        }
        if (!onPhone)
        {
            float horizontalMouseMovement = Input.GetAxisRaw("Mouse X");
            float verticalMouseMovement = -Input.GetAxisRaw("Mouse Y");
           
                transform.Rotate(Vector3.up * horizontalMouseMovement * sensitivity, Space.World);

                rotationX += verticalMouseMovement * sensitivity;
                rotationX = Mathf.Clamp(rotationX, -verticalRotationLimit, verticalRotationLimit);
                transform.parent.Rotate(Vector3.up * horizontalMouseMovement * sensitivity, Space.World);

                transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            
          
               
        }
        
       
    }
}
