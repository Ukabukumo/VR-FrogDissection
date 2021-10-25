using UnityEngine;

public class Mortar : MonoBehaviour
{
    [SerializeField] private GameObject pollenPref;

    private void OnTriggerEnter(Collider _other)
    {
        // ��������������� � ���������
        if (_other.tag == "Anther")
        {
            // ����������� ��������
            if (_other.gameObject != null)
            {
                Destroy(_other.gameObject);
            }

            // �������� ������
            Instantiate(pollenPref, _other.transform.position, Quaternion.identity);
        }
    }
}
