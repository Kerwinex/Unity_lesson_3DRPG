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

