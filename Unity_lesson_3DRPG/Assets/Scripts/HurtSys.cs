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
        
        private Animator ani;

        protected float hpMax;

        private void Awake()
        {
            ani = GetComponent<Animator>();
            hpMax = hp;
        }

        public virtual bool Hurt(float dmg)
        {
            if (ani.GetBool(parameterDead)) return true;
            hp -= dmg;
            ani.SetTrigger(parameterHurt);
            onHurt.Invoke();
            if (hp <= 0) {
                Dead();
                return true;
            }
            else return false;            
        }

        public void Dead()
        {
            ani.SetBool(parameterDead, true);
            onDead.Invoke();
        }
    }
}

