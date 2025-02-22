using Unity.VisualScripting;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HoomanBehavior : MonoBehaviour
{
    GameObject playerObj;

    public Transform head;
    public Transform player; // target(player)
    public Camera playerCam;
    public float maxSpeed = 3f;
    public float acceleration = 1f;
    public float rotateSpeed = 2f;

    Vector3 moveDirection;

    // difficulty varies
    public static float hoomanResponseTime;

    // add this to also difficulty setting?
    public Slider HPslider;
    [SerializeField]
    private float HealthPoint = 100;

    private Rigidbody rb; // Rigidbody for physics
    private float currentSpeed = 0f;

    public bool isKnockedBack = false; // Is object getting hit?

    Vector3 directionToPlayer;
    Quaternion targetRotation;
    float headTiltX, tempX, tempY, tempZ;

    // testing
    int hitCount = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (player == null)
        {
            // Find player automatically by FindWithTag
            playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }

            if (playerCam == null)
            {
                playerCam = Camera.main;
            }
        }
    }

    void FixedUpdate()
    {
        // without this line the hitting logic doesn't work
        if (isKnockedBack) return; // ignore original behaviour if it is already hitted

        if (player == null) return;

        if (HPslider != null) HPslider.value = HealthPoint;

#region Head spinning logic
        // Spin their head towards player
        directionToPlayer = player.position - head.position;
        targetRotation = Quaternion.LookRotation(directionToPlayer);

        // Add an upward offset to make the Hooman look up slightly
        directionToPlayer.y += 0.5f; // Adjust this value for the amount of "looking up"

        // Check head tilt
        headTiltX = Quaternion.Angle(Quaternion.identity, head.localRotation);
        tempX = head.localPosition.x;
        tempY = head.localPosition.y;
        tempZ = head.localPosition.z;

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

        HPslider.transform.rotation = Quaternion.LookRotation(transform.position - playerCam.transform.position);

        // Movement logic
        moveDirection = (player.position - transform.position).normalized;

        currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        rb.linearVelocity = moveDirection * currentSpeed;
    }

#region Get Hurt logic
    public void GetHit(Vector3 hitDirection, float hitForce)
    {
        if (isKnockedBack) return; // ignore original behaviour if it is already hitted

        isKnockedBack = true;

        rb.linearVelocity = hitDirection.normalized * hitForce;

        // HealthPoint -= hitForce;

        if (HealthPoint <= 0)
        {
            // maybe some visual effect?
            //Destroy(this);
        }

        if (hitCount == 0)
        {
            Destroy(this.gameObject);
        }

    }
#endregion

#region Get Up logic
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
        hitCount--;
    }
#endregion
}