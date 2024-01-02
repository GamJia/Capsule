using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    void Awake()
    {
        Camera camrea=GetComponent<Camera>();
        Rect rect=GetComponent<Camera>().rect;

        float scaleHeight=((float)Screen.width/Screen.height)/((float)9/24);
        float scaleWidth=1f/scaleHeight;

        if(scaleHeight<1)
        {
            rect.height=scaleHeight;
            rect.y=(1f-scaleHeight)/2f;
        }

        scaleHeight=((float)Screen.width/Screen.height)/((float)9/16);
        scaleWidth=1f/scaleHeight;

        if(scaleHeight>1)
        {
            rect.width=scaleWidth;
            rect.x=(1f-scaleWidth)/2f;
        }

        GetComponent<Camera>().rect=rect;
    }

}
