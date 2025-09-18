using UnityEngine;
using UnityEngine.UI;

public class EndOfDayUI : MonoBehaviour, ITimeListener
{
    [SerializeField] private GameObject endOfDayPanel;
    [SerializeField] private Button nextDayButton;

    private TimeManager _timeManager;

    public void Register(TimeManager timeManager)
    {
        _timeManager = timeManager;

        timeManager.OnDayEnded += ShowEndOfDayPanel;
        nextDayButton.onClick.AddListener(() =>
        {
            _timeManager.ResetDay();
            _timeManager.ResumeTime();
            endOfDayPanel.SetActive(false);

            // 🌞 DayCycleManager 상태 초기화
            var cycleManager = FindObjectOfType<DayCycleManager>();
            if (cycleManager != null)
            {
                cycleManager.ForceSetToMorning();
            }
        });
    }

    public void Unregister(TimeManager timeManager)
    {
        timeManager.OnDayEnded -= ShowEndOfDayPanel;
        nextDayButton.onClick.RemoveAllListeners();
    }

    private void ShowEndOfDayPanel()
    {
        endOfDayPanel.SetActive(true);
        _timeManager.PauseTime();
    }
}
