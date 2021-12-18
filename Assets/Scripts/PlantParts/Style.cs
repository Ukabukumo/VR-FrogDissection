using UnityEngine;

public class Style : MonoBehaviour
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

    /// <summary>
    /// Присоединение рыльца к столбику пестика
    /// </summary>
    /// <param name="_stigma"></param>
    public void AttachStigma(Stigma _stigma)
    {
        GetComponent<FixedJoint>().connectedBody = _stigma.GetComponent<Rigidbody>();
    }
}