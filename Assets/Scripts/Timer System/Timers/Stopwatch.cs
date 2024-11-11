using System;

public class Stopwatch : ITimer
{
    public event Action Started;
    public event Action<float> Ticked;
    public event Action Finished;

    private float time;
    private bool isLooped;

    public bool IsPlaying { get; private set; }
    public TimerUpdateType UpdateType { get; private set; }

    public Stopwatch(
        TimerUpdateType updateType = TimerUpdateType.Update,
        bool loop = false,
        bool play = false)
    {
        isLooped = loop;
        UpdateType = updateType;

        if (play)
            Play();

        TimerSystem.Instance.Register(this, UpdateType);
    }

    ~Stopwatch()
    {
        TimerSystem.Instance.Remove(this, UpdateType);
    }

    public void Tick(float deltaTime)
    {
        if (!IsPlaying)
            return;

        time += deltaTime;
        Ticked?.Invoke(time);
    }

    public void Play(bool reset = false)
    {
        if (IsPlaying)
            return;

        if (reset)
            time = 0f;

        IsPlaying = true;
        Started?.Invoke();
    }

    public void Stop()
    {
        IsPlaying = false;
    }

    public void Finish()
    {
        if (isLooped)
        {
            time = 0f;
            Finished?.Invoke();
            Started?.Invoke();
            return;
        }

        IsPlaying = false;
        Finished?.Invoke();
    }

    public void Reset()
    {
        IsPlaying = false;
        time = 0f;
        Started = null;
        Ticked = null;
        Finished = null;
    }

    public void OnFinished(Action callback)
    {
        Finished = callback;
    }
}