using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// This is a class for multiple cutscenes management
/// </summary>
public class Triggering : MonoBehaviour
{
    // Every PlayableDirector is a single scene
    // In this case every game object and their motions can be stored within 1 PlayableDirector
    // No need to create multiple animation tree
    // To be play by script, mapped multiple cutscenes into this script
    // And call any PlayableDirector you need

    // To edit a timeline in unity it is always recommended to open the Timeline panel and Animation panel together
    // So you need a ong yee yee ass screen
    // Ya

    public List<GameObject> scenes;
    public List<PlayableDirector> Timelines;
    // public PlayableDirector Timeline;
    // public List<Cutscene> cutscenes;

    void Start()
    {
        foreach (GameObject scene in scenes)
        {
            Timelines.Add(scene.GetComponent<PlayableDirector>());
        }

        foreach (PlayableDirector timeline in Timelines)
        {
            timeline.Play();
        }
    }

    /*
    public void play()
    {
        Timeline.Play();
    }

    public void stop()
    {
        Timeline.Stop();
    }
    */

    public void play()
    {
        // somehow find the specific index of the scene, set active and then play
        // Timeline.Play();
    }

    public void stop()
    {
        // somehow find the specific index of the scene, stop and then deactivate
        // Timeline.Stop();
    }
}

/*
// geez why am I being so smart using a far more complicated method while I can simply find opponent in an game object
// geezus
[CreateAssetMenu(fileName = "Cutscene", menuName = "Scriptable Objects/Cutscene")]
public class Cutscene : ScriptableObject
{
    public PlayableDirector Timeline;
    public GameObject scene;

    public void Active()
    {
        scene.SetActive(true);
        Timeline.Play();
    }
}
*/