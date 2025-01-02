using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace Global.ScreenUIControllers
{
    public class InputFieldController : ScreenUIController
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Text labelName;

        public override void Init(InputFieldStruct inputFieldStruct)
        {
            base.Init(inputFieldStruct);
            inputField.onValueChanged.AddListener(value => inputFieldStruct.onValueChnaged(inputFieldStruct.labelName, value));
            labelName.text = "Module " +  inputFieldStruct.labelName;
        }

    }

    public struct InputFieldStruct
    {
        public Action<string, string> onValueChnaged;
        public string labelName;
    }
}
