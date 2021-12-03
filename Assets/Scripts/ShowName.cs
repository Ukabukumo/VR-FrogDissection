using UnityEngine;
using TMPro;

public class ShowName : MonoBehaviour
{
    private void Awake()
    {
        string _objectName = name;
        GameObject _obj = new GameObject();
        _obj.AddComponent<TextMeshPro>().text = name;
        //Instantiate(_obj, transform);
    }
}
