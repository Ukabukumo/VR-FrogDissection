using UnityEngine;

public class Sponge : MonoBehaviour
{
    private void OnTriggerEnter(Collider _other)
    {
        // Соприкосновение с пыльцой
        if (_other.tag == "Pollen")
        {
            // Нажатие на курок
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                // Присоединение пыльцы
                GetComponent<FixedJoint>().connectedBody = _other.GetComponent<Rigidbody>();
            }
            
            // Отжатие курка
            else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
            {
                // Отсоединение пыльцы
                GetComponent<FixedJoint>().connectedBody = null;
            }
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        // Окончание соприкосновения с пыльцой
        if (_other.tag == "Pollen")
        {
            // Отсоединение пыльцы
            GetComponent<FixedJoint>().connectedBody = null;
        }
    }
}
