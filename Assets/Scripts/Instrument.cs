using UnityEngine;

public class Instrument : MonoBehaviour
{
    private FixedJoint fj;

    /// <summary>
    /// ������������� ������� � �����������.
    /// </summary>
    protected void AttachObject(Collider _object)
    {
        // ������� IndexTrigger �� ����� ����
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            // ���� ���������� � ����� ����
            if (CheckHand("LeftHand"))
            {
                // ���� �� ������� ��� ���������� FixedJoint
                if (fj == null)
                {
                    // ��������� �� ���������� ��������� FixedJoint
                    fj = gameObject.AddComponent<FixedJoint>();

                    // ������������� ������� � �����������
                    GetComponent<FixedJoint>().connectedBody = _object.GetComponent<Rigidbody>();
                }
            }
        }

        // ������� IndexTrigger �� ������ ����
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            // ���� ���������� � ������ ����
            if (CheckHand("RightHand"))
            {
                // ���� �� ������� ��� ���������� FixedJoint
                if (fj == null)
                {
                    // ��������� �� ���������� ��������� FixedJoint
                    fj = gameObject.AddComponent<FixedJoint>();

                    // ������������� ������� � �����������
                    GetComponent<FixedJoint>().connectedBody = _object.GetComponent<Rigidbody>();
                }
            }
        }
    }

    /// <summary>
    /// ������������ ������� �� �����������.
    /// </summary>
    protected void DetachObject()
    {
        // ������� IndexTrigger ��� HandTrigger �� ����� ����
        if (!OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            // ���� ���������� � ����� ����
            if (CheckHand("LeftHand"))
            {
                if (fj != null)
                {
                    // ����������� ������������ � ����������� ������
                    Destroy(fj);
                }
            }
        }

        // ������� IndexTrigger ��� HandTrigger �� ������ ����
        if (!OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            // ���� ���������� � ������ ����
            if (CheckHand("RightHand"))
            {
                if (fj != null)
                {
                    // ����������� ������������ � ����������� ������
                    Destroy(fj);
                }
            }
        }

        // ����, � ������� ��������� ����������
        OVRGrabber _currentHand = GetComponent<OVRGrabbable>().grabbedBy;

        // ���� ���������� �� � ����
        if (_currentHand == null)
        {
            if (fj != null)
            {
                // ����������� ������������ � ����������� ������
                Destroy(fj);
            }
        }
    }

    /// <summary>
    /// ��������.
    /// </summary>
    protected void Crush(Collider _object, GameObject _objectPref, string _plantCode)
    {
        // ����������� ��������� �������
        if (_object.gameObject != null)
        {
            Destroy(_object.gameObject);
        }

        // �������� ������ �������
        GameObject _newObject = Instantiate(_objectPref, _object.transform.position, Quaternion.identity);
        
        // �������� ���� �������� �������
        _newObject.GetComponent<PlantCode>().Code = _plantCode;
    }

    /// <summary>
    /// �������� ����, � ������� ��������� ����������.
    /// </summary>
    protected bool CheckHand(string _handName)
    {
        // ����, � ������� ��������� ����������
        OVRGrabber _currentHand = GetComponent<OVRGrabbable>().grabbedBy;

        if (_currentHand != null)
        {
            // ���� ���������� � ��������������� ����
            if (_currentHand.tag == _handName)
            {
                return true;
            }
        }

        return false;
    }
}