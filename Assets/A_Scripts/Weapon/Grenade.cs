using System.Runtime.CompilerServices;
using UnityEngine;

public class Grenade : Weapon
{
    [SerializeField] private GameObject explosionEffectPrefab;
    GameObject explosionEffect;
    [SerializeField] private Vector3 explosionParticleOffset = new Vector3(0, 1, 0);

    [SerializeField] private float explosionDelay = 3f;
    [SerializeField] private float explosionForce = 700f;
    [SerializeField] private float explosionRadius = 5f;

    private float countdown;
    private bool hasExploded = false;
    [SerializeField] private bool calm = true;

    private void Start()
    {
        countdown = explosionDelay;
    }

    private void Update()
    {
        if (calm == false)
        {
            if (!hasExploded)
            {
                countdown -= Time.deltaTime;
                if (countdown <= 0)
                {
                    Explode();
                    hasExploded = true;
                }
            }
        }
    }

    void Explode()
    {
        if (explosionEffectPrefab)
        {
            explosionEffect = Instantiate(explosionEffectPrefab, transform.position + explosionParticleOffset, Quaternion.identity);

            Destroy(explosionEffect, 4f);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    if (rb.GetComponentInParent<HoomanBehavior>() == true)
                        rb.GetComponentInParent<HoomanBehavior>().GetHit(new Vector3(rb.transform.position.x - this.transform.position.x,
                                                                                     10f,
                                                                                     rb.transform.position.z - this.transform.position.z),
                                                                         explosionForce / 2f);

                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }
        Destroy(gameObject);
    }
    
    public void PickedUp()
    {
        calm = false;
    }
}
