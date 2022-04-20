using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private float shakeTimer = 0f;
    private float shakeTimerTotal = 0f;
    private float startingIntensity;

    public static CinemachineShake Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        virtualCamera = this.GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intesity, float time)
    {
        CinemachineBasicMultiChannelPerlin cmbmp = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cmbmp.m_AmplitudeGain = intesity;

        startingIntensity = intesity;
        shakeTimerTotal = shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            CinemachineBasicMultiChannelPerlin cmbmp = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cmbmp.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, (1 - (shakeTimer / shakeTimerTotal)));
        }
    }
}
