using UnityEngine;
using System.Collections;

public class TimeRewinder : MonoBehaviour
{
    private float currentTime = 0f;
    private int rewindSpeed = 1;
    private int timeBound = 10;
    private OVRInput.Button rewindButton = OVRInput.Button.One;

    public float CurrentTime
    {
        get { return currentTime; }
    }

    public void TimeRewinderInit(int _timeBound)
    {
        timeBound = _timeBound;

        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        while ( (currentTime * rewindSpeed) < timeBound)
        {
            yield return null;

            if (OVRInput.Get(rewindButton))
            {
                currentTime += Time.deltaTime;
            }
        }

        Destroy(gameObject);
    }
}
