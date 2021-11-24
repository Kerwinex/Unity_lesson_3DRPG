using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ker
{
    public class HurtSys : MonoBehaviour
    {
        [Header("��q"), Range(0, 5000)]
        public float hp = 100;
        [Header("���˨ƥ�")]
        public UnityEvent onHurt;
        [Header("���`�ƥ�")]
        public UnityEvent onDead;
        [Header("�ʵe�ѼơG���˻P���`")]
        public string parameterHurt = "����Ĳ�o";
        public string parameterDead = "���`�}��";

        protected float hpMax;
        private Animator ani;        

        private void Awake()
        {
            ani = GetComponent<Animator>();
            hpMax = hp;
        }

        public virtual void Hurt(float dmg)
        {
            if (ani.GetBool(parameterDead)) return;
            hp -= dmg;
            ani.SetTrigger(parameterHurt);
            onHurt.Invoke();
            if (hp <= 0) Dead();
        }

        public void Dead()
        {
            ani.SetBool(parameterDead, true);
            onDead.Invoke();
        }
    }
}

