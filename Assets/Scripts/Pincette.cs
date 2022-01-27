using UnityEngine;

public class Pincette : Instrument
{
    private void Update()
    {
        DetachObject();
    }

    private void OnTriggerStay(Collider _other)
    {
        // ��������������� � ��������� ��� c ��������� ����������
        if (_other.tag == "RipenedBox" || _other.tag == "Anther")
        {
            AttachObject(_other);
        }
    }
}
