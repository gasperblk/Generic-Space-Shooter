using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField]
    private RawImage img;
    [SerializeField]
    private float scrollSpeed;

    // Update is called once per frame
    void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(0.0f, scrollSpeed) * Time.deltaTime, img.uvRect.size);
    }
}
