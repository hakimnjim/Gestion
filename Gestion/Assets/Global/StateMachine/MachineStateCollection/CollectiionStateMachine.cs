using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.StateMachine
{
    using Global.StateMachine.States;

    public class CollectiionStateMachine : GlobalStateMachine
    {
        public State CreateCollectionState;
        public CreateModuleCollectionState createModuleState;

        public void SetCurrentModule(string moduleName)
        {
            createModuleState.moduleName = moduleName;
        }
    }
}

