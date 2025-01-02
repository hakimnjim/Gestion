using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.AppManager
{
    using Global.ScreenUIControllers;
    using Global.ScribtableObject;
    using System;

    public class AppManager : MonoBehaviour
    {
        public  ResourceTypes resourceTypes;
        [SerializeField] private Collections collections;

        private void OnEnable()
        {
            GlobalEventManager.OnGetResourceTypes += OnGetResourceTypes;
            GlobalEventManager.OnGetCollections += OnGetCollections;
            GlobalEventManager.OnAddNewCollection += OnAddNewCollection;
            GlobalEventManager.OnAddResourceTypes += OnAddResourceTypes;
            GlobalEventManager.OnSpawnWarningMessage += OnSpawnWarningMessage;
        }

        private bool OnAddNewCollection(Collection collection)
        {
            int index = collections.allCollections.FindIndex(x => x.collectionName == collection.collectionName);
            if (index != -1)
            {
                return false;
            }
            collections.allCollections.Add(collection);
            return true;
        }

        private Collections OnGetCollections()
        {
            return collections;
        }

        private bool OnAddResourceTypes(string type, string desc)
        {
            int index = resourceTypes.stringTypes.FindIndex(x => x.typeName == type);
            if (index != -1)
            {
                return false;
            }
            StringTypeSO stringTypeSO = new StringTypeSO
            {
                typeName = type,
                description = desc
            };
            resourceTypes.stringTypes.Add(stringTypeSO);
            return true;
        }

        private ResourceTypes OnGetResourceTypes()
        {
            return resourceTypes;
        }

        private void OnSpawnWarningMessage(string msg)
        {
            WarningController warning = (WarningController)GlobalEventManager.OnSpawnScreen(StateMachine.ScreenType.Warning, null);
            warning.Init(new WarningStruct
            {
                msg = msg,
                onCloseButton = OnCloseWarningAction
            });

        }

        private void OnCloseWarningAction()
        {
            GlobalEventManager.OnDestroyScreenController(StateMachine.ScreenType.Warning, null);
        }

        private void OnDisable()
        {
            GlobalEventManager.OnGetResourceTypes -= OnGetResourceTypes;
            GlobalEventManager.OnGetCollections -= OnGetCollections;
            GlobalEventManager.OnAddResourceTypes -= OnAddResourceTypes;
            GlobalEventManager.OnSpawnWarningMessage -= OnSpawnWarningMessage;
            GlobalEventManager.OnAddNewCollection -= OnAddNewCollection;
        }
    }

}
