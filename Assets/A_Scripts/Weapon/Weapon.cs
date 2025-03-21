using UnityEngine;
using UnityEngine.Audio;

public class Weapon : MonoBehaviour
{
    public float hitForce = 10f;

    // Sound
    [SerializeField] private GameObject audioSourcePrefab;
    [SerializeField] private AudioClip impactSound;
    [SerializeField] private AudioClip hitSound;    // for grenade this will be the same as impact sound
    private AudioSource audioSource;

    HoomanBehavior hooman;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (audioSource)
        {
            audioSource.clip = impactSound;

            audioSource.spatialBlend = 1;

            audioSource.Play();
        }

        // Compare if the tag matched Hooman
        if (collision.gameObject.CompareTag("Hooman"))
        {
            // Grab the object's HoomanBehavior script
            hooman = collision.gameObject.GetComponent<HoomanBehavior>();
            if (hooman != null)
            {
                // Hit force calculation
                Vector3 hitDirection = collision.contacts[0].point - transform.position;
                hooman.GetHit(hitDirection, hitForce);

                if (audioSource)
                {
                    audioSource.clip = hitSound;

                    audioSource.spatialBlend = 1;

                    audioSource.Play();
                }
            }
        }
    }
}
