using UnityEngine;

public class DayCycleManager : MonoBehaviour, ITimeListener
{
    [SerializeField] private GameObject dayObject;
    [SerializeField] private GameObject afterObject;
    [SerializeField] private GameObject nightObject;

    [Header("Skybox Materials")]
    [SerializeField] private Material daySkybox;
    [SerializeField] private Material afterSkybox;
    [SerializeField] private Material nightSkybox;

    public void Register(TimeManager timeManager)
    {
        timeManager.OnHourChanged += UpdateDayPart;
    }

    public void Unregister(TimeManager timeManager)
    {
        timeManager.OnHourChanged -= UpdateDayPart;
    }

    public void ForceSetToMorning()
    {
        dayObject.SetActive(true);
        afterObject.SetActive(false);
        nightObject.SetActive(false);
        RenderSettings.skybox = daySkybox;
        DynamicGI.UpdateEnvironment(); // 반영 즉시 적용
    }

    private void UpdateDayPart(int hour)
    {
        if (hour >= 8 && hour < 12)
        {
            dayObject.SetActive(true);
            afterObject.SetActive(false);
            nightObject.SetActive(false);
            RenderSettings.skybox = daySkybox;
        }
        else if (hour >= 12 && hour < 16)
        {
            dayObject.SetActive(false);
            afterObject.SetActive(true);
            nightObject.SetActive(false);
            RenderSettings.skybox = afterSkybox;
        }
        else if (hour >= 16 && hour < 20)
        {
            dayObject.SetActive(false);
            afterObject.SetActive(false);
            nightObject.SetActive(true);
            RenderSettings.skybox = nightSkybox;
        }

        DynamicGI.UpdateEnvironment(); // 변경 즉시 반영
    }
}
