using UnityEngine;
using UnityEngine.Video;

//修飾詞 類別 類別名稱:繼承類別
//MonoBehaviour unity基底類別，要掛在物件上議定要繼承
//繼承後會享有該類別的成員
//常用成員：欄位 Field、屬性 Property(變數)、方法 Method、事件 Event
//在類別及成員上方增加三條斜線可以增加摘要
/// <summary>
/// 這裡是摘要
/// 吳紹雍 20210906
/// 第三人稱控制器
/// 移動、跳躍
/// </summary>

namespace Ker.pratice
{
    public class ThirdPersonControll : MonoBehaviour
    {
        #region 欄位 Field
        //變數值會以unity內的屬性面板為主，腳本中會指定為預設值，於unity中使用reset便會改為腳本中的值
        //欄位屬性 Attribute：輔助欄位資料
        //欄位屬性語法[屬性名稱(屬性值)]
        //Header 標題
        //Tooltip 提示：滑鼠停留在欄位名稱會顯示彈出視窗
        //Range 範圍：可使用在數值資料上
        [Header("移動速度"), Tooltip("用來調整角色移動速度"), Range(1, 500)]
        public float speed = 10.5f;
        [Header("跳躍高度"), Tooltip("用來調整角色跳躍高度"), Range(0, 1000)]
        public int height = 200;

        [Header("檢查地面資料"), Tooltip("偵測角色是否在地板上")]
        public bool ground_chk = false;
        public Vector3 ground_move;

        [Range(0, 3)]
        public float ground_chk_r = 0.1f;

        [Header("音效檔案")]
        public AudioClip jump_sound;
        public AudioClip landing_sound;

        [Header("動畫參數")]
        public string walk = "走路開關";
        public string run = "跑步開關";
        public string get_hit = "受傷觸發";
        public string death = "死亡開關";
        public string anijump = "跳躍觸發";
        public string anigndchk = "是否在地板上";

        private AudioSource audiosource;
        private Rigidbody rgbody;
        private Animator anitor;
        public GameObject playerobject;
        #endregion

        #region Unity 資料類型
        /*
        //顏色 Color
        public Color color;
        public Color white=Color.white;
        public Color color1 = new Color(0.5f, 0.02f, 0.87f); //RGB
        public Color color2 = new Color(0.64f, 0.69f, 0.87f, 0.66f); //RGBA

        //座標 Vector2-4
        public Vector2 V2;
        public Vector2 V2Right = Vector2.right; //x=1,y=0
        public Vector2 V2up = Vector2.up; //x=0, y=1
        public Vector2 V2One = Vector2.one; //x=1, y=1
        public Vector2 V2c = new Vector2(0.5f, 8.7f);

        //按鍵 列舉資料 enum
        public KeyCode key;
        public KeyCode move = KeyCode.W;
        public KeyCode jump = KeyCode.Space;

        //遊戲資料類型：無法指定預設值
        //存放 project 專案內的資料
        public AudioClip sound;      //音效 mp3, ogg, wav
        public VideoClip video;      //影片 mp4
        public Sprite sprite;        //圖片 png, jpeg, 不支援gif
        public Texture2D texture2D;  //2D圖片 png, jpeg
        public Material material;    //材質球

        //元件 Component: 屬性面板上可折疊的
        public Transform trans;
        public Animation aniOld;
        public Animator aniNew;
        public Light lig;
        public Camera cam;
        */
        #endregion

        #region 屬性 Property
        /**屬性練習
        //儲存資料:與欄位相同
        //差異在於可設定存取權限 Get Set
        //屬性語法:修飾詞 資料類型 屬性名稱{取; 存;}
        public int readandwrite { get; set; }
        //唯獨屬性：只能取得 get
        public int read { get; }
        public int readvalue //唯獨屬性設值
        {
            get { return 88; } 
        }
        //唯寫屬性：禁止
        private int _hp;
        public int hp 
        { 
            get{ return _hp; }
            set{ _hp = value; } 
        }
        */

        public KeyCode keyjump { get; }
        #endregion

        #region 方法 Method
        //定義與實作較複雜程式的區塊，功能
        //方法語法：修飾詞 回傳資料類型 方法名稱(參數1, ...參數n){}
        //常用回傳類型：無傳回 void -不回傳資料
        //排版格式化：Ctrl+KD
        //自訂方法名稱為淡黃色(深色介面)，淺灰色(淺色介面)-沒有被呼叫
        //自訂方法名稱為亮黃色(深色介面)，棕色(淺色介面)-有被呼叫
        private int jumpp() { return 999; }
        //參數語法：資料類型 參數名稱 指定預設值
        //有預設值不用輸入引述，選填式參數
        //選填式參數只能放在()裡右邊

        private void skill(int dmg, string effect = "灰塵特效", string sound = "嘎嘎嘎")
        {
            print("有參數-傷害值：" + dmg);
            print("有參數-技能特效：" + effect);
            print("有參數-技能音效：" + sound);
        }

        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="speed"></param>
        private void move(float speed)
        {
            rgbody.velocity =
                Vector3.forward * movekey("Vertical") * speed + //前後速度(0,0,1)
                Vector3.right * movekey("Horizontal") * speed + //左右速度(1,0,0)
                Vector3.up * rgbody.velocity.y; //加回原有的y軸加速度(重力)
        }

        /// <summary>
        /// 移動按鍵輸入
        /// </summary>
        /// <returns>移動按鍵值</returns>
        private float movekey(string axisname)
        {
            return Input.GetAxis(axisname);
        }

        /// <summary>
        /// 地板檢查
        /// </summary>
        /// <returns>檢查結果</returns>
        private bool gnd_chk()
        {
            Collider[] hit = Physics.OverlapSphere((transform.position +
                transform.right * ground_move.x +
                transform.up * ground_move.y +
                transform.forward * ground_move.z),
                ground_chk_r, 1 << 3);
            ground_chk = hit.Length > 0;
            return hit.Length > 0;
        }

        /// <summary>
        /// 跳躍
        /// </summary>
        private void jump()
        {
            //print("是否在地面上：" + gnd_chk());
            if (gnd_chk() && Input.GetKeyDown(KeyCode.Space)) {
                rgbody.AddForce(transform.up * height);
            }
        }

        /// <summary>
        /// 更新動畫
        /// </summary>
        private void ani_re()
        {
            //if(Input.GetKey(KeyCode.W)|Input.GetKey(KeyCode.S)) anitor.SetBool(walk, true);
            // else anitor.SetBool(walk, false);
            anitor.SetBool(walk, Input.GetAxis("Vertical") != 0 | Input.GetAxis("Horizontal") != 0);
            anitor.SetBool(anigndchk, ground_chk);
            if (Input.GetKeyDown(KeyCode.Space)) anitor.SetTrigger(anijump);
        }

        #endregion

        #region 事件 Event
        private void Start()
        {
            #region 輸出方法
            /*
            print("哈囉，臥喔喔喔喔喔喔的");
            Debug.Log("一般訊息");
            Debug.LogWarning("警告訊息");
            Debug.LogError("錯誤訊息，假的，眼睛業障重");
            */
            #endregion

            #region 屬性練習
            /*
            //欄位與屬性 取得Get、設定Set
            print("欄位資料-移動速度：" + speed);
            print("欄位資料-讀寫屬性：" + readandwrite);
            speed = 20.5f;
            readandwrite = 66;
            print("修改後資料");
            print("欄位資料-移動速度：" + speed);
            print("欄位資料-讀寫屬性：" + readandwrite);

            print("唯獨屬性：" + read);
            print("唯獨屬性，有預設值：" + readvalue);

            print("HP：" + hp);
            hp = 1000;
            print("HP：" + hp);
            */
            #endregion
            audiosource = playerobject.GetComponent(typeof(AudioSource)) as AudioSource;
            rgbody = gameObject.GetComponent<Rigidbody>();
            anitor = GetComponent<Animator>();

            int j = jumpp();
            print("跳躍值：" + j);
            print("跳躍值++：" + (jumpp() + 1));
            skill(999);
            skill(9999, "Explotion!!");
            skill(9999, sound: "碰碰碰!");
        }

        private void Update()
        {
            jump();
            ani_re();
        }

        private void FixedUpdate()
        {
            move(speed);

        }
        #endregion

        //繪製圖示事件
        //在unity editor內繪製圖示輔助開發，發布後隱藏
        private void OnDrawGizmos()
        {
            //1.指定顏色
            //2.繪製圖型
            Gizmos.color = new Color(1, 0, 0.2f, 0.5f);
            Gizmos.DrawSphere(transform.position +
                transform.right * ground_move.x +
                transform.up * ground_move.y +
                transform.forward * ground_move.z,
                ground_chk_r);
        }
    }
}

