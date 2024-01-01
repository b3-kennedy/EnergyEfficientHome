using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 2.0f; // Adjust the rotation sensitivity.
    public float verticalRotationLimit = 80.0f; // Set the maximum vertical rotation angle.
    public float horizontalRotationLimit = 90.0f; // Set the maximum horizontal rotation angle.

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

            float rotationY = transform.parent.localRotation.eulerAngles.y;
            transform.parent.localRotation = Quaternion.Euler(0, rotationY, 0);
        }
        
       
    }
}
