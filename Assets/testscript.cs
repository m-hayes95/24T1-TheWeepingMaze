using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
    public GameObject goX, goY, goZ;

    void Start()
    {
        Vector3 xtest = new Vector3(1, 0, 0);
        Vector3 ytest = new Vector3(0, 1, 0);
        Vector3 ztest = new Vector3(0, 0, 1);
        for (int i = 0; i < 3; i++)
        {
            if (i == 0) Instantiate(goX, transform.position + xtest, Quaternion.identity);
            if (i == 1) Instantiate(goY, transform.position + ytest, Quaternion.identity);
            if (i == 2) Instantiate(goZ, transform.position + ztest, Quaternion.identity);
            if (i == 3) Debug.Log("Test Done");
        }
        
    }
}
