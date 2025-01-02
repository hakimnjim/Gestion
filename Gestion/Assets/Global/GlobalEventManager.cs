using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global
{
    using Global.ScribtableObject;
    using Global.ScreenUIControllers;
    using Global.StateMachine;
    public class GlobalEventManager
    {
        public static Func<ScreenType, Transform, ScreenUIController> OnSpawnScreen;
        public static Action<ScreenType, Transform> OnDestroyScreenController;
        public static Func<ResourceTypes> OnGetResourceTypes;
        public static Func<Collections> OnGetCollections;
        /// <summary>
        /// type, description, and return true if succ
        /// </summary>
        public static Func<string, string, bool> OnAddResourceTypes;
        public static Func<Collection, bool> OnAddNewCollection;
        internal static Action<string> OnSpawnWarningMessage;
    }
}
