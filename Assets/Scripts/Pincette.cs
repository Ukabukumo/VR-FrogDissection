using UnityEngine;

public class Pincette : MonoBehaviour
{
    private void OnTriggerStay(Collider _other)
    {
        // ��������������� � ���������
        if (_other.tag == "Anther")
        {
            // ������� �� �����
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                // ������������� �������� � �������
                GetComponent<FixedJoint>().connectedBody = _other.GetComponent<Rigidbody>();
            }

            // ������� �����
            else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
            {
                // ����������� ������� �� �������
                GetComponent<FixedJoint>().connectedBody = null;
            }
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        // ��������� ��������������� � ���������
        if (_other.tag == "Ansther")
        {
            // ����������� ������� �� �������
            GetComponent<FixedJoint>().connectedBody = null;
        }
    }
}
