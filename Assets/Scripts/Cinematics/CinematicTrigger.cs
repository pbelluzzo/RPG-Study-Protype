using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        [SerializeField] bool triggered = false;

        public object CaptureState()
        {
            return triggered;
        }

        public void RestoreState(object state)
        {
            triggered = (bool)state;
        }

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