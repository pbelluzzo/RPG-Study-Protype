using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool triggered = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !triggered)
            {
                GetComponent<PlayableDirector>().Play();
                triggered = true;
            }
        }

    }

}