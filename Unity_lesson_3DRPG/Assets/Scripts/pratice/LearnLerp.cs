using UnityEngine;

public class LearnLerp : MonoBehaviour
{
    public float a = 0, b = 100, c = 0, d = 100;

    private void Start()
    {
        print("a b¨âÂI´¡­È¡At=0.5 " + Mathf.Lerp(a, b, 0.5f));
       
    }

    private void Update()
    {
        d = Mathf.Lerp(c, d, 0.5f);

    }

    
}
