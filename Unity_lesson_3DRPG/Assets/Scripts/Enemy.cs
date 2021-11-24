using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Ker.Dialogue;

namespace Ker.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [Header("移動速度"), Range(0, 20)]
        public float speed = 2.5f;
        [Header("攻擊力"), Range(0, 200)]
        public float attack = 35;
        [Header("範圍:追蹤與攻擊")]
        [Range(0,7)]
        public float rangeAttack = 5;
        [Range(7, 20)]
        public float rangeTrack = 15;
        [Header("等待隨機秒數")]
        public Vector2 v2RandomWait = new Vector2(1, 5);
        [Header("走路隨機秒數")]
        public Vector2 v2RandomWalk = new Vector2(3, 7);  
        [Header("NPC 名稱")]
        public string nameNPC = "NPC 1號";

        private NPC npc;
        private HurtSys hurtSys;

        [SerializeField]
        private StateEnemy state;       
        private Animator ani;
        private NavMeshAgent nma;
        private bool isIdle;
        private bool isWalk;
        private string parameterIdleWalk = "走路開關";
        private Vector3 v3RandomWalk { get => Random.insideUnitSphere * rangeTrack + transform.position; }
        private Vector3 v3RandomWalkFinal;
        private bool playerInTrackRange { get => Physics.OverlapSphere(transform.position, rangeTrack, 1 << 6).Length > 0; }
        private Transform traPlayer;
        private string namePlayer = "Female05";
        private bool isTrack;

        [Header("攻擊區域位移與尺寸")]
        public Vector3 v3AttackOffset;
        public Vector3 v3AttackSize = Vector3.one;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeAttack);

            Gizmos.color = new Color(0.2f, 1, 0, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeTrack);

            if (state == StateEnemy.Walk) {
                Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
                Gizmos.DrawSphere(v3RandomWalkFinal, 0.3f);
            }

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
            nma = GetComponent<NavMeshAgent>();
            nma.speed = speed;
            traPlayer = GameObject.Find(namePlayer).transform;
            
            hurtSys = GetComponent<HurtSys>();
            npc = GameObject.Find(nameNPC).GetComponent<NPC>();
            hurtSys.onDead.AddListener(npc.UpdateMissionCount);
            nma.SetDestination(transform.position);
        }
        private void Update()
        {
            StateManager();
        }

        private void StateManager()
        {
            switch (state) {
                case StateEnemy.Idle:
                    Idle();
                    break;
                case StateEnemy.Walk:
                    Walk();
                    break;
                case StateEnemy.Track:
                    Track();
                    break;
                case StateEnemy.Attack:
                    Attack();
                    break;
                case StateEnemy.Hurt:
                    break;
                case StateEnemy.Dead:
                    break;
                default:
                    break;
            }
        }
        private void Idle()
        {
            if (!targetIsDead && playerInTrackRange) { state = StateEnemy.Track; }
            if (isIdle) return;
            isIdle = true;
            print("等待");
            ani.SetBool(parameterIdleWalk, false);
            StartCoroutine(IdleEffect());
        }
        private IEnumerator IdleEffect()
        {
            float randomwait = Random.Range(v2RandomWait.x, v2RandomWait.y);
            yield return new WaitForSeconds(randomwait);
            state = StateEnemy.Walk;
            isIdle = false;
        }

        private void Walk()
        {
            if (!targetIsDead && playerInTrackRange) { state = StateEnemy.Track; }
            nma.SetDestination(v3RandomWalkFinal);     
            ani.SetBool(parameterIdleWalk, nma.remainingDistance > 0.1f);

            if (isWalk) return;
            isWalk = true;            
            NavMeshHit hit;
            NavMesh.SamplePosition(v3RandomWalk, out hit, rangeTrack, NavMesh.AllAreas);
            v3RandomWalkFinal = hit.position;            
            StartCoroutine(WalkEffect());
        }        
        private IEnumerator WalkEffect()
        {
            float randomwalk = Random.Range(v2RandomWalk.x, v2RandomWalk.y);
            yield return new WaitForSeconds(randomwalk);
            state = StateEnemy.Idle;
            isWalk = false;
        }

        private void Track()
        {
            if (!playerInTrackRange) { state = StateEnemy.Walk; }
            if (!isTrack) {
                StopAllCoroutines();
            }
            isTrack = true;            
            nma.isStopped = false;
            nma.SetDestination(traPlayer.position);
            ani.SetBool(parameterIdleWalk, true);
            if (nma.remainingDistance <= rangeAttack) { state = StateEnemy.Attack; }
        }
        
        [Header("攻擊時間"), Range(0, 5)]
        public float timeAttack=2.5f;
        [Header("攻擊傷害延遲時間"), Range(0, 5)]
        public float damageDelay = 0.5f;
        private string parameterAttack = "攻擊觸發";
        private bool isAttack;
        private bool targetIsDead;
        
        private void Attack()
        {
            nma.isStopped = true;
            ani.SetBool(parameterIdleWalk, false);
            nma.SetDestination(traPlayer.position);
            LookAtPlayer();
            if (nma.remainingDistance > rangeAttack) state = StateEnemy.Track;

            if (isAttack) return;
            isAttack = true;
            ani.SetTrigger(parameterAttack);
            StartCoroutine(DelatSendDamgeToTarget());            
        }
        private IEnumerator DelatSendDamgeToTarget()
        {
            yield return new WaitForSeconds(damageDelay);
            Collider[] hits = Physics.OverlapBox(
                transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                v3AttackSize / 2, Quaternion.identity, 1 << 6);

            if (hits.Length > 0) {
                print("攻擊到的物件：" + hits[0].name);
                targetIsDead = hits[0].GetComponent<HurtSys>().Hurt(attack);
            }
            if (targetIsDead) TargetDead();

            float wiatToNextAttack = timeAttack - damageDelay;
            yield return new WaitForSeconds(wiatToNextAttack);
            isAttack = false;
        }

        private void TargetDead()
        {
            state = StateEnemy.Walk;
            isIdle = false;
            isWalk = false;
            nma.isStopped = false;
        }

        [Header("面向玩家速度")]
        public float speedLookAt = 10;

        private void LookAtPlayer()
        {
            Quaternion angle = Quaternion.LookRotation(traPlayer.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookAt);
            ani.SetBool(parameterIdleWalk, transform.rotation != angle);            
        }


    }
}
