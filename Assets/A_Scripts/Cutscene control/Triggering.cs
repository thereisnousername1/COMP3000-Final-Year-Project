using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// This is a class for multiple cutscenes management
/// 
/// Transition is a scene for transit(wow you don't say)
/// it store/get the next scene, which is where the actual game take place
/// it allows player to view/review seen/ unseen cutscene(very beautiful cutscene made by me, geez you should thank me)
/// 
/// Depending on the SceneTransitionManger.TargetScene(static)
/// the woosh pillar(it performs magic by making player vanish so I call it woosh pillar)
/// add a listener to the button on top of it, so that player can travel to the desired scene(SceneTransitionManger.TargetScene)
/// also the text change depends on the SceneTransitionManger.TargetScene
/// 
/// geez you should really thank me
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

    // this way I can still explore the index by their gameobject sequence, smart way to use static (or just regular way?)
    public List<GameObject> scenes;
    public List<PlayableDirector> Timelines;
    public PlayableDirector CurrentTimeline;
    public static int Index = 0;
    public string NextScenetoGo = null;

    public GameObject WooshPillar;

    [SerializeField]
    private FadeScreen fadeScreen;

    // initialization and receiving data take place
    void Start()
    {
        foreach (GameObject scene in scenes)
        {
            Timelines.Add(scene.GetComponent<PlayableDirector>());
        }

        CurrentTimeline = Timelines[Index];

        // when the player just start a new game in the startpage or comes from other level(with TargetScene)
        // by default play the timeline (defined in the EventSystem)
        
        // Debug.Log(WooshPillar.GetComponentInChildren<XRSimpleInteractable>().selectEntered.ToString());

        // Scoring.End reset in ExitChecking and goBack function
        if (Scoring.End != true)    // reach an end
        {
            if (SceneTransitionManager.TargetScene == null) // pressed cutscene button at startpage
            {
                NextScenetoGo = "Level 1";
                WooshPillar.GetComponentInChildren<Text>().text = "Begin at " + NextScenetoGo;
            }

            // when player start a new game in startpage scene, at this point the SceneTransitionManager.TargetScene should be != null
            if (SceneTransitionManager.TargetScene == "Level 1")
            {
                // pressed start button at startpage
                CurrentTimeline.Play();

                NextScenetoGo = SceneTransitionManager.TargetScene;
                WooshPillar.GetComponentInChildren<Text>().text = "Begin at " + NextScenetoGo;
            }

            /*
            if (Index == 0)
            {
                // pressed start button at startpage
                CurrentTimeline.Play();

                // when player start a new game in startpage scene, at this point the SceneTransitionManager.TargetScene should be != null
                NextScenetoGo = SceneTransitionManager.TargetScene;
                WooshPillar.GetComponentInChildren<Text>().text = "Begin at " + NextScenetoGo;
            }
            else
            {
                NextScenetoGo = SceneTransitionManager.TargetScene;
                WooshPillar.GetComponentInChildren<Text>().text = "Retry at " + NextScenetoGo;
            }
            */

            // if SceneTransitionManager.TargetScene == ... in the future
            // InputCutsceneIndex(<desired transition cutscene between 2 levels(if any)>)
        }
        else
        {
            CurrentTimeline.Play(); // play ending cutscene

            NextScenetoGo = null;
            WooshPillar.GetComponentInChildren<Text>().text = "Geez, you can't play this game";
        }

        WooshPillar.GetComponentInChildren<XRSimpleInteractable>().selectEntered.AddListener(WooshPillar_Button_Selected);
    }

    /*
    public void play()
    {
        // somehow find the specific index of the scene, set active and then play
        // Timeline.Play();
        foreach (PlayableDirector timeline in Timelines)
        {
            timeline.Play();
        }
    }

    public void stop()
    {
        // somehow find the specific index of the scene, stop and then deactivate
        // Timeline.Stop();
    }
    */

    // receive data from outside (i.e. scoring.cs, it decide what cutscene to play in script, just in case)
    public static void InputCutsceneIndex(int index)
    {
        /// Introduction
        /// 0: Intro
        /// 
        /// Bad Endings
        /// 1: Died from starvation
        /// 2: Died from obesity
        /// 
        /// Good Endings
        /// 
        Index = index;
    }

#region Scene Selector and Woosh Pillar logic

    // First off startpage will sent a target scene to the triggering script
    // and then NextScenetoGo will store a value
    // the scene selector will also send the name to this function
    // so this is the first function that actually happened in the transition scene(so far at this point 22/2/2025)
    //
    // name in here equals to the scene name
    public void InputDesiredScene(string name)
    {
        NextScenetoGo = name;
        WooshPillar.GetComponentInChildren<Text>().text = "Begin at " + NextScenetoGo;
    }

    private void WooshPillar_Button_Selected(SelectEnterEventArgs arg0)
    {
        if (NextScenetoGo != null)
        {
            InputCutsceneIndex(0);
            StartCoroutine(FadeScreenAnimation());
            SceneManager.LoadScene(NextScenetoGo);
            NextScenetoGo = null;
        }
        else
            Debug.Log("No target scene selected!");
    }

    IEnumerator FadeScreenAnimation()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // set FadeScreen to inactive, to prevent blocking ray interactors and UI canvas
        fadeScreen.gameObject.SetActive(false);

    }
#endregion

#region Actions for Spatial video Player (Ya it is a genius idea, I hope one day I can recreate this in apple vision pro, watching real life spatial video)
    // The following functions can be called only in the Transition scene
    public void play()
    {
        // somehow find the specific index of the scene, set active and then play
        CurrentTimeline = Timelines[Index];
        CurrentTimeline.Play();
    }

    public void pause()
    {
        CurrentTimeline.Pause();
    }

    public void stop()
    {
        // stop and then deactivate
        CurrentTimeline.Stop();
    }
#endregion
}

/* geez
//      why am I being so smart using a far more complicated method while I can simply find opponent in an game object
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