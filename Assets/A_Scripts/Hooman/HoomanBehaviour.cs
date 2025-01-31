using Oculus.Interaction;
using System;
using System.Collections;
using UnityEngine;

public class HoomanBehavior : MonoBehaviour
{
    public Transform head;
    public Transform player; // target(player)
    public float maxSpeed = 3f;
    public float acceleration = 1f;
    public float rotateSpeed = 2f;

    Vector3 moveDirection;

    // difficulty varies
    public static float hoomanResponseTime;

    private Rigidbody rb; // Rigidbody for physics
    private float currentSpeed = 0f;

    public bool isKnockedBack = false; // Is object getting hit?

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
        // without this line the hitting logic doesn't work
        if (isKnockedBack) return; // ignore original behaviour if it is already hitted

        if (player == null) return;

#region Head spinning logic
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
            // Move the head forward from -0.15f to -0.05f
            if (headTiltX > 20f)
                targetZ = -0.05f;
            // Move the head forward from -0.05f to 0.05f
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
#endregion

        // Movement logic
        moveDirection = (player.position - transform.position).normalized;

        currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        rb.linearVelocity = moveDirection * currentSpeed;
    }

    public void GetHit(Vector3 hitDirection, float hitForce)
    {
        if (isKnockedBack) return; // ignore original behaviour if it is already hitted

        isKnockedBack = true;

        rb.linearVelocity = hitDirection.normalized * hitForce;
    }
    public void OnCollisionEnter(Collision collision)
    {
        /*
        if (isKnockedBack && collision.gameObject.CompareTag("Floor"))
        {
            // return to normal
            // isKnockedBack = false;
            // rb.linearVelocity = Vector3.zero;
            
            StartCoroutine(GetUp());
        }
        */

        if (collision.gameObject.CompareTag("Floor"))
        {

            if (isKnockedBack)
            {
                // return to normal
                // isKnockedBack = false;
                // rb.linearVelocity = Vector3.zero;

                StartCoroutine(GetUp());
            }
        }
    }

    IEnumerator GetUp()
    {
        yield return new WaitForSeconds(hoomanResponseTime);

        // return to normal
        isKnockedBack = false;
        rb.linearVelocity = Vector3.zero;
    }
}
