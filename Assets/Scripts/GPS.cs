using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour
{
    [SerializeField] string latitude;
    [SerializeField] string longitude;
    [SerializeField] string altitude;
    [SerializeField] string horizontalAccuracy;
    [SerializeField] string timestamp;
    Coroutine ActivatedGPSCoroutine;
    void Update()
    {
        if(Input.location.status != LocationServiceStatus.Running)
            return;
        latitude = "xxx." + Input.location.lastData.latitude.ToString("F8").Split('.')[1];
        longitude = "xxx." + Input.location.lastData.longitude.ToString("F8").Split('.')[1];
        altitude = "xxx." + Input.location.lastData.altitude.ToString("F8").Split('.')[1];
        horizontalAccuracy = Input.location.lastData.horizontalAccuracy.ToString();
        timestamp = Input.location.lastData.timestamp.ToString();
        this.transform.rotation = Quaternion.Euler(0,-Input.compass.trueHeading,0);
    }
    private void OnEnable()
    {
        if(ActivatedGPSCoroutine == null)
            ActivatedGPSCoroutine = StartCoroutine(ActivateGPS());
    }
    private void OnDisable()
    {
        StopCoroutine(ActivatedGPSCoroutine);
        if(Input.location.status == LocationServiceStatus.Running)
            Input.location.Stop();
    }
    IEnumerator ActivateGPS()
    {
#if UNITY_EDITOR
        Debug.Log("Unity Remote Connecting");
        while(UnityEditor.EditorApplication.isRemoteConnected == false)
            yield return new WaitForSecondsRealtime(1);
#endif
        Debug.Log("Unity Remote Connected");
        if(Input.location.isEnabledByUser == false)
        {
            Debug.Log("Location service is not enabled by user");
            yield break;
        }
        Debug.Log("Start Location Services");
        Input.location.Start();
        int maxWait = 15;
        while((Input.location.status == LocationServiceStatus.Stopped
            || Input.location.status == LocationServiceStatus.Initializing)
            && maxWait > 0)
        {
            Debug.Log("Location Service Status Check: " + Input.location.status);
            yield return new WaitForSecondsRealtime(1);
            maxWait -= 1;
        }
        if(maxWait < 1)
        {
            Debug.Log("Location Service Time Out");
            yield break;
        }
        if(Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Location Service Failed to Start");
            yield break;
        }
        Input.compass.enabled = true;
    }
}
