using UnityEngine;

public class Petal : MonoBehaviour
{
    [SerializeField] private Transform begin;

    public Transform Begin
    {
        get { return begin; }
    }
}