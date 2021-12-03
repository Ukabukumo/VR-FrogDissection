using UnityEngine;

public class Instrument : MonoBehaviour
{
    private FixedJoint fj;

    /// <summary>
    /// Присоединение объекта к инструменту.
    /// </summary>
    protected void AttachObject(Collider _object)
    {
        // Нажатие IndexTrigger на ЛЕВОЙ руке
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            // Если инструмент в ЛЕВОЙ руке
            if (CheckHand("LeftHand"))
            {
                // Если на объекте нет компонента FixedJoint
                if (fj == null)
                {
                    // Добавляем на инструмент компонент FixedJoint
                    fj = gameObject.AddComponent<FixedJoint>();

                    // Присоединение объекта к инструменту
                    GetComponent<FixedJoint>().connectedBody = _object.GetComponent<Rigidbody>();
                }
            }
        }

        // Нажатие IndexTrigger на ПРАВОЙ руке
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            // Если инструмент в правой руке
            if (CheckHand("RightHand"))
            {
                // Если на объекте нет компонента FixedJoint
                if (fj == null)
                {
                    // Добавляем на инструмент компонент FixedJoint
                    fj = gameObject.AddComponent<FixedJoint>();

                    // Присоединение объекта к инструменту
                    GetComponent<FixedJoint>().connectedBody = _object.GetComponent<Rigidbody>();
                }
            }
        }
    }

    /// <summary>
    /// Отсоединение объекта от инструмента.
    /// </summary>
    protected void DetachObject()
    {
        // Отжатие IndexTrigger или HandTrigger на ЛЕВОЙ руке
        if (!OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            // Если инструмент в ЛЕВОЙ руке
            if (CheckHand("LeftHand"))
            {
                if (fj != null)
                {
                    // Отсоединяем прикреплённый к инструменту объект
                    Destroy(fj);
                }
            }
        }

        // Отжатие IndexTrigger или HandTrigger на ПРАВОЙ руке
        if (!OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            // Если инструмент в ПРАВОЙ руке
            if (CheckHand("RightHand"))
            {
                if (fj != null)
                {
                    // Отсоединяем прикреплённый к инструменту объект
                    Destroy(fj);
                }
            }
        }

        // Рука, в которой находится инструмент
        OVRGrabber _currentHand = GetComponent<OVRGrabbable>().grabbedBy;

        // Если инструмент не в руке
        if (_currentHand == null)
        {
            if (fj != null)
            {
                // Отсоединяем прикреплённый к инструменту объект
                Destroy(fj);
            }
        }
    }

    /// <summary>
    /// Толчение пыльника.
    /// </summary>
    protected void CrushAnther(Collider _object, GameObject _pollenPref)
    {
        // Уничтожение пыльника
        if (_object.gameObject != null)
        {
            Destroy(_object.gameObject);
        }

        // Создание пыльцы
        Instantiate(_pollenPref, _object.transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Проверка руки, в которой находится инструмент.
    /// </summary>
    protected bool CheckHand(string _handName)
    {
        // Рука, в которой находится инструмент
        OVRGrabber _currentHand = GetComponent<OVRGrabbable>().grabbedBy;

        if (_currentHand != null)
        {
            // Если инструмент в соответствующей руке
            if (_currentHand.tag == _handName)
            {
                return true;
            }
        }

        return false;
    }
}