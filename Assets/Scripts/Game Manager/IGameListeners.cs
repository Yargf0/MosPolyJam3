public interface IGameListener { }

//public interface IGameInitListener : IGameListener
//{
//    void OnGameInitialized();
//}

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

//public interface IUpdateGameListener : IGameListener
//{
//    void OnUpdate(float deltaTime);
//}

//public interface ILateUpdateGameListener : IGameListener
//{
//    void OnLateUpdate(float lateDeltaTime);
//}

//public interface IFixedUpdateGameListener : IGameListener
//{
//    void OnFixedUpdate(float fixedDeltaTime);
//}