using UnityEngine;

public class Pestle : Instrument
{
    [SerializeField] private GameObject pollenPref;
    [SerializeField] private GameObject seedsPref;

    private void OnTriggerEnter(Collider _other)
    {
        // Соприкосновение с пыльником
        if (_other.tag == "Anther")
        {
            string _plantCode = _other.GetComponent<PlantCode>().Code;
            Crush(_other, pollenPref, _plantCode);
        }

        // Соприкосновение с созревшей коробочкой
        if (_other.tag == "RipenedBox")
        {
            string _plantCode = _other.GetComponent<PlantCode>().Code;
            Crush(_other, seedsPref, _plantCode);
        }
    }
}
