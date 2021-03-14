using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cameraToReset;
    Quaternion originalRotationValue;
    Vector3 originalPos;

    void Start()
    {
        originalPos = new Vector3(cameraToReset.transform.position.x, cameraToReset.transform.position.y, cameraToReset.transform.position.z);
        originalRotationValue = cameraToReset.transform.rotation;

    }
    public void resetCam()
    {
        cameraToReset.transform.position = originalPos;
        cameraToReset.transform.rotation = originalRotationValue;
    }
}
