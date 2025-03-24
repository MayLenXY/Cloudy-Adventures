using UnityEngine;

namespace MyGame.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Движение")]
        [SerializeField] private float moveSpeed = 10f;

        [Header("Границы")]
        [SerializeField] private float edgeSize = 10f;

        [Header("Ограничения передвижения камеры")]
        [SerializeField] private float minX = -50f, maxX = 50f;
        [SerializeField] private float minZ = -50f, maxZ = 50f;

        private void Update()
        {
            Vector3 moveDirection = Vector3.zero;
            Vector3 mousePosition = Input.mousePosition;

            Vector3 right = transform.right;
            Vector3 forward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);

            if (mousePosition.x <= edgeSize) moveDirection -= right;
            else if (mousePosition.x >= Screen.width - edgeSize) moveDirection += right;

            if (mousePosition.y <= edgeSize) moveDirection -= forward;
            else if (mousePosition.y >= Screen.height - edgeSize) moveDirection += forward;

            if (moveDirection == Vector3.zero) return;

            Vector3 newPosition = transform.position + moveDirection.normalized * moveSpeed * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
            transform.position = newPosition;
        }
    }
}
