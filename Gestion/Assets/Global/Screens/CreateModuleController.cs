using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Global.ScreenUIControllers
{
    public class CreateModuleController : ScreenUIController
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private ActionButtonController createButton;
        [SerializeField] private Transform content;

        public override void Init(CreateModuleStruct createModuleStruct)
        {
            base.Init(createModuleStruct);
            title.text = "Create New " + createModuleStruct.moduleName;
            createButton.Init(new ActionButtonStruct
            {
                module = createModuleStruct.moduleName,
                onclickActionBuuton = createModuleStruct.createButtonAction
            });
            for (int i = 0; i < createModuleStruct.labelNames.Count; i++)
            {
                InputFieldController inputFieldController = (InputFieldController) GlobalEventManager.OnSpawnScreen(StateMachine.ScreenType.InputFieldController, content);
                inputFieldController.Init(new InputFieldStruct
                {
                    onValueChnaged = createModuleStruct.onValueChnaged,
                    labelName = createModuleStruct.labelNames[i]
                });
            }
        }

    }

    public struct CreateModuleStruct
    {
        public Action<string, string> onValueChnaged;
        public Action<string> createButtonAction;
        public List<string> labelNames;
        public string moduleName;
    }
}

