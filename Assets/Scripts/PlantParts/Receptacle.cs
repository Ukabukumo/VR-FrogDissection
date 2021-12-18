using UnityEngine;

public class Receptacle : MonoBehaviour
{
    [SerializeField] private Transform begin;
    [SerializeField] private Transform ovaryConnect;
    [SerializeField] private Transform[] petalConnects;
    [SerializeField] private Transform[] sepalConnects;
    [SerializeField] private Transform[] filamentConnects;

    public Transform Begin
    {
        get { return begin; }
    }

    public Transform OvaryConnect
    {
        get { return ovaryConnect; }
    }

    public Transform[] PetalConnects
    {
        get { return petalConnects; }
    }

    public Transform[] SepalConnects
    {
        get { return sepalConnects; }
    }

    public Transform[] FilamentConnects
    {
        get { return filamentConnects; }
    }
}