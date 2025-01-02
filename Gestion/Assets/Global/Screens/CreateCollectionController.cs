using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Global.ScreenUIControllers
{
    public class CreateCollectionController : ScreenUIController
    {
        [SerializeField] private InputField collectionNameField;
        [SerializeField] private Button actionButton;
        [SerializeField] private Transform content;

        public override void Init(CreateCollectionStruct createCollectionStruct)
        {
            base.Init(createCollectionStruct);
            collectionNameField.onValueChanged.AddListener(Value => createCollectionStruct.onCollectionNameChanged(Value));
            actionButton.onClick.AddListener(() => createCollectionStruct.addCollectionAction());
            foreach (var item in createCollectionStruct.collectionNames)
            {
                CheckBoxController checkBoxController = (CheckBoxController)GlobalEventManager.OnSpawnScreen(StateMachine.ScreenType.CollectionToggle, content);
                checkBoxController.Init(new CheckBoxStruct
                {
                    labelName = item,
                    onValueChange = createCollectionStruct.onValueChanged
                });
            }
        }
    }

    public struct CreateCollectionStruct
    {
        public Action<string> onCollectionNameChanged;
        public List<string> collectionNames;
        public Action<bool ,string> onValueChanged;
        public Action addCollectionAction;
    }

}

