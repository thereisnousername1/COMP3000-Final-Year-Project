using Oculus.Interaction;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Meteor : Weapon
{
    
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] private Vector3 explosionParticleOffset = new Vector3(0, 1, 0);

    [SerializeField] private float explosionForce = 700f;
    [SerializeField] private float explosionRadius = 5f;

    private bool hasExploded = false;

    public void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded)
        {
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; 

            if (collision.gameObject.CompareTag("Floor"))
            {
                Explode();
                hasExploded = true;
            }
        }
    }

    void Explode()
    {
        if (explosionEffectPrefab)
        {
            GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position + explosionParticleOffset, Quaternion.identity);

            Destroy(explosionEffect, 4f);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.gameObject.GetComponent<Weapon>() == null || nearbyObject.gameObject.GetComponent<Meteor>() == true)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    //if (rb.GetComponentInParent<HoomanBehavior>() == true)
                    //    rb.GetComponentInParent<HoomanBehavior>().GetHit(new Vector3(rb.transform.position.x - this.transform.position.x,
                    //                                                                 10f,
                    //                                                                 rb.transform.position.z - this.transform.position.z),
                    //                                                     explosionForce / 2f);

                    rb.constraints = RigidbodyConstraints.None;

                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }
    }
}
