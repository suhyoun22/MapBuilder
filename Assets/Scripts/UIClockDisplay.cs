using TMPro;
using UnityEngine;

public class UIClockDisplay : MonoBehaviour, ITimeListener
{
    [SerializeField] private TextMeshProUGUI timeText;

    public void Register(TimeManager timeManager)
    {
        timeManager.OnMinuteChanged += UpdateTimeUI;
    }

    public void Unregister(TimeManager timeManager)
    {
        timeManager.OnMinuteChanged -= UpdateTimeUI;
    }

    private void UpdateTimeUI(int hour, int minute)
    {
        int roundedMinute = (minute / 15) * 15;
        timeText.text = $"{hour:D2}:{roundedMinute:D2}";
    }
}
