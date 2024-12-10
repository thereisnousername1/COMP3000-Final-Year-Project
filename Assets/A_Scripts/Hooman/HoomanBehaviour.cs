using Unity.VisualScripting;
using UnityEngine;

public class HoomanBehavior : MonoBehaviour
{
    public Transform head;
    public Transform player; // target(player)
    public float maxSpeed = 3f;
    public float acceleration = 1f;
    public float rotateSpeed = 2f;

    private Rigidbody rb; // Rigidbody for physics
    private float currentSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (player == null)
        {
            // Find player automatically by FindWithTag
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Spin their head towards player
        Vector3 directionToPlayer = player.position - head.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        // Add an upward offset to make the Hooman look up slightly
        directionToPlayer.y += 0.5f; // Adjust this value for the amount of "looking up"

        // Check head tilt
        float headTiltX = Quaternion.Angle(Quaternion.identity, head.localRotation);
        float tempX = head.localPosition.x;
        float tempY = head.localPosition.y;
        float tempZ = head.localPosition.z;

        if (head.localPosition.x != tempX || head.localPosition.y != tempY)
            head.localPosition = Vector3.Lerp(
                    head.localPosition,
                    new Vector3(tempX, tempY, head.localPosition.z),
                    Time.deltaTime * rotateSpeed);
        if (headTiltX > 5f)
        {
            // Move the head forward from -0.25f to -0.15f
            float targetZ = -0.15f;
            if (headTiltX > 20f)
                targetZ = -0.05f;
            if (headTiltX > 35f)
                targetZ = 0.05f;

            if (head.localPosition.z < targetZ)
            {
                head.localPosition = Vector3.Lerp(
                    head.localPosition,
                    new Vector3(head.localPosition.x, head.localPosition.y, targetZ),
                    Time.deltaTime * rotateSpeed);
            }
        }
        else
        {
            head.localPosition = Vector3.Lerp(
                    head.localPosition,
                    new Vector3(head.localPosition.x, head.localPosition.y, tempZ),
                    Time.deltaTime * rotateSpeed);
        }
        head.rotation = Quaternion.Slerp(head.rotation, targetRotation, Time.deltaTime * rotateSpeed);

        // Movement logic
        Vector3 moveDirection = (player.position - transform.position).normalized;
        currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        rb.linearVelocity = moveDirection * currentSpeed;
    }
}
