using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCameraScreenSpace : MonoBehaviour
{
    [SerializeField] RawImage background;
    Coroutine cameraStarter;
    WebCamTexture backCamera;
    private void OnEnable()
    {
        if(cameraStarter == null)
            cameraStarter = StartCoroutine(StartCamera());
    }
    private void OnDisable()
    {
        if(backCamera != null && backCamera.isPlaying)
            backCamera.Stop();
        if(cameraStarter != null)
            StopCoroutine(cameraStarter);
    }
    void Update()
    {
        if(cameraStarter == null && (backCamera == null || backCamera.isPlaying == false))
        {
            StartCoroutine(StartCamera());
            return;
        }
    }
    IEnumerator StartCamera()
    {
#if UNITY_EDITOR
        Debug.Log("Connecting Unity Remote");
        while(UnityEditor.EditorApplication.isRemoteConnected == false)
            yield return new WaitForEndOfFrame();
#endif
        Debug.Log("Unity Remote is Connected");
        WebCamDevice[] devices = WebCamTexture.devices;
        foreach(var device in devices)
        {
            if(device.isFrontFacing == false)
            {
                backCamera = new WebCamTexture(device.name, 640, 360);
            }
        }
        if(backCamera == null)
        {
            Debug.Log("Back camera not found");
            yield break;
        }
        Debug.Log("Back Camera Found: " + backCamera.name);
        Debug.Log("Start Camera");
        background.texture = backCamera;
        backCamera.Play();
        while(backCamera.isPlaying == false)
            yield return new WaitForEndOfFrame();
        Debug.Log("Camera Started");
        while(backCamera.width < 100)
            yield return new WaitForEndOfFrame();
        Debug.Log("Camera Ready");
        int flipY = backCamera.videoVerticallyMirrored ? -1 : 1;
        background.transform.localScale = new Vector3(1, flipY, 1);
        int orient = -backCamera.videoRotationAngle;
        background.transform.rotation = Quaternion.Euler(0, 0, orient);
        var scale = Screen.height/backCamera.width;
        background.rectTransform.sizeDelta = new Vector2(
            backCamera.width*scale,
            backCamera.height*scale
        );
    }
}
