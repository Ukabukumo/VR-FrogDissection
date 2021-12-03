using UnityEngine;
using System.Collections;

public class Stigma : MonoBehaviour
{
    [SerializeField] Material pollinatedMat;
    [SerializeField] Material ripenedMat;
    [SerializeField] GameObject timeRewinderPref;

    private const int TIME_RIPEN = 10;  // Время созревания
    private TimeRewinder tr;            // Инструмент для перемотки времени
    private Vector3 originScale;        // Первоначальный размер рыльца

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
