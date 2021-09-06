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
    public float speed=10.5f;
    #endregion

    #region Unity �������
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
    //�s��project�M�פ������
    public AudioClip sound;      //���� mp3, ogg, wav
    public VideoClip video;      //�v�� mp4
    public Sprite sprite;        //�Ϥ� png, jpeg, ���䴩gif
    public Texture2D texture2D;  //2D�Ϥ� png, jpeg
    public Material material;    //����y



    #endregion

    #region �ݩ� Property

    #endregion


    #region ��k Method

    #endregion


    #region �ƥ� Event

    #endregion


}
