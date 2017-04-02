using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lol : MonoBehaviour {

    public int intBooiiii = 0;
    
	void Start ()
    {
        Calculate();
	}

    void Calculate()
    {
        intBooiiii = 0;
        for (int i = 0; i < 1000; i++)
        {
            if (IsInt(i / 3f) || IsInt(i / 5f))
            {
                intBooiiii += i;
            }
        }
    }

    bool IsInt(float f)
    {
        return Mathf.Floor(f) == f;
    }
}
