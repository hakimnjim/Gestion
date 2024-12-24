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
            canvasGroup.DOFade(1, UIConfig.FadeDuration).OnComplete(() =>
            {

            });
        }

        public virtual void DestroyController()
        {
            canvasGroup.DOFade(0, UIConfig.FadeDuration).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

    }

    public static class UIConfig
    {
        public const float FadeDuration = 0.5f;
    }
}