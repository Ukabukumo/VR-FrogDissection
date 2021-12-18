using UnityEngine;

public class Stem : MonoBehaviour
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
}