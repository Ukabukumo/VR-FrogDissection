using UnityEngine;

public class Leaf : MonoBehaviour
{
    [SerializeField] private Transform begin;

    public Transform Begin
    {
        get { return begin; }
    }
}