using System;

public interface ITimer
{
    public event Action<float> Ticked;
    public event Action Finished;

    public bool IsPlaying { get; }

    public void Tick(float deltaTime);
}