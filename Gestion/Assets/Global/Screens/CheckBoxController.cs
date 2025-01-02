using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Global.ScreenUIControllers
{
    public class CheckBoxController : ScreenUIController
    {
        [SerializeField] private Text label;
        [SerializeField] private Toggle toggle;

        public override void Init(CheckBoxStruct checkBoxStruct)
        {
            base.Init(checkBoxStruct);
            label.text = "Has " + checkBoxStruct.labelName;
            toggle.onValueChanged.AddListener(value => checkBoxStruct.onValueChange(value, checkBoxStruct.labelName));
        }
    }

    public struct CheckBoxStruct
    {
        public string labelName;
        public Action<bool ,string> onValueChange;
    }
}

