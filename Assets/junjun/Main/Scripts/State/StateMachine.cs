using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junjun
{
    public class StateMachine<T> where T : MonoBehaviour
    {
        /// <summary>�X�e�[�g�̎�ނɂ���ăI�[�i�[��ݒ肷��</summary>
        T owner;
        /// <summary>���݂̃X�e�[�g</summary>
        public IState<T> currentState;

        /// <summary>
        /// �I�[�i�[�Ə����X�e�[�g��ݒ肷��R���X�g���N�^�[
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="initState"></param>
        public StateMachine(T owner, IState<T> initState)
        {
            this.owner = owner;
            currentState = initState;
        }

        /// <summary>
        /// ���̃X�e�[�g�Ɉڍs������֐�
        /// </summary>
        /// <param name="nextState"></param>
        public void ChageMachine(IState<T> nextState)
        {
            currentState = nextState;
        }
    }
}
