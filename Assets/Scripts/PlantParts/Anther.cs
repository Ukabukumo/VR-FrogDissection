using UnityEngine;

public class Anther : MonoBehaviour
{
    [SerializeField] private Transform begin;

    public Transform Begin
    {
        get { return begin; }
    }
}