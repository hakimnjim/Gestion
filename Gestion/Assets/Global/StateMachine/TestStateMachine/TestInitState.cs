using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.StateMachine.States
{
    public class TestInitState : State
    {
        public override void Enter(GlobalStateMachine host)
        {
            base.Enter(host);
            GlobalEventManager.OnSpawnScreen(ScreenType.Loader, null);
            StartCoroutine(TestChangeState());
        }

        IEnumerator TestChangeState()
        {
            yield return new WaitForSeconds(4);
            Host.ChangeState(Host.IdleState);
        }

        public override void Exit()
        {
            base.Exit();
            GlobalEventManager.OnDestroyScreenController(ScreenType.Loader, null);
        }
    }
}

