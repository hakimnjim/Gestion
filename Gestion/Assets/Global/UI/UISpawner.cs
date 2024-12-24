using Global.ScreenUIControllers;
using Global.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
namespace Global.UI
{
    public class UISpawner : MonoBehaviour
    {
        public List<ScreenView> screens;
        [SerializeField] private List<ScreenView> _currentScreentControllers = new List<ScreenView>();
        [SerializeField] private Transform contentParent;

        private void OnEnable()
        {
            GlobalEventManager.OnSpawnScreen += OnSpawnScreen;
            GlobalEventManager.OnDestroyScreenController += OnDestroyScreenController;
        }

        private ScreenUIController OnSpawnScreen(ScreenType screenType, Transform parent)
        {
            ScreenView screenView = screens.Find(x => x.screenType == screenType);
            int index = _currentScreentControllers.FindIndex(x => x.screenType == screenType);
            ScreenUIController controller = null;

            // if already have this controller
            if (index != -1)
            {
                controller = _currentScreentControllers[index].screenController;
                return controller;
            }

            var content = parent ? parent : contentParent;

            if (screenView.screenController)
            {
                controller = Instantiate(screenView.screenController, content);
            }
            _currentScreentControllers.Add(new ScreenView { screenType = screenType, screenController = controller });
            return controller;
        }


        private void OnDestroyScreenController(ScreenType screenType, Transform parent)
        {
            int index = _currentScreentControllers.FindIndex(x => x.screenType == screenType);
            if (index != -1)
            {
                ScreenUIController controller = _currentScreentControllers[index].screenController;
                _currentScreentControllers.RemoveAt(index);
                controller.DestroyController();
            }
        }

        private void OnDestroyAllUI()
        {
            for (int i = 0; i < _currentScreentControllers.Count; i++)
            {
                ScreenUIController controller = _currentScreentControllers[i].screenController;
                _currentScreentControllers.RemoveAt(i);
                Destroy(controller.gameObject);
            }
        }

        private void OnDisable()
        {
            GlobalEventManager.OnSpawnScreen -= OnSpawnScreen;
            GlobalEventManager.OnDestroyScreenController -= OnDestroyScreenController;
        }
    }

    [System.Serializable]
    public struct ScreenView
    {
        public ScreenType screenType;
        public ScreenUIController screenController;
    }
}