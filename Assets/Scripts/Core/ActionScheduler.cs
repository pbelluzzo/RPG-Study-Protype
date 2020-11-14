using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            print("current action is " + currentAction);
            if (action == currentAction) return;    
            if (currentAction != null)
            {
                currentAction.Cancel();
                print("Starting " + action);
            }
            currentAction = action;
        }

    }

}