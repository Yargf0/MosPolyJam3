using System;

public interface ITimer
{
    public event Action Finished;

    public bool IsPlaying { get; }
    public TimerUpdateType UpdateType { get; }

    public void Tick(float deltaTime);
}