using UnityEngine;
using System.Collections;

public class Stigma : MonoBehaviour
{
    [SerializeField] private Material pollinatedMat;
    [SerializeField] private Material ripenedMat;
    [SerializeField] private GameObject timeRewinderPref;
    [SerializeField] private Transform begin;

    private const int TIME_RIPEN = 10;  // ����� ����������
    private TimeRewinder tr;            // ���������� ��� ��������� �������
    private Vector3 originScale;        // �������������� ������ ������

    public Transform Begin
    {
        get { return begin; }
    }

    private void Awake()
    {
        // ���������� �������������� ������
        originScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider _other)
    {
        // ��������������� � �������
        if (_other.tag == "Pollen")
        {
            Pollination(_other);
            StartCoroutine(RewindTime(TIME_RIPEN));
        }
    }

    /// <summary>
    /// �������� ������ �������.
    /// </summary>
    private void Pollination(Collider _pollen)
    {
        // ����������� ������
        Destroy(_pollen.gameObject);

        // ����������, ��� ������ ������� �������
        GetComponent<MeshRenderer>().material = pollinatedMat;
    }

    /// <summary>
    /// ��������� ������� �� ������� �������.
    /// </summary>
    private IEnumerator RewindTime(int _timeBound)
    {
        // ������ ������ ��� ��������� �������
        tr = Instantiate(timeRewinderPref).GetComponent<TimeRewinder>();
        tr.TimeRewinderInit(_timeBound);

        while (tr != null)
        {
            // �������� ����� �����
            yield return null;

            // ���������� �������� ���������
            transform.localScale = originScale * (1f + tr.CurrentTime / 10);
        }

        // ����������, ��� ��������� �������
        GetComponent<MeshRenderer>().material = ripenedMat;
        tag = "RipenedBox";
    }
}
