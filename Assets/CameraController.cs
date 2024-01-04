using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 20.0f; // Adjust the rotation sensitivity.
    public float verticalRotationLimit = 15.0f; // Set the maximum vertical rotation angle.
    public float horizontalRotationLimit = 15.0f; // Set the maximum horizontal rotation angle.

    private float rotationX = 0.0f;
    private bool onPhone = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            onPhone = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onPhone = false;
        }

        if (!onPhone)
        {
            float horizontalMouseMovement = Input.GetAxisRaw("Mouse X");
            float verticalMouseMovement = -Input.GetAxisRaw("Mouse Y");

            // Rotate the camera vertically
            
            rotationX += verticalMouseMovement * sensitivity;
                rotationX = Mathf.Clamp(rotationX, -5, 20);

                transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            // Rotate the player object (parent) horizontally
            float rotationY = transform.parent.localRotation.eulerAngles.y + horizontalMouseMovement * sensitivity;
           

            transform.parent.localRotation = Quaternion.Euler(0, rotationY, 0);
        }
    }
}
