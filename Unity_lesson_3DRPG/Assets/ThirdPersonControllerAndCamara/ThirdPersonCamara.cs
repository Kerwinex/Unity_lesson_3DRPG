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
        [Header("X軸上下旋轉限制，最小值與最大值")]
        public Vector2 limanglex = new Vector2(-0.2f, 0.2f);
        [Header("Z軸上下旋轉限制，最小值與最大值")]
        public Vector2 limanglez = new Vector2(-0.2f, 0f);
        private Vector3 _posForward;
        private float lenghtForward = 3f;
        #endregion

        #region 屬性
        private float inputMouseX { get => Input.GetAxis("Mouse X"); }
        private float inputMouseY { get => Input.GetAxis("Mouse Y"); }
        public Vector3 posForward
        {
            get
            {
                _posForward = transform.position + transform.forward * lenghtForward;
                _posForward.y = target.position.y;
                return _posForward;
            } 
        }
        #endregion

        #region 事件
        private void Update()
        {
            TurnCam();
            LimAngleXAndLimAngleZ();
            FreezeAngleZ();
        }

        private void LateUpdate()
        {
            TrackTarget();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.2f, 0,1, 0.3f);
            _posForward = transform.position + transform.forward * lenghtForward;
            _posForward.y = target.position.y;            
            Gizmos.DrawSphere(_posForward,0.15f);
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

        private void LimAngleXAndLimAngleZ()
        {
            print(transform.rotation);
            Quaternion angle = transform.rotation;
            angle.x = Mathf.Clamp(angle.x, limanglex.x, limanglex.y);
            angle.z = Mathf.Clamp(angle.z, limanglez.x, limanglez.y);
            transform.rotation = angle;
        }

        private void FreezeAngleZ()
        {
            Vector3 angle = transform.eulerAngles;
            angle.z = 0;
            transform.eulerAngles = angle;
        }
        #endregion
    }
}

