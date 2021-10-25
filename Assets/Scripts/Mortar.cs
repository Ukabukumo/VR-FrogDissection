using UnityEngine;

public class Mortar : MonoBehaviour
{
    [SerializeField] private GameObject pollenPref;

    private void OnTriggerEnter(Collider _other)
    {
        // Соприкосновение с пыльником
        if (_other.tag == "Anther")
        {
            // Уничтожение пыльника
            if (_other.gameObject != null)
            {
                Destroy(_other.gameObject);
            }

            // Создание пыльцы
            Instantiate(pollenPref, _other.transform.position, Quaternion.identity);
        }
    }
}
