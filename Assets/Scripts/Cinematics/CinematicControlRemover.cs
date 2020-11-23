using RPG.Controller;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicControlRemover : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayableDirector director = GetComponent<PlayableDirector>();
        director.played += DisableControl;
        director.stopped += EnableControl;
        
    }
    void DisableControl(PlayableDirector director)
    {
        player.GetComponent<ActionScheduler>().CancelCurrentAction();
        player.GetComponent<PlayerController>().enabled = false;
    }

    void EnableControl(PlayableDirector director)
    {
        player.GetComponent<PlayerController>().enabled = true;
    }
}
