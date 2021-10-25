using UnityEngine;

public class Pincette : MonoBehaviour
{
    private void OnTriggerStay(Collider _other)
    {
        // Соприкосновение с пыльником
        if (_other.tag == "Anther")
        {
            // Нажатие на курок
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                // Присоединение пыльника к пинцету
                GetComponent<FixedJoint>().connectedBody = _other.GetComponent<Rigidbody>();
            }

            // Отжатие курка
            else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
            {
                // Отсоединяем пыльник от пинцета
                GetComponent<FixedJoint>().connectedBody = null;
            }
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        // Окончание соприкосновения с пыльником
        if (_other.tag == "Ansther")
        {
            // Отсоединяем пыльник от пинцета
            GetComponent<FixedJoint>().connectedBody = null;
        }
    }
}
