using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform playerTransform;
    [Header("Camera Settings")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -10f);
    [SerializeField] private Vector3 rotationAngles = new Vector3(15f, 0f, 0f);
    [SerializeField] private float followSpeed = 2f;
    [SerializeField] private float lookAtSpeed = 2f;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        Invoke("FindPlayer", 0.1f);
        if (playerTransform != null)
        {
            UpdateCameraPositionAndRotation(true);
        }
    }

    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            UpdateCameraPositionAndRotation(false);
        }
    }

    void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("CameraTarget");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void UpdateCameraPositionAndRotation(bool immediate)
    {
        Vector3 desiredPosition = playerTransform.position + offset;

        if (immediate)
        {
            transform.position = desiredPosition;
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 1f / followSpeed);
        }
        Quaternion desiredRotation = Quaternion.Euler(rotationAngles) * Quaternion.LookRotation(playerTransform.position - transform.position);

        if (immediate)
        {
            transform.rotation = desiredRotation;
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, lookAtSpeed * Time.deltaTime);
        }
    }
}
