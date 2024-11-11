using System;

public class CountdownTimer : ITimer
{
    public event Action Started;
    public event Action Finished;

    private float duration;
    private float remainingTime;

    private bool isLooped;

    public bool IsPlaying { get; private set; }
    public TimerUpdateType UpdateType { get; private set; }

    public CountdownTimer(
        float value = 0f,
        TimerUpdateType updateType = TimerUpdateType.Update,
        bool loop = false,
        bool play = false)
    {
        duration = value;
        isLooped = loop;
        UpdateType = updateType;

        if (play)
            Play();

        TimerSystem.Instance.Register(this, UpdateType);
    }

    ~CountdownTimer()
    {
        TimerSystem.Instance.Remove(this, UpdateType);
    }

    public void Play()
    {
        if (IsPlaying || remainingTime <= 0f)
            return;

        IsPlaying = true;
        Started?.Invoke();
    }

    public void Play(float time)
    {
        if (IsPlaying || time <= 0f)
            return;

        duration = time;
        remainingTime = duration;
        IsPlaying = true;
        Started?.Invoke();
    }

    public void Stop()
    {
        IsPlaying = false;
    }

    public void Tick(float deltaTime)
    {
        if (!IsPlaying)
            return;

        remainingTime -= deltaTime;

        if (remainingTime <= 0f)
            Finish();
    }

    public void Reset()
    {
        IsPlaying = false;
        remainingTime = duration;
        Finished = null;
    }

    public void OnFinished(Action callback)
    {
        Finished = callback;
    }

    private void Finish()
    {
        if (isLooped)
        {
            remainingTime = duration;
            Started?.Invoke();
        }
        else
            IsPlaying = false;

        Finished?.Invoke();
    }
}
