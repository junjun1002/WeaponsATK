using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junjun
{
    /// <summary>
    /// GameManager��Enemy�̃X�e�[�g���Ǘ�����ėp�I�ȃX�e�[�g�}�V��
    /// �W�F�l���b�N�^�iT�j�̕����ɃX�e�[�g�̃I�[�i�[���`����
    /// �p���̓X�e�[�g�̏�ԑ��Ōp������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IState<T> where T : MonoBehaviour
    {
        /// <summary>
        /// �X�e�[�g���؂�ւ��u�ԂɌĂ΂��֐�
        /// </summary>
        /// <param name="owner"></param>
        void OnExecute(T owner);
    }
}

