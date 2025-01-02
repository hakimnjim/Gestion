using Global.ScribtableObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global.ScreenUIControllers;
using System;

namespace Global.StateMachine.States
{
    public class CreateCollectionState : State
    {
        private Collections _collections;
        private CreateCollectionController _createCollection;
        private Collection collection = new Collection();
        private HashSet<string> collectionNamesOn = new HashSet<string>();
        private string collectionName;

        public override void Enter(GlobalStateMachine host)
        {
            base.Enter(host);
            _collections = GlobalEventManager.OnGetCollections();
            _createCollection = (CreateCollectionController)GlobalEventManager.OnSpawnScreen(ScreenType.Collection, null);
            List<string> collectionNames = new List<string>();

            for (int i = 0; i < _collections.defualtEntitys.Count; i++)
            {
                var item = _collections.defualtEntitys[i];
                collectionNames.Add(item.nameOfEntity);
            }
            foreach (var item in _collections.allCollections)
            {
                collectionNames.Add(item.collectionName);
            }

            _createCollection.Init(new CreateCollectionStruct
            {
                onCollectionNameChanged = OnCollectionNameChanged,
                collectionNames = collectionNames,
                onValueChanged = OnValueChanged,
                addCollectionAction = AddCollectionAction
            });
        }

        private void AddCollectionAction()
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                GlobalEventManager.OnSpawnWarningMessage("Collection Name is required please assign one");
                return;
            }

            collection.collectionName = collectionName;
            foreach (var item in collectionNamesOn)
            {
                Debug.Log(item);
                int indexDefault = _collections.defualtEntitys.FindIndex(x => x.nameOfEntity == item);
                if (indexDefault != -1)
                {
                    collection.collectionEntities.Add(_collections.defualtEntitys[indexDefault]);
                }
                else
                {
                    int indexCollection = _collections.allCollections.FindIndex(x => x.collectionName == item);
                    if (indexCollection != -1)
                    {
                        collection.collections.Add(_collections.allCollections[indexCollection]);
                    }
                    else if(item == "Type")
                    {
                        collection.hasStringTypeSO = true;
                    }
                    else
                    {
                        GlobalEventManager.OnSpawnWarningMessage("Something wrong in integration");
                        return;
                    }
                }
            }
            if (!GlobalEventManager.OnAddNewCollection.Invoke(collection))
            {
                GlobalEventManager.OnSpawnWarningMessage("Please change the collection name because we already have one");
            }
            else
            {
                Host.ChangeState(NextState);
            }
        }

        private void OnValueChanged(bool isOn, string name)
        {
            if (isOn)
            {
                // Adds name to the HashSet if it's not already present
                collectionNamesOn.Add(name);
            }
            else
            {
                // Removes the name from the HashSet if it exists
                collectionNamesOn.Remove(name);
            }
        }

        private void OnCollectionNameChanged(string value)
        {
            collectionName = value;
        }

        public override void Exit()
        {
            base.Exit();
            _collections = new Collections();
            _createCollection = null;
            collection = new Collection();
            collectionNamesOn = new HashSet<string>();
            collectionName = "";
            GlobalEventManager.OnDestroyScreenController(ScreenType.Collection, null);

        }
    }
}


