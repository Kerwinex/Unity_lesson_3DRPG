using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ker.Dialogue
{
    [CreateAssetMenu(menuName = "Kerwin/對話資料", fileName = "NPC 對話資料")]
    public class Datadialogue : ScriptableObject
    {
        [Header("任務前對話內容"), TextArea(2, 7)]
        public string[] beforemission;
        [Header("任務中對話內容"), TextArea(2, 7)]
        public string[] missioning;
        [Header("任務完成後對話內容"), TextArea(2, 7)]
        public string[] aftermission;
        [Header("任務需求數量"), Range(0, 100)]
        public int countNeed;

        [Header("NPC任務狀態")]
        public StateNPCMission stateNPCmission = StateNPCMission.BeforeMission;
    }
}

