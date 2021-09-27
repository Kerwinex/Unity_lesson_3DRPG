using UnityEngine;

public class APIStatic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region
        float r = Random.value;
        print("取得靜態屬性，隨機值：" + r);
        //Cursor.visible = false;

        float rndf = Random.Range(10f, 20f);
        print("隨機浮點 " + rndf);

        int rndi = Random.Range(1, 5);
        print("隨機浮點 " + rndi);
        #endregion

        int camnum = Camera.allCamerasCount;
        
        double pi = Mathf.PI;
        Physics2D.gravity= new Vector2(0, -20);        
        Time.timeScale = 0.5f;
        print("floor去小數點：" + Mathf.Floor(9.999f));
        print("round去小數點：" + Mathf.Round(9.999f));
        print("roundtoint去小數點" + Mathf.RoundToInt(9.999f));;
        print(Vector3.Distance(new Vector3(1, 1, 1), new Vector3(22, 22, 22)));
        Application.OpenURL("https://unity.com/");
    }

    // Update is called once per frame
    void Update()
    {
        print("過了多久" + Time.time);
        print("是否輸入任意鍵 " + Input.anyKey);
        print("是否輸入空白鍵 " + Input.GetKeyDown(KeyCode.Space));
    }
}
