using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Global.ScreenUIControllers
{
    public class HomeCollectionController : ScreenUIController
    {
        [SerializeField] private Transform content;
        [SerializeField] private Button createCollectionButton;

        public override void Init(HomeCollectionstruct homeCollectionstruct)
        {
            base.Init(homeCollectionstruct);
            createCollectionButton.onClick.AddListener(() => homeCollectionstruct.crerateNewCollection());
            for (int i = 0; i < homeCollectionstruct.collectionNames.Count; i++)
            {
                ActionButtonController actionButtonController = (ActionButtonController)GlobalEventManager.OnSpawnScreen(StateMachine.ScreenType.ActionButton, content);
                actionButtonController.Init(new ActionButtonStruct
                {
                    module = homeCollectionstruct.collectionNames[i],
                    onclickActionBuuton = homeCollectionstruct.createNewCollectionModule
                });
            }
        }
    }

    public struct HomeCollectionstruct
    {
        public List<string> collectionNames;
        public Action crerateNewCollection;
        public Action<string> createNewCollectionModule;
    }
}

