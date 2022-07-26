using UnityEngine;

public class Filament : MonoBehaviour
{
    [SerializeField] private Transform begin;
    [SerializeField] private Transform end;

    public Transform Begin
    {
        get { return begin; }
    }

    public Transform End
    {
        get { return end; }
    }

    public void AttachAnther(Anther _anther)
    {
        GetComponent<FixedJoint>().connectedBody = _anther.GetComponent<Rigidbody>();
    }
}
