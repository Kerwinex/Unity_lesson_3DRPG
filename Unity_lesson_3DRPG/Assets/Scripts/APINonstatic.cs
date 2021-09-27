using UnityEngine;

public class APINonstatic : MonoBehaviour
{
    public Transform trans;
    public Camera cam;
    public Light lig;
    // Start is called before the first frame update
    void Start()
    {
        print("座標位置 " + trans.position);
        print("深度 " + cam.depth);

        trans.position = new Vector3(12, 34, 56);
        cam.depth = 7;

        lig.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
