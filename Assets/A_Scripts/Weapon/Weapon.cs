using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float hitForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        // Compare if the tag matched Hooman
        if (collision.gameObject.CompareTag("Hooman"))
        {
            // Grab the object's HoomanBehavior script
            HoomanBehavior hooman = collision.gameObject.GetComponent<HoomanBehavior>();
            if (hooman != null)
            {
                // Hit force calculation
                Vector3 hitDirection = collision.contacts[0].point - transform.position;
                hooman.GetHit(hitDirection, hitForce);
            }
        }
    }
}
