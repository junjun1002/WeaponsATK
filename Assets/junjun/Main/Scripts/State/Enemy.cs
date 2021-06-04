using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using UniRx;

namespace Junjun
{
    public class Enemy : MonoBehaviour
    {
       public  StateMachine<Enemy> stateMachine;

        public IState<Enemy> IdleState { get; set; } = new Idle();

        public IState<Enemy> AttackState { get; set; } = new Attack();

        private async void Start()
        {
            stateMachine = new StateMachine<Enemy>(this, IdleState);

            Debug.Log(stateMachine.currentState);
            //Debug.Log(stateMachine.currentState);
            //stateMachine.currentState.OnExcute();

            //stateMachine.ChageMachine(AttackState);
            //Debug.Log(stateMachine.currentState);
            //stateMachine.currentState.OnExcute();


        }

        private void Update()
        {
            stateMachine.currentState.OnExcute(this);
        }
    }

    public class Idle : IState<Enemy>
    {
        float timer = 0;
        public void OnExcute(Enemy owner)
        {
            timer += Time.deltaTime;

            if (timer > 5f)
            {
                owner.stateMachine.ChageMachine(owner.AttackState);
                Debug.Log(owner.stateMachine.currentState);
                timer = 0;
            }
        }
    }

    public class Attack : IState<Enemy>
    {
        float timer = 0;
        public void OnExcute(Enemy owner)
        {
            timer += Time.deltaTime;

            if (timer > 5f)
            {
                owner.stateMachine.ChageMachine(owner.IdleState);
                Debug.Log(owner.stateMachine.currentState);
                timer = 0;
            }
        }
    }
}
