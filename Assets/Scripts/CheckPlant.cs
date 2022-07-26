using UnityEngine;

public class CheckPlant : MonoBehaviour
{
    private bool correctPlant;

    public bool CorrectPlant
    {
        get { return correctPlant; }
    }

    private void Awake()
    {
        correctPlant = false;
    }

    private void Start()
    {
        GetComponent<PlantCode>().RandomCode();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.tag == "Pot")
        {
            if (_other.GetComponent<PlantCode>().Code == GetComponent<PlantCode>().Code)
            {
                correctPlant = true;
            }
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.tag == "Pot")
        {
            if (_other.GetComponent<PlantCode>().Code == GetComponent<PlantCode>().Code)
            {
                correctPlant = false;
            }
        }
    }
}
