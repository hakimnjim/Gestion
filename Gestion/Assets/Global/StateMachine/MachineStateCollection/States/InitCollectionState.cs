using UnityEngine;
using Global.ScreenUIControllers;
using Global.ScribtableObject;
using System.Collections.Generic;
using System;

namespace Global.StateMachine.States
{
    public class InitCollectionState : State
    {
        private Collections _collections;
        private HomeCollectionController _homeCollectionController;
        private CollectiionStateMachine _collectiionStateMachine;

        public override void Enter(GlobalStateMachine host)
        {
            base.Enter(host);
            _collectiionStateMachine = (CollectiionStateMachine)Host;
            _collections = GlobalEventManager.OnGetCollections();
            _homeCollectionController = (HomeCollectionController) GlobalEventManager.OnSpawnScreen(ScreenType.HomeCollectioncontroller, null);
            List<string> collectionNames = new List<string>();
            foreach (var item in _collections.allCollections)
            {
                collectionNames.Add(item.collectionName);
            }
            _homeCollectionController.Init(new HomeCollectionstruct
            {
                collectionNames = collectionNames,
                crerateNewCollection = CreateNewCollectionAction,
                createNewCollectionModule = CreateNewCollectionModule
            });

        }

        private void CreateNewCollectionModule(string moduleName)
        {
            CollectiionStateMachine collectiionStateMachine = (CollectiionStateMachine)Host;
            if (collectiionStateMachine != null)
            {
                collectiionStateMachine.SetCurrentModule(moduleName);
                collectiionStateMachine.ChangeState(collectiionStateMachine.createModuleState);
            }
        }

        private void CreateNewCollectionAction()
        {
            Host.ChangeState(_collectiionStateMachine.CreateCollectionState);
        }

        public override void Exit()
        {
            base.Exit();
        }

    }
}

