using UnityEngine;

namespace Ker
{
    public class ThirdPersonCamara : MonoBehaviour
    {
        #region 欄位
        [Header("目標物件")]
        public Transform target;
        [Header("追蹤速度"), Range(0, 100)]
        public float trackspeed = 1.5f;
        [Header("左右旋轉速度"), Range(0, 100)]
        public float turnspeedH = 20f;
        [Header("上下旋轉速度"), Range(0, 100)]
        public float turnspeedV = 20f;
        #endregion

        #region 屬性
        private float inputMouseX { get=>Input.GetAxis("Mouse X");}
        private float inputMouseY { get => Input.GetAxis("Mouse Y"); }
        #endregion

        #region 事件
        private void Update()
        {
            TurnCam();
        }

        private void LateUpdate()
        {
            TrackTarget();
        }
        #endregion

        #region 方法
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

