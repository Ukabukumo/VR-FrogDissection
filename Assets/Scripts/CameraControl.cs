using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private OVRInput.Button leftRotateButton = OVRInput.Button.Three;
    private OVRInput.Button rightRotateButton = OVRInput.Button.Four;
    private Transform curTransform;
    
    private void Awake()
    {
        curTransform = transform;
    }

    private void Update()
    {
        if (OVRInput.GetDown(leftRotateButton))
        {
            curTransform.rotation *= Quaternion.Euler(0f, 90f, 0f);
        }

        if (OVRInput.GetDown(rightRotateButton))
        {
            curTransform.rotation *= Quaternion.Euler(0f, -90f, 0f);
        }
    }
}
