using UnityEngine;

namespace Ker
{
    public class ThirdPersonCamara : MonoBehaviour
    {
        #region ���
        [Header("�ؼЪ���")]
        public Transform target;
        [Header("�l�ܳt��"), Range(0, 100)]
        public float trackspeed = 1.5f;
        [Header("���k����t��"), Range(0, 100)]
        public float turnspeedH = 20f;
        [Header("�W�U����t��"), Range(0, 100)]
        public float turnspeedV = 20f;
        #endregion

        #region �ݩ�
        private float inputMouseX { get=>Input.GetAxis("Mouse X");}
        private float inputMouseY { get => Input.GetAxis("Mouse Y"); }
        #endregion

        #region �ƥ�
        private void Update()
        {
            TurnCam();
        }

        private void LateUpdate()
        {
            TrackTarget();
        }
        #endregion

        #region ��k
        private void TrackTarget()
        {
            Vector3 postarget = target.position;
            Vector3 poscam = transform.position;
            poscam = Vector3.Lerp(postarget, poscam, trackspeed * Time.deltaTime);
            transform.position = poscam;
        }

        private void TurnCam()
        {
            transform.Rotate(inputMouseY * turnspeedV * Time.deltaTime, inputMouseX * turnspeedH * Time.deltaTime, 0);
        }
        #endregion



    }
}

