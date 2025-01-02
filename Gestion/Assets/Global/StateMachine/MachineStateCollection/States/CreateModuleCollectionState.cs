using Global.ScreenUIControllers;
using Global.ScribtableObject;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.StateMachine.States
{
    public class CreateModuleCollectionState : State
    { 
        public string moduleName { get; set; }
        private Collections _collections;
        public Collection currentModule;
        private Collection currentCollection;

        public override void Enter(GlobalStateMachine host)
        {
            base.Enter(host);
            _collections = GlobalEventManager.OnGetCollections();
            if (moduleName != "")
            {
                int index = _collections.allCollections.FindIndex(x => x.collectionName == moduleName);
                if (index == -1)
                {
                    Debug.LogError("Hey what happend here what are doing kiki");
                    return;
                }
                currentCollection = _collections.allCollections[index];
                List<string> collectionNames = new List<string>();
                for (int i = 0; i < currentCollection.collectionEntities.Count; i++)
                {
                    collectionNames.Add(currentCollection.collectionEntities[i].nameOfEntity);
                }

                CreateModuleController moduleController = (CreateModuleController)GlobalEventManager.OnSpawnScreen(ScreenType.ModuleController, null);
                moduleController.Init(new CreateModuleStruct
                {
                    onValueChnaged = OnInputFieldValueChanged,
                    createButtonAction = OnCreateNewModule,
                    labelNames = collectionNames,
                    moduleName = moduleName
                });

                currentModule = currentCollection.DeepCopy();
            }
        }

        private void OnCreateNewModule(string s)
        {
           
        }

        private void OnInputFieldValueChanged(string labelName, string value)
        {
            foreach (var item in currentModule.collectionEntities)
            {
                if (item.nameOfEntity == labelName)
                {
                    item.input = value;
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            _collections = new Collections();
            moduleName = "";
            currentCollection = new Collection();
        }
    }
}


