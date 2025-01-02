using UnityEngine;
using System;
using UnityEngine.UI;

namespace Global.ScreenUIControllers
{
    public class WarningController : ScreenUIController
    {
        [SerializeField] private Text msgText;
        [SerializeField] private Button actionButton;

        public override void Init(WarningStruct warningStruct)
        {
            base.Init(warningStruct);
            msgText.text = warningStruct.msg;
            actionButton.onClick.AddListener(() => warningStruct.onCloseButton());
        }
    }

    public struct WarningStruct
    {
        public string msg;
        public Action onCloseButton;
    }
}

