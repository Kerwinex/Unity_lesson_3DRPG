using UnityEngine;
using UnityEngine.Video;

//�׹��� ���O ���O�W��:�~�����O
//MonoBehaviour unity�����O�A�n���b����Wĳ�w�n�~��
//�~�ӫ�|�ɦ������O������
//�`�Φ����G��� Field�B�ݩ� Property(�ܼ�)�B��k Method�B�ƥ� Event
//�b���O�Φ����W��W�[�T���׽u�i�H�W�[�K�n
/// <summary>
/// �o�̬O�K�n
/// �d�йl 20210906
/// �ĤT�H�ٱ��
/// ���ʡB���D
/// </summary>

namespace Ker.pratice
{
    public class ThirdPersonControll : MonoBehaviour
    {
        #region ��� Field
        //�ܼƭȷ|�Hunity�����ݩʭ��O���D�A�}�����|���w���w�]�ȡA��unity���ϥ�reset�K�|�אּ�}��������
        //����ݩ� Attribute�G���U�����
        //����ݩʻy�k[�ݩʦW��(�ݩʭ�)]
        //Header ���D
        //Tooltip ���ܡG�ƹ����d�b���W�ٷ|��ܼu�X����
        //Range �d��G�i�ϥΦb�ƭȸ�ƤW
        [Header("���ʳt��"), Tooltip("�Ψӽվ㨤�Ⲿ�ʳt��"), Range(1, 500)]
        public float speed = 10.5f;
        [Header("���D����"), Tooltip("�Ψӽվ㨤����D����"), Range(0, 1000)]
        public int height = 200;

        [Header("�ˬd�a�����"), Tooltip("��������O�_�b�a�O�W")]
        public bool ground_chk = false;
        public Vector3 ground_move;

        [Range(0, 3)]
        public float ground_chk_r = 0.1f;

        [Header("�����ɮ�")]
        public AudioClip jump_sound;
        public AudioClip landing_sound;

        [Header("�ʵe�Ѽ�")]
        public string walk = "�����}��";
        public string run = "�]�B�}��";
        public string get_hit = "����Ĳ�o";
        public string death = "���`�}��";
        public string anijump = "���DĲ�o";
        public string anigndchk = "�O�_�b�a�O�W";

        private AudioSource audiosource;
        private Rigidbody rgbody;
        private Animator anitor;
        public GameObject playerobject;
        #endregion

        #region Unity �������
        /*
        //�C�� Color
        public Color color;
        public Color white=Color.white;
        public Color color1 = new Color(0.5f, 0.02f, 0.87f); //RGB
        public Color color2 = new Color(0.64f, 0.69f, 0.87f, 0.66f); //RGBA

        //�y�� Vector2-4
        public Vector2 V2;
        public Vector2 V2Right = Vector2.right; //x=1,y=0
        public Vector2 V2up = Vector2.up; //x=0, y=1
        public Vector2 V2One = Vector2.one; //x=1, y=1
        public Vector2 V2c = new Vector2(0.5f, 8.7f);

        //���� �C�|��� enum
        public KeyCode key;
        public KeyCode move = KeyCode.W;
        public KeyCode jump = KeyCode.Space;

        //�C����������G�L�k���w�w�]��
        //�s�� project �M�פ������
        public AudioClip sound;      //���� mp3, ogg, wav
        public VideoClip video;      //�v�� mp4
        public Sprite sprite;        //�Ϥ� png, jpeg, ���䴩gif
        public Texture2D texture2D;  //2D�Ϥ� png, jpeg
        public Material material;    //����y

        //���� Component: �ݩʭ��O�W�i���|��
        public Transform trans;
        public Animation aniOld;
        public Animator aniNew;
        public Light lig;
        public Camera cam;
        */
        #endregion

        #region �ݩ� Property
        /**�ݩʽm��
        //�x�s���:�P���ۦP
        //�t���b��i�]�w�s���v�� Get Set
        //�ݩʻy�k:�׹��� ������� �ݩʦW��{��; �s;}
        public int readandwrite { get; set; }
        //�߿W�ݩʡG�u����o get
        public int read { get; }
        public int readvalue //�߿W�ݩʳ]��
        {
            get { return 88; } 
        }
        //�߼g�ݩʡG�T��
        private int _hp;
        public int hp 
        { 
            get{ return _hp; }
            set{ _hp = value; } 
        }
        */

        public KeyCode keyjump { get; }
        #endregion

        #region ��k Method
        //�w�q�P��@�������{�����϶��A�\��
        //��k�y�k�G�׹��� �^�Ǹ������ ��k�W��(�Ѽ�1, ...�Ѽ�n){}
        //�`�Φ^�������G�L�Ǧ^ void -���^�Ǹ��
        //�ƪ��榡�ơGCtrl+KD
        //�ۭq��k�W�٬��H����(�`�⤶��)�A�L�Ǧ�(�L�⤶��)-�S���Q�I�s
        //�ۭq��k�W�٬��G����(�`�⤶��)�A�Ħ�(�L�⤶��)-���Q�I�s
        private int jumpp() { return 999; }
        //�Ѽƻy�k�G������� �ѼƦW�� ���w�w�]��
        //���w�]�Ȥ��ο�J�ޭz�A��񦡰Ѽ�
        //��񦡰Ѽƥu���b()�̥k��

        private void skill(int dmg, string effect = "�ǹЯS��", string sound = "�ǹǹ�")
        {
            print("���Ѽ�-�ˮ`�ȡG" + dmg);
            print("���Ѽ�-�ޯ�S�ġG" + effect);
            print("���Ѽ�-�ޯ୵�ġG" + sound);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="speed"></param>
        private void move(float speed)
        {
            rgbody.velocity =
                Vector3.forward * movekey("Vertical") * speed + //�e��t��(0,0,1)
                Vector3.right * movekey("Horizontal") * speed + //���k�t��(1,0,0)
                Vector3.up * rgbody.velocity.y; //�[�^�즳��y�b�[�t��(���O)
        }

        /// <summary>
        /// ���ʫ����J
        /// </summary>
        /// <returns>���ʫ����</returns>
        private float movekey(string axisname)
        {
            return Input.GetAxis(axisname);
        }

        /// <summary>
        /// �a�O�ˬd
        /// </summary>
        /// <returns>�ˬd���G</returns>
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
        /// ���D
        /// </summary>
        private void jump()
        {
            //print("�O�_�b�a���W�G" + gnd_chk());
            if (gnd_chk() && Input.GetKeyDown(KeyCode.Space)) {
                rgbody.AddForce(transform.up * height);
            }
        }

        /// <summary>
        /// ��s�ʵe
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

        #region �ƥ� Event
        private void Start()
        {
            #region ��X��k
            /*
            print("���o�A�׳�����᪺");
            Debug.Log("�@��T��");
            Debug.LogWarning("ĵ�i�T��");
            Debug.LogError("���~�T���A�����A�����~�٭�");
            */
            #endregion

            #region �ݩʽm��
            /*
            //���P�ݩ� ���oGet�B�]�wSet
            print("�����-���ʳt�סG" + speed);
            print("�����-Ū�g�ݩʡG" + readandwrite);
            speed = 20.5f;
            readandwrite = 66;
            print("�ק����");
            print("�����-���ʳt�סG" + speed);
            print("�����-Ū�g�ݩʡG" + readandwrite);

            print("�߿W�ݩʡG" + read);
            print("�߿W�ݩʡA���w�]�ȡG" + readvalue);

            print("HP�G" + hp);
            hp = 1000;
            print("HP�G" + hp);
            */
            #endregion
            audiosource = playerobject.GetComponent(typeof(AudioSource)) as AudioSource;
            rgbody = gameObject.GetComponent<Rigidbody>();
            anitor = GetComponent<Animator>();

            int j = jumpp();
            print("���D�ȡG" + j);
            print("���D��++�G" + (jumpp() + 1));
            skill(999);
            skill(9999, "Explotion!!");
            skill(9999, sound: "�I�I�I!");
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

        //ø�s�ϥܨƥ�
        //�bunity editor��ø�s�ϥܻ��U�}�o�A�o��������
        private void OnDrawGizmos()
        {
            //1.���w�C��
            //2.ø�s�ϫ�
            Gizmos.color = new Color(1, 0, 0.2f, 0.5f);
            Gizmos.DrawSphere(transform.position +
                transform.right * ground_move.x +
                transform.up * ground_move.y +
                transform.forward * ground_move.z,
                ground_chk_r);
        }
    }
}

