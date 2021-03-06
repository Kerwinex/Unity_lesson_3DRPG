using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ker
{
    public class HurtSysWithUI : HurtSys
    {
        [Header("要更新的血條")]
        public Image imgHp;
        private float hpEffectOriginal;

        public override bool Hurt(float dmg)
        {
            hpEffectOriginal = hp;
            base.Hurt(dmg);
            StartCoroutine(HpBarEffect());
            return hp <= 0;
        }

        private IEnumerator HpBarEffect()
        {
            while (hpEffectOriginal != hp) {
                hpEffectOriginal--;
                imgHp.fillAmount = hpEffectOriginal / hpMax;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    
}


