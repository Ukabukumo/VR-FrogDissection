using UnityEngine;
using System.Collections;
using System;

public class Stigma : MonoBehaviour
{
    [SerializeField] private Material pollinatedMat;
    [SerializeField] private Material ripenedMat;
    [SerializeField] private GameObject timeRewinderPref;
    [SerializeField] private Transform begin;

    private const int TIME_RIPEN = 10;  // Время созревания
    private TimeRewinder tr;            // Инструмент для перемотки времени
    private Vector3 originScale;        // Первоначальный размер рыльца

    public Transform Begin
    {
        get { return begin; }
    }

    private void Awake()
    {
        // Запоминаем первоначальный размер
        originScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider _other)
    {
        // Соприкосновение с пыльцой
        if (_other.tag == "Pollen")
        {
            Pollination(_other);
            StartCoroutine(RewindTime(TIME_RIPEN));
        }
    }

    /// <summary>
    /// Опыление рыльца пестика.
    /// </summary>
    private void Pollination(Collider _pollen)
    {
        // Определяем код нового растения
        string _firstCode = GetComponent<PlantCode>().Code;
        string _secondCode = _pollen.GetComponent<PlantCode>().Code;

        if (Convert.ToInt32(_firstCode) < Convert.ToInt32(_secondCode))
        {
            GetComponent<PlantCode>().Code = _firstCode + _secondCode;
        }

        else
        {
            GetComponent<PlantCode>().Code = _secondCode + _firstCode;
        }

        // Уничтожение пыльцы
        Destroy(_pollen.gameObject);

        // Показываем, что рыльце пестика опылено
        GetComponent<MeshRenderer>().material = pollinatedMat;
    }

    /// <summary>
    /// Перемотка времени до нужного момента.
    /// </summary>
    private IEnumerator RewindTime(int _timeBound)
    {
        // Создаём объект для перемотки времени
        tr = Instantiate(timeRewinderPref).GetComponent<TimeRewinder>();
        tr.TimeRewinderInit(_timeBound);

        while (tr != null)
        {
            // Ожидание конца кадра
            yield return null;

            // Увеличение размеров коробочки
            transform.localScale = originScale * (1f + tr.CurrentTime / 10);
        }

        // Показываем, что коробочка созрела
        GetComponent<MeshRenderer>().material = ripenedMat;
        tag = "RipenedBox";
    }
}
