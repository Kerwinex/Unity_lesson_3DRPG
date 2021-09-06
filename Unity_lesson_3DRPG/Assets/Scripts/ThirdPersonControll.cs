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
    public float speed=10.5f;
    #endregion

    #region Unity 資料類型
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
    //存放project專案內的資料
    public AudioClip sound;      //音效 mp3, ogg, wav
    public VideoClip video;      //影片 mp4
    public Sprite sprite;        //圖片 png, jpeg, 不支援gif
    public Texture2D texture2D;  //2D圖片 png, jpeg
    public Material material;    //材質球



    #endregion

    #region 屬性 Property

    #endregion


    #region 方法 Method

    #endregion


    #region 事件 Event

    #endregion


}
