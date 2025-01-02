using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace Global.ScreenUIControllers
{
    public class ActionButtonController : ScreenUIController
    {
        [SerializeField] private Button actionButton;
        [SerializeField] private TextMeshProUGUI buttonText;

        public override void Init(ActionButtonStruct actionButtonStruct)
        {
            base.Init(actionButtonStruct);
            buttonText.text = "Create " + actionButtonStruct.module;
            actionButton.onClick.AddListener(() => actionButtonStruct.onclickActionBuuton(actionButtonStruct.module));
        }
    }

    public struct ActionButtonStruct
    {
        public string module;
        public Action<string> onclickActionBuuton;
    }
}

