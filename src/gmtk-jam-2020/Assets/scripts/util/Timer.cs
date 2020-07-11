using System;

public class Timer
{
    public enum TimerState
    {
        NotStarted,
        Started,
        Done
    }

    public event EventHandler Elapsed;
    public event EventHandler Started;

    public Action ElapsedCallback { get; set; } = () => { };
    public bool OneTimeCallback { get; set; } = true;

    public float SetTime { get; private set; } = 1;
    public float RemainingTime { get; protected set; } = 0;

    public TimerState CurrentState { get; private set; } = TimerState.NotStarted;

    public Timer(float timerTime)
    {
        SetTime = timerTime;
    }

    /// <summary>
    /// Starts a timer if set time is 0 it stops immediately 
    /// </summary>
    /// <param name="offset"></param>
    public virtual void Start(float offset = 0)
    {
        if (offset > SetTime)
            throw new ArgumentException($"offset {offset} cannot be greater than SetTime {SetTime}");
        CurrentState = TimerState.Started;
        RemainingTime = SetTime - offset;
        OnStarted();
        if (RemainingTime <= 0)
        {
            OnStartedWithZeroTime();
        }
    }
    protected virtual void OnStartedWithZeroTime()
    {
        Stop();
    }

    public virtual void Update(float deltaTime)
    {
        if (CurrentState == TimerState.Started)
        {
            if (RemainingTime > 0)
            {
                RemainingTime -= deltaTime;
            }
            if (RemainingTime <= 0)
            {
                Stop();
            }
        }
    }

    public void RecalibrateTimer(float newTimerTime)
    {
        if (CurrentState == TimerState.Started)
            throw new InvalidOperationException("Ongoing timers can't be changed!");

        SetTime = newTimerTime;
    }

    protected void OnElapsed()
    {
        Elapsed?.Invoke(this, EventArgs.Empty);
        ElapsedCallback?.Invoke();
        if (OneTimeCallback)
            ElapsedCallback = () => { };
    }
    protected void OnStarted()
    {
        Started?.Invoke(this, EventArgs.Empty);
    }

    public void Stop()
    {
        if (CurrentState == TimerState.Started)
        {
            CurrentState = TimerState.Done;
            OnElapsed();
        }
    }

    public void Interrupt()
    {
        if (CurrentState == TimerState.Started)
        {
            CurrentState = TimerState.Done;
            if (OneTimeCallback)
                ElapsedCallback = () => { };
        }
    }
}
