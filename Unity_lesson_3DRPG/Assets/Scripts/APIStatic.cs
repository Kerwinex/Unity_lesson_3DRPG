using UnityEngine;

public class APIStatic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region
        float r = Random.value;
        print("���o�R�A�ݩʡA�H���ȡG" + r);
        //Cursor.visible = false;

        float rndf = Random.Range(10f, 20f);
        print("�H���B�I " + rndf);

        int rndi = Random.Range(1, 5);
        print("�H���B�I " + rndi);
        #endregion

        int camnum = Camera.allCamerasCount;
        
        double pi = Mathf.PI;
        Physics2D.gravity= new Vector2(0, -20);        
        Time.timeScale = 0.5f;
        print("floor�h�p���I�G" + Mathf.Floor(9.999f));
        print("round�h�p���I�G" + Mathf.Round(9.999f));
        print("roundtoint�h�p���I" + Mathf.RoundToInt(9.999f));;
        print(Vector3.Distance(new Vector3(1, 1, 1), new Vector3(22, 22, 22)));
        Application.OpenURL("https://unity.com/");
    }

    // Update is called once per frame
    void Update()
    {
        print("�L�F�h�[" + Time.time);
        print("�O�_��J���N�� " + Input.anyKey);
        print("�O�_��J�ť��� " + Input.GetKeyDown(KeyCode.Space));
    }
}
