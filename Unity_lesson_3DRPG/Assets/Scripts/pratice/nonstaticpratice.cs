using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nonstaticpratice : MonoBehaviour
{

    public Camera cam;
    public SpriteRenderer spr;
    public Rigidbody2D rg2d;
   
    
    // Start is called before the first frame update
    void Start()
    {
        print("��v���`�סG" + cam.depth);
        print("�Ϥ��C��G" + spr.color);
        cam.backgroundColor = Random.ColorHSV();
        spr.flipY = true;          
    }

    // Update is called once per frame
    void Update()
    {
        spr.transform.Rotate(0, 5, 0);
        //rg2d.gravityScale = -1;
        rg2d.AddForce(new Vector2(0, 10));
    }
}
