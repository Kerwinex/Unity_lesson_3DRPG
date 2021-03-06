using UnityEngine;
using UnityEngine.Events;

namespace Ker.Dialogue
{
    public class NPC : MonoBehaviour
    {
        [Header("對話資料")]
        public Datadialogue datadialogue;
        [Header("相關資訊")]
        [Range(0, 10)]
        public float checkPlayerRadius = 3f;
        public GameObject goTip;
        [Range(0, 10)]
        public float speedLookAt = 3;
        [Header("對話系統")]
        public DialogueSystem dialogueSystem;
        [Header("完成任務事件")]
        public UnityEvent onFinish;

        private int countCurrent;
        private Transform target;
        public bool startDialogueKey { get => Input.GetKeyDown(KeyCode.E); }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, checkPlayerRadius);
        }

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            goTip.SetActive(CheckPlayer());
            LookAtPlayer();
            StartDialogue();
        }

        private void Initialize()
        {
            datadialogue.stateNPCmission = StateNPCMission.BeforeMission;
        }

        private bool CheckPlayer()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, checkPlayerRadius, 1 << 6);
            if (hits.Length > 0) target = hits[0].transform;
            return hits.Length > 0;
        }

        public void LookAtPlayer()
        {
            if (CheckPlayer()) {
                Quaternion angle = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookAt);
            }
        }

        private void StartDialogue()
        {
            if(CheckPlayer() && startDialogueKey) {
                dialogueSystem.Dialogue(datadialogue);

                if (datadialogue.stateNPCmission == StateNPCMission.BeforeMission) datadialogue.stateNPCmission = StateNPCMission.Missioning;
            }
            else if(!CheckPlayer()) {
                dialogueSystem.StopDialogue();
            }
        }

        public void UpdateMissionCount()
        {
            countCurrent++;
            if (countCurrent == datadialogue.countNeed) {
                datadialogue.stateNPCmission = StateNPCMission.AfterMission;
                onFinish.Invoke();
            }

        }
    }
}
