using System.Collections;
using UnityEngine;
using TMPro;

public class Quest : MonoBehaviour
{
    [SerializeField] private GameObject time;
    [SerializeField] private GameObject result;
    [SerializeField] private GameObject plantStand;
    [SerializeField] private GameObject petalIcon;
    [SerializeField] private Material correctMat;
    [SerializeField] private Material wrongMat;

    [SerializeField] private float QUEST_TIME = 80f;

    private void Start()
    {
        plantStand.GetComponent<PlantCode>().RandomCode();
        petalIcon.GetComponent<Renderer>().material =
            petalIcon.GetComponent<PlantMaterials>().GetPetalMat(plantStand.GetComponent<PlantCode>().Code);

        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        float _timeLeft = QUEST_TIME;

        while (_timeLeft >= 0)
        {
            time.GetComponent<TextMeshPro>().text = "Оставшееся время " + string.Format("{0:D2}:{1:D2}", 
                (int)_timeLeft / 60, (int)_timeLeft % 60);

            _timeLeft -= Time.deltaTime;

            yield return null;
        }

        time.GetComponent<TextMeshPro>().text = "Время вышло!";

        CheckResult();
    }

    private void CheckResult()
    {
        result.SetActive(true);

        if (plantStand.GetComponent<CheckPlant>().CorrectPlant)
        {
            result.GetComponent<TextMeshPro>().text = "Верно!";
            plantStand.GetComponent<Renderer>().material = correctMat;
        }

        else
        {
            result.GetComponent<TextMeshPro>().text = "Не удалось...";
            plantStand.GetComponent<Renderer>().material = wrongMat;
        }
    }
}
