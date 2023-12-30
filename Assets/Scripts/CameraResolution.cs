using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    void Awake()
    {
        Camera camrea=GetComponent<Camera>();
        Rect rect=GetComponent<Camera>().rect;

        float scaleHeight=((float)Screen.width/Screen.height)/((float)9/21);
        float scaleWidth=1f/scaleHeight;

        if(scaleHeight<1)
        {
            rect.height=scaleHeight;
            rect.y=(1f-scaleHeight)/2f;
        }

        GetComponent<Camera>().rect=rect;
    }

}
