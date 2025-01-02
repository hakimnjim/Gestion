using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.StateMachine
{
    public class StateMachineRunner : MonoBehaviour
    {
        [SerializeField] private GlobalStateMachine currentStateMachine;
        [SerializeField] private List<StateMachineType> stateMachineTypes;

        private void OnEnable()
        {
            Init();
        }

        public void Init()
        {
            if (currentStateMachine != null)
            {
                currentStateMachine.Initialise();
            }
        }

        public IEnumerator ChangeStateMachine(ScreenType screenType)
        {
            yield return new WaitForEndOfFrame();
            if (currentStateMachine != null)
            {
                currentStateMachine.CallChangeStateCoroutine(Time.deltaTime, currentStateMachine.IdleState);
                yield return new WaitForSeconds(0.5f);
            }

            int index = stateMachineTypes.FindIndex(x => x.screenType == screenType);
            if (index != -1)
            {
                GlobalStateMachine stateMachine = stateMachineTypes[index].stateMachine;
                stateMachine.Initialise();
            }
            else
            {
                Debug.LogError("No State Machine find with this type: " + screenType);
            }
        }

    }

    [System.Serializable]
    public struct StateMachineType
    {
        public ScreenType screenType;
        public GlobalStateMachine stateMachine;
    }
    public enum ScreenType { None, Login, Loader, Collection, Types, Warning, CollectionToggle, ActionButton, HomeCollectioncontroller, ModuleController, InputFieldController } 
}
