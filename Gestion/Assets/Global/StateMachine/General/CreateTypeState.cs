using Global.ScreenUIControllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.StateMachine.States
{
    public class CreateTypeState : State
    {
        private string type;
        private string desc;
        private CreateNewTypeController _createNewTypeController;

        public override void Enter(GlobalStateMachine host)
        {
            base.Enter(host);
            _createNewTypeController = (CreateNewTypeController)GlobalEventManager.OnSpawnScreen(ScreenType.Types, null);
            TypeStruct typeStruct = new TypeStruct
            {
                onTypeTextChanged = OnTypeTextChanged,
                onTypeDescChanged = OnTypeDescChanged,
                addButtonAction = AddButtonAction
            };
            _createNewTypeController.Init(typeStruct);
        }

        private void AddButtonAction()
        {
            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(desc))
            {
                GlobalEventManager.OnSpawnWarningMessage?.Invoke("type text and description are required");
                return;
            }

            if (GlobalEventManager.OnAddResourceTypes != null)
            {
                bool isSucc = GlobalEventManager.OnAddResourceTypes(type, desc);
                if (isSucc)
                {
                    if (NextState != null)
                    {
                        Host.ChangeState(NextState);
                    }
                }
                else
                {
                    GlobalEventManager.OnSpawnWarningMessage?.Invoke("this type already added");
                }
            }
        }

        private void OnTypeDescChanged(string value)
        {
            desc = value;
        }

        private void OnTypeTextChanged(string value)
        {
            type = value;
        }

        public override void Exit()
        {
            base.Exit();
            GlobalEventManager.OnDestroyScreenController(ScreenType.Types, null);
        }
    }
}

