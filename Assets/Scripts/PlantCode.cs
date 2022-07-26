using UnityEngine;

public class PlantCode : MonoBehaviour
{
    private string code;
    private string[] correctCodes;

    public string Code
    {
        set
        { 
            if (IsCorrectCode(value))
            {
                code = value;
            }

            else
            {
                code = "01";
            }
        }
        get { return code; }
    }

    private void Awake()
    {
        correctCodes = new string[] {
        "01",
        "10",
        "0110",
        "010110",
        "100110",
        "10010110",
        "01010110",
        "0110010110",
        "010110100110",
        "0110100110",
        "10100110",
        "01100110"
        };
    }

    public bool IsCorrectCode(string _plantCode)
    {
        foreach (string code in correctCodes)
        {
            if (_plantCode == code)
            {
                return true;
            }
        }

        return false;
    }

    public void RandomCode()
    {
        int _ind = new System.Random().Next(2, 12);
        code = correctCodes[_ind];
    }
}
