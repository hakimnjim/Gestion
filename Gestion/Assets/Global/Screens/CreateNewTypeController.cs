using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Global.ScreenUIControllers
{
    public class CreateNewTypeController : ScreenUIController
    {
        [SerializeField] private InputField typeText;
        [SerializeField] private InputField typeDescription;
        [SerializeField] private Button addButton;

        public override void Init(TypeStruct typeStruct)
        {
            base.Init();
            typeText.onValueChanged.AddListener(value => typeStruct.onTypeTextChanged(value));
            typeDescription.onValueChanged.AddListener(value => typeStruct.onTypeDescChanged(value));
            addButton.onClick.AddListener(() => typeStruct.addButtonAction());
        }
    }

    public struct TypeStruct
    {
        public Action<string> onTypeTextChanged;
        public Action<string> onTypeDescChanged;
        public Action addButtonAction;

    }
}

