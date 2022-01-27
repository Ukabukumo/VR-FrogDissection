using UnityEngine;

public class PotSpawner : MonoBehaviour
{
    [SerializeField] private GameObject potPref;

    private bool isEmpty = true;

    private void Update()
    {
        // Если точка спавна пуста
        if (isEmpty)
        {
            // Создаём новый горшок
            Instantiate(potPref, transform.position, Quaternion.Euler(-90f, 0f, 0f));

            // Указываем, что точка спавна занята
            isEmpty = false;
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        // Если горшок вышел из точки спавна
        if (_other.tag == "Pot")
        {
            // Убираем тег во избежание повторных срабатываний
            _other.tag = "Untagged";

            // Указываем, что точка спавна пуста
            isEmpty = true;
        }
    }
}
