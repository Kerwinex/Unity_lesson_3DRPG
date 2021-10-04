using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("移動速度"), Tooltip("用來調整怪物移動速度"), Range(1, 10)]
    public float speed = 3.5f;
    [Header("攻擊力"), Tooltip("用來調整怪物攻擊力"), Range(0, 500)]
    public int atk = 100;
    [Header("血量"), Tooltip("用來調整怪物血量"), Range(0, 5000)]
    public int hp = 350;
    [Header("追蹤範圍"), Tooltip("偵測角色範圍"),Range(0,50)]
    public float player_chk_r = 30f;
    [Header("追蹤位移")]
    public Vector3 vector_track;
    [Header("掉落道具")]
    public GameObject item_drop;
    [Header("掉落機率")]
    public float drop_rate = 1f;

    [Header("音效檔案")]
    public AudioClip drop_sound;
    public AudioClip hurt_sound;
    public AudioClip attack_sound;
    
    private AudioSource audiosource;
    private Rigidbody2D rgbody;
    private Animator anitor;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
