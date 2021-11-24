using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ker
{
    public class HurtSys : MonoBehaviour
    {
        [Header("血量"), Range(0, 5000)]
        public float hp = 100;
        [Header("受傷事件")]
        public UnityEvent onHurt;
        [Header("死亡事件")]
        public UnityEvent onDead;
        [Header("動畫參數：受傷與死亡")]
        public string parameterHurt = "受傷觸發";
        public string parameterDead = "死亡開關";

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

