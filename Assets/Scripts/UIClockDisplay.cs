using System.Collections;
using TMPro;
using UnityEngine;

public class UIClockDisplay : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    void OnEnable()
    {
        StartCoroutine(RegisterToTimeManager());
    }

    IEnumerator RegisterToTimeManager()
    {
        // TimeManager.Instance가 null이 아닐 때까지 대기
        while (TimeManager.Instance == null)
            yield return null;

        TimeManager.Instance.OnMinuteChanged += UpdateTimeUI;
    }

    void OnDisable()
    {
        TimeManager.Instance.OnMinuteChanged -= UpdateTimeUI;
    }

    void UpdateTimeUI(int hour, int minute)
    {
        timeText.text = $"{hour:D2}:{minute:D2}";
        // 또는 별도 애니메이션/효과 연출도 가능
    }
}
