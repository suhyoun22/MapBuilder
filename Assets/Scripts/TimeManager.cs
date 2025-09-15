using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 선택 사항: 씬 전환해도 유지하려면
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }

    public int Hour { get; private set; } = 8;
    public int Minute { get; private set; } = 0;

    public float realSecondsPerGameHour = 60f; // 1분 = 1시간
    private float timer = 0f;

    public event Action<int, int> OnMinuteChanged; // 예: 8:10
    public event Action<int> OnHourChanged;
    public event Action OnDayEnded;

    public bool IsDayOver => Hour >= 20;
    public bool IsBusinessHours => Hour >= 8 && Hour < 20;

    private bool isPaused = false;

    void Start()
    {
        // 시작할 때 초기 시간 UI 갱신
        OnMinuteChanged?.Invoke(Hour, Minute);
    }


    void Update()
    {
        if (isPaused || IsDayOver) return;

        timer += Time.deltaTime;

        float secondsPerGameMinute = realSecondsPerGameHour / 60f;

        if (timer >= secondsPerGameMinute)
        {
            timer -= secondsPerGameMinute;
            AdvanceMinute();
        }
    }

    void AdvanceMinute()
    {
        Minute++;

        if (Minute >= 60)
        {
            Minute = 0;
            Hour++;
            OnHourChanged?.Invoke(Hour);
        }

        // 10분 단위 알림 (0,10,20,...,50)
        if (Minute % 15 == 0)
        {
            OnMinuteChanged?.Invoke(Hour, Minute);
        }

        if (Hour >= 20)
        {
            EndDay();
        }
    }


    public void PauseTime() => isPaused = true;
    public void ResumeTime() => isPaused = false;

    public void EndDay()
    {
        Debug.Log("EndDay() called");
        isPaused = true;
        OnDayEnded?.Invoke();
    }

    public void ResetDay()
    {
        Hour = 8;
        Minute = 0;
        timer = 0f;
        isPaused = false;
    }

    public string GetFormattedTime()
    {
        return $"{Hour:D2}:{Minute:D2}";
    }
}
