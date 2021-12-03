using UnityEngine;

public class Sponge : Instrument
{
    private void Update()
    {
        DetachObject();
    }

    private void OnTriggerStay(Collider _other)
    {
        // Соприкосновение с пыльцой или семенами
        if (_other.tag == "Pollen" || _other.tag == "Seeds")
        {
            AttachObject(_other);
        }
    }
}
