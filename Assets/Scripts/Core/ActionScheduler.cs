using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;
        public void StartAction(IAction action)
        {
            if (action == currentAction) return;
            if (currentAction != null)
            {
                print($"Cancelling {currentAction}");
                currentAction.Cancel();
            }
            currentAction = action;
        }
    }
}
