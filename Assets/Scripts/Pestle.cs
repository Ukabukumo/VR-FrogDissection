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
            CrushAnther(_other, pollenPref);
        }

        // Соприкосновение с созревшей коробочкой
        if (_other.tag == "RipenedBox")
        {
            Debug.Log("RipenedBox");


            CrushAnther(_other, seedsPref);
        }
    }
}
