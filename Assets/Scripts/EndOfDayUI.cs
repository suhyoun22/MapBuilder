using TMPro;
using UnityEngine;

public class EndOfDayUI : MonoBehaviour
{
    [SerializeField] GameObject panelObject; // DayEndPanel
    [SerializeField] TextMeshProUGUI eventText;

    void Start()
    {
        if (TimeManager.Instance != null)
        {
            Debug.Log("EndOfDayUI: 이벤트 구독 성공");
            TimeManager.Instance.OnDayEnded += ShowDayEndPanel;
        }
        else
        {
            Debug.LogWarning("EndOfDayUI: TimeManager.Instance가 null입니다!");
        }
    }

    void OnDisable()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnDayEnded -= ShowDayEndPanel;
        }
    }

    void ShowDayEndPanel()
    {
        Debug.Log("ShowDayEndPanel 호출됨");
        if (panelObject == null)
        {
            Debug.LogError("panelObject가 null입니다! Inspector에 연결했는지 확인하세요.");
            return;
        }

        panelObject.SetActive(true);
        eventText.text = "Someone caused chaos in the village after drinking a suspicious potion!";
    }

    public void OnNextDayButton()
    {
        panelObject.SetActive(false);
        TimeManager.Instance.ResetDay();
        TimeManager.Instance.ResumeTime();
    }
}
