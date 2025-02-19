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

    public List<PlayableDirector> Timeline;
    // public PlayableDirector Timeline;

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
}
