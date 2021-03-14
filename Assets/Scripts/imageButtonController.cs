using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imageButtonController : MonoBehaviour
{
    public string ulrForImage;

    // Update is called once per frame
    public void showImage()
    {
        Application.OpenURL(ulrForImage);
    }
}
