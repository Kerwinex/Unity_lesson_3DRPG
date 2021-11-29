using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ker
{
    public class AttackSys : MonoBehaviour
    {
        [Header("攻擊力"), Range(0, 500)]
        public float attack = 20;
        [Header("攻擊冷卻時間"), Range(0, 5)]
        public float timeAttack = 1.3f;
        [Header("延遲傳送傷害時間"), Range(0, 3)]
        public float damageDelay = 0.2f;
        [Header("攻擊區域尺寸及位移")]
        public Vector3 v3AttackOffset;
        public Vector3 v3AttackSize = Vector3.one;

        private Animator ani;
        private bool keyAttack { get => Input.GetKeyDown(KeyCode.Mouse0); }

        [Header("攻擊與走路動畫參數")]
        public string parameterAttack = "攻擊圖層觸發";
        public string parameterWalk = "走路開關";
        private bool isAttack;
        [Header("攻擊事件")]
        public UnityEvent onAttack;
        [Header("攻擊圖層遮色片")]
        public AvatarMask maskAttack;

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.8f, 0.2f, 0.7f, 0.3f);
            Gizmos.matrix = Matrix4x4.TRS(
                transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                transform.rotation, transform.localScale);
            Gizmos.DrawCube(Vector3.zero, v3AttackSize);
        }

        private void Awake()
        {
            ani = GetComponent<Animator>();
        }

        private void Update()
        {
            Attack();
        }

        private void Attack()
        {
            bool isWalk = ani.GetBool(parameterWalk);
            maskAttack.SetHumanoidBodyPartActive(AvatarMaskBodyPart.LeftLeg, !isWalk);
            maskAttack.SetHumanoidBodyPartActive(AvatarMaskBodyPart.RightLeg, !isWalk);
            maskAttack.SetHumanoidBodyPartActive(AvatarMaskBodyPart.LeftFootIK, !isWalk);
            maskAttack.SetHumanoidBodyPartActive(AvatarMaskBodyPart.RightFootIK, !isWalk);
            maskAttack.SetHumanoidBodyPartActive(AvatarMaskBodyPart.Root, !isWalk);
            if (keyAttack && !isAttack) {
                isAttack = true;
                ani.SetTrigger(parameterAttack);
                onAttack.Invoke();
                StartCoroutine(DelayHit());
            }
        }
        private IEnumerator DelayHit()
        {
            yield return new WaitForSeconds(damageDelay);
            Collider[] hits = Physics.OverlapBox(
                transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                v3AttackSize / 2, Quaternion.identity, 1 << 7 );
            if (hits.Length > 0) hits[0].GetComponent<HurtSys>().Hurt(attack);

            float waitToNextAttack = timeAttack - damageDelay;
            yield return new WaitForSeconds(waitToNextAttack);
            isAttack = false;
        }
    }
}

