using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ker.Dialogue
{
    [CreateAssetMenu(menuName = "Kerwin/��ܸ��", fileName = "NPC ��ܸ��")]
    public class Datadialogue : ScriptableObject
    {
        [Header("���ȫe��ܤ��e"), TextArea(2, 7)]
        public string[] beforemission;
        [Header("���Ȥ���ܤ��e"), TextArea(2, 7)]
        public string[] missioning;
        [Header("���ȧ������ܤ��e"), TextArea(2, 7)]
        public string[] aftermission;
        [Header("���ȻݨD�ƶq"), Range(0, 100)]
        public int countNeed;

        [Header("NPC���Ȫ��A")]
        public StateNPCMission stateNPCmission = StateNPCMission.BeforeMission;
    }
}

