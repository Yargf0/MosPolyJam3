using System;

public class CountdownTimer : ITimer
{
    public event Action<float> Ticked;
    public event Action Finished;

    public bool IsPlaying { get; private set; }

    private float duration;
    private float remainingTime;

    private bool isLooped;

    public CountdownTimer(float value = 0f, bool loop = false, bool play = false)
    {
        duration = value;
        isLooped = loop;

        if (play)
            Play();

        TimerSystem.Instance.Register(this, TimerUpdateType.Update);
    }

    public void Play()
    {
        if (IsPlaying || remainingTime <= 0f)
            return;

        IsPlaying = true;
    }

    public void Play(float time)
    {
        if (IsPlaying || time <= 0f)
            return;

        duration = time;
        remainingTime = duration;
        IsPlaying = true;
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

    private void Reset()
    {
        remainingTime = duration;
    }

    private void Finish()
    {
        if (isLooped)
            Reset();
        else
            IsPlaying = false;

        Finished?.Invoke();
    }
}
