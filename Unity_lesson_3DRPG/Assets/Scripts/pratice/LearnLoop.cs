using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnLoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int i = 1;

        while (i < 6) {
            print("while°j°é:" + i);
            i++;
        }

        for(i=1; i<6; i++) {
            print("for°j°é:" + i);
        }
    }

    
}
