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
        if (_other.tag == "Anther" || _other.tag == "RipenedBox")
        {
            AttachObject(_other);
        }
    }
}
