public interface IGameListener { }

public interface IGameStartListener : IGameListener
{
    void OnGameStarted();
}

public interface IGameFinishListener : IGameListener
{
    void OnGameFinished();
}

public interface IGamePauseListener : IGameListener
{
    void OnGamePaused();
}

public interface IGameResumeListener : IGameListener
{
    void OnGameResumed();
}