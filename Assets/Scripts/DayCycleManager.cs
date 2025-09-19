using UnityEngine;

public class DayCycleManager : MonoBehaviour, ITimeListener
{
    [SerializeField] private GameObject dayObject;
    [SerializeField] private GameObject dayObject_1;
    [SerializeField] private GameObject dayObject_2;
    [SerializeField] private GameObject afterObject;
    [SerializeField] private GameObject afterObject_1;
    [SerializeField] private GameObject afterObject_2;
    [SerializeField] private GameObject nightObject;

    [Header("Skybox Materials")]
    [SerializeField] private Material daySkybox;
    [SerializeField] private Material daySkybox_1;
    [SerializeField] private Material daySkybox_2;
    [SerializeField] private Material afterSkybox;
    [SerializeField] private Material afterSkybox_1;
    [SerializeField] private Material afterSkybox_2;
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
        if (hour >= 8 && hour < 10)
        {
            dayObject.SetActive(true);
            dayObject_1.SetActive(false);
            dayObject_2.SetActive(false);
            afterObject.SetActive(false);
            afterObject_1.SetActive(false);
            afterObject_2.SetActive(false);
            nightObject.SetActive(false);
            RenderSettings.skybox = daySkybox;
        }
        else if (hour >= 10 && hour < 12)
        {
            dayObject.SetActive(false);
            dayObject_1.SetActive(true);
            dayObject_2.SetActive(false);
            afterObject.SetActive(false);
            afterObject_1.SetActive(false);
            afterObject_2.SetActive(false);
            nightObject.SetActive(false);
            RenderSettings.skybox = daySkybox_1;
        }
        else if (hour >= 12&& hour < 14)
        {
            dayObject.SetActive(false);
            dayObject_1.SetActive(false);
            dayObject_2.SetActive(true);
            afterObject.SetActive(false);
            afterObject_1.SetActive(false);
            afterObject_2.SetActive(false);
            nightObject.SetActive(false);
            RenderSettings.skybox = daySkybox_2;
        }
        else if (hour >= 14 && hour < 16)
        {
            dayObject.SetActive(false);
            dayObject_1.SetActive(false);
            dayObject_2.SetActive(false);
            afterObject.SetActive(true);
            afterObject_1.SetActive(false);
            afterObject_2.SetActive(false);
            nightObject.SetActive(false);
            RenderSettings.skybox = afterSkybox;
        }
        else if (hour >= 16 && hour < 18)
        {
            dayObject.SetActive(false);
            dayObject_1.SetActive(false);
            dayObject_2.SetActive(false);
            afterObject.SetActive(false);
            afterObject_1.SetActive(true);
            afterObject_2.SetActive(false);
            nightObject.SetActive(false);
            RenderSettings.skybox = afterSkybox_1;
        }
        else if (hour >= 18 && hour < 19)
        {
            dayObject.SetActive(false);
            dayObject_1.SetActive(false);
            dayObject_2.SetActive(false);
            afterObject.SetActive(false);
            afterObject_1.SetActive(false);
            afterObject_2.SetActive(true);
            nightObject.SetActive(false);
            RenderSettings.skybox = afterSkybox_2;
        }
        else if (hour >= 19 && hour < 20)
        {
            dayObject.SetActive(false);
            dayObject_1.SetActive(false);
            dayObject_2.SetActive(false);
            afterObject.SetActive(false);
            afterObject_1.SetActive(false);
            afterObject_2.SetActive(false);
            nightObject.SetActive(true);
            RenderSettings.skybox = nightSkybox;
        }

        DynamicGI.UpdateEnvironment(); // 변경 즉시 반영
    }
}
