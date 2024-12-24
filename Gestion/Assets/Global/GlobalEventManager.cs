using Global.ScreenUIControllers;
using Global.StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global
{
    public class GlobalEventManager
    {
        public static Func<ScreenType, Transform, ScreenUIController> OnSpawnScreen;
        public static Action<ScreenType, Transform> OnDestroyScreenController;
    }
}
