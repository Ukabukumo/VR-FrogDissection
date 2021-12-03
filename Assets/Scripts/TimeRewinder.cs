using UnityEngine;
using System.Collections;

public class TimeRewinder : MonoBehaviour
{
    private float currentTime = 0f;       // Текущее время
    private int rewindSpeed = 1;          // Скорость перемотки
    private int timeBound = 10;           // Сколько времени нужно перемотать
    private OVRInput.Button rewindButton = OVRInput.Button.One; // Кнопка перемотки времени

    /// <summary>
    /// Возвращает время с начала перемотки
    /// </summary>
    public float CurrentTime
    {
        get { return currentTime; }
    }

    /// <summary>
    /// Инициализация инструмента для перемотки времени.
    /// </summary>
    public void TimeRewinderInit(int _timeBound)
    {
        timeBound = _timeBound;

        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        // Перематываем пока не дошли до необходимого времени
        while ( (currentTime * rewindSpeed) < timeBound)
        {
            // Ожидание конца кадра
            yield return null;

            if (OVRInput.Get(rewindButton))
            {
                // Добавляем прошедшее время
                currentTime += Time.deltaTime;
            }
        }

        // Удаляем инструмент для перемотки времени
        Destroy(gameObject);
    }
}
