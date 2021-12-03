using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private OVRInput.Button leftRotateButton = OVRInput.Button.Three; // Кнопка поворота камеры ВЛЕВО на 90 градусов
    private OVRInput.Button rightRotateButton = OVRInput.Button.Four; // Кнопка поворота камеры ВПРАВО на 90 градусов
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
