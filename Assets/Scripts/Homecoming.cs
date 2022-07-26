using UnityEngine;

public class Homecoming : MonoBehaviour
{
    [SerializeField] private Vector3 bounds = new Vector3(3f, 0.5f, 3f);
    private Vector3 originPos;
    private Quaternion originRot;

    private void Awake()
    {
        originPos = transform.position;
        originRot = transform.rotation;
    }

    private void Update()
    {
        if (Mathf.Abs(originPos.x - transform.position.x) > bounds.x ||
            Mathf.Abs(originPos.y - transform.position.y) > bounds.y ||
            Mathf.Abs(originPos.z - transform.position.z) > bounds.z)
        {
            if (GetComponent<OVRGrabbable>().grabbedBy == null)
            {
                transform.position = originPos;
                transform.rotation = originRot;
            }
        }
    }
}
