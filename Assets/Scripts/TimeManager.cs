using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    public event Action<int, int> OnMinuteChanged;
    public event Action<int> OnHourChanged;
    public event Action OnDayEnded;

    public int Hour { get; private set; }
    public int Minute { get; private set; }

    [SerializeField] private int startHour = 8;
    [SerializeField] private int endHour = 20;
    [SerializeField] private int realSecondsPerDay = 60;

    private float timeAccumulator;
    private bool isPaused;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Hour = startHour;
        Minute = 0;
    }

    void Update()
    {
        if (isPaused) return;

        timeAccumulator += Time.deltaTime;
        float secondsPerMinute = realSecondsPerDay / ((endHour - startHour) * 60f);

        if (timeAccumulator >= secondsPerMinute)
        {
            timeAccumulator -= secondsPerMinute;
            AdvanceMinute();
        }
    }

    private void AdvanceMinute()
    {
        Minute++;
        if (Minute >= 60)
        {
            Minute = 0;
            Hour++;

            OnHourChanged?.Invoke(Hour);

            if (Hour >= endHour)
            {
                OnDayEnded?.Invoke();
            }
        }

        OnMinuteChanged?.Invoke(Hour, Minute);
    }

    public void PauseTime() => isPaused = true;
    public void ResumeTime() => isPaused = false;

    public void ResetDay()
    {
        Hour = startHour;
        Minute = 0;
        timeAccumulator = 0;
    }

    public void EndDay()
    {
        Hour = endHour;
        Minute = 0;
        OnDayEnded?.Invoke();
    }
}
