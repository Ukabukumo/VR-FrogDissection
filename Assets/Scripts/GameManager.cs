using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Plant plantPref;
    [SerializeField] private Transform firstPlant;
    [SerializeField] private Transform secondPlant;

    public void Start()
    {
        // �������� ���� ������ ��������
        Plant _first = Instantiate(plantPref, firstPlant.position, Quaternion.Euler(-90f, 0f, 0f));
        Plant _second = Instantiate(plantPref, secondPlant.position, Quaternion.Euler(-90f, 0f, 0f));

        //_first.Time = 0f;
        //_second.Time = 0f;

        _first.CreatePlant("01", _first.gameObject);
        _second.CreatePlant("10", _second.gameObject);
    }
}
