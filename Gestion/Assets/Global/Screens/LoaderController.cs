using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Global.ScreenUIControllers
{
    public class LoaderController : ScreenUIController
    {
        [SerializeField] private Image filledImage;

        public float rotationDuration = 1f;

        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();
            RotateImage();
        }

        public override void DestroyController()
        {
            base.DestroyController();
        }

        private void RotateImage()
        {
            // Rotate the image continuously
            filledImage.transform
                .DORotate(new Vector3(0, 0, 360), rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear) // Keeps the rotation smooth
                .SetLoops(-1, LoopType.Restart); // Infinite loop
        }

        private void OnDisable()
        {
            filledImage.transform.DOKill();
        }
    }
}
