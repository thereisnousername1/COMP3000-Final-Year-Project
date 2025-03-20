using System.Collections.Generic;
using UnityEngine;

// to be mapped to the food item
public class FoodItemBehaviour : MonoBehaviour
{
    //public Attributes foodItem; // Reference to the corresponding ScriptableObject
    
    public List<Attribute> foodItems; // Reference to the corresponding ScriptableObject

    // Sound
    [SerializeField] private GameObject audioSourcePrefab;
    
    [SerializeField] private AudioClip impactSound;
    
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void PlaySoundAtPosition(AudioClip clip)
    {
        GameObject audioSourceObject = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
        AudioSource instantiateAudioSource = audioSourceObject.GetComponent<AudioSource>();
        instantiateAudioSource.clip = clip;
        instantiateAudioSource.spatialBlend = 1;
        instantiateAudioSource.Play();

        Destroy(audioSourceObject, instantiateAudioSource.clip.length);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (audioSource)
        {
            audioSource.clip = impactSound;

            audioSource.spatialBlend = 1;

            audioSource.Play();
        }
    }
}