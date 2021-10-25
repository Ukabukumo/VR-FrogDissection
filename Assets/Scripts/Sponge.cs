using UnityEngine;

public class Sponge : MonoBehaviour
{
    private void OnTriggerEnter(Collider _other)
    {
        // ��������������� � �������
        if (_other.tag == "Pollen")
        {
            // ������� �� �����
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                // ������������� ������
                GetComponent<FixedJoint>().connectedBody = _other.GetComponent<Rigidbody>();
            }
            
            // ������� �����
            else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
            {
                // ������������ ������
                GetComponent<FixedJoint>().connectedBody = null;
            }
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        // ��������� ��������������� � �������
        if (_other.tag == "Pollen")
        {
            // ������������ ������
            GetComponent<FixedJoint>().connectedBody = null;
        }
    }
}
