using UnityEngine;

public class TurretRotateBlu : MonoBehaviour
{
    public float RotationSpeed = 5f;

    private Vector3 lastDirection = Vector3.forward;

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("HorizontalJoy1");
        float verticalInput = Input.GetAxisRaw("VerticalJoy1");

        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            Vector3 direction = new Vector3(horizontalInput, 0, -verticalInput);
            lastDirection = direction.normalized;
        }

        Quaternion targetRotation = Quaternion.LookRotation(lastDirection, Vector3.up) * Quaternion.Euler(0, 90, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
    }
}