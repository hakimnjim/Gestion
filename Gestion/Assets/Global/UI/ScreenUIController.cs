using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.ScreenUIControllers
{
    public class ScreenUIController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        public virtual void Init()
        {
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, UIConfig.FadeDuration).OnComplete(() =>
            {

            });
        }

        public virtual void Init(TypeStruct typeStruct)
        {
            Init();
        }

        public virtual void Init(WarningStruct warningStruct)
        {
            Init();
        }

        public virtual void DestroyController()
        {
            canvasGroup.DOFade(0, UIConfig.FadeDuration).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

        public virtual void Init(CheckBoxStruct checkBoxStruct)
        {
            Init();
        }

        public virtual void Init(CreateCollectionStruct createCollectionStruct)
        {
            Init();
        }

        public virtual void Init(HomeCollectionstruct homeCollectionstruct)
        {
            Init();
        }

        public virtual void Init(ActionButtonStruct actionButtonStruct)
        {
            Init();
        }

        public virtual void Init(InputFieldStruct inputFieldStruct)
        {
            Init();
        }

        public virtual void Init(CreateModuleStruct createModuleStruct)
        {
            Init();
        }

    }

    public static class UIConfig
    {
        public const float FadeDuration = 0.5f;
    }
}