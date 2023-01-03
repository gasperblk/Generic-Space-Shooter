using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBorder : MonoBehaviour
{
    Camera cam;
    private Vector3 downLeft;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        downLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, transform.position.y - cam.transform.position.y));
    }

    void Update()
    {
        if (transform.position.y < downLeft.y)
        {
            //gameObject.Selfdestruct();
        }
    }
}
