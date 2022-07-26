using UnityEngine;

public class PotSpawner : MonoBehaviour
{
    [SerializeField] private GameObject potPref;

    private bool isEmpty = true;

    private void Update()
    {
        if (isEmpty)
        {
            Instantiate(potPref, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            isEmpty = false;
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.tag == "Pot")
        {
            _other.tag = "Untagged";
            isEmpty = true;
        }
    }
}
