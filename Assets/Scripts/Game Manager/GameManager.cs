using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private List<IGameListener> gameListeners = new();

    //private readonly List<IUpdateGameListener> updateGameListeners = new();
    //private readonly List<IFixedUpdateGameListener> fixedUpdateGameListeners = new();
    //private readonly List<ILateUpdateGameListener> lateUpdateGameListeners = new();

    //public void InitGame()
    //{
    //    foreach (var listener in gameListeners)
    //    {
    //        if (listener is IGameInitListener initListener)
    //        {
    //            initListener.OnGameInitialized();
    //        }
    //    }
    //}

    public void StartGame()
    {
        foreach (var listener in gameListeners)
        {
            if (listener is IGameStartListener startListener)
            {
                startListener.OnGameStarted();
            }
        }
    }

    public void FinishGame()
    {
        foreach (var listener in gameListeners)
        {
            if (listener is IGameFinishListener finishListener)
            {
                finishListener.OnGameFinished();
            }
        }
    }

    public void PauseGame()
    {
        foreach (var listener in gameListeners)
        {
            if (listener is IGamePauseListener pauseListener)
            {
                pauseListener.OnGamePaused();
            }
        }
    }

    public void ResumeGame()
    {
        foreach (var listener in gameListeners)
        {
            if (listener is IGameResumeListener resumeListener)
            {
                resumeListener.OnGameResumed();
            }
        }
    }

    //public void OnUpdate(float deltaTime)
    //{
    //    for (int i = 0; i < _updateGameListeners.Count; i++)
    //    {
    //        _updateGameListeners[i].OnUpdate(deltaTime);
    //    }
    //}

    //public void OnFixedUpdate(float fixedDeltaTime)
    //{
    //    for (int i = 0; i < _fixedUpdateGameListeners.Count; i++)
    //    {
    //        _fixedUpdateGameListeners[i].OnFixedUpdate(fixedDeltaTime);
    //    }
    //}

    //public void OnLateUpdate(float deltaTime)
    //{
    //    for (int i = 0; i < _fixedUpdateGameListeners.Count; i++)
    //    {
    //        _lateUpdateGameListeners[i].OnLateUpdate(deltaTime);
    //    }
    //}

    public void RegisterListener(IGameListener gameListener)
    {
        if (gameListeners.Contains(gameListener))
        {
            Debug.LogWarning($"[{nameof(GameManager)}] Already contatins listener");
            return;
        }

        gameListeners.Add(gameListener);

        //if (gameListener is IUpdateGameListener updateGameListener)
        //{
        //    updateGameListeners.Add(updateGameListener);
        //}

        //if (gameListener is IFixedUpdateGameListener fixedUpdateGameListener)
        //{
        //    fixedUpdateGameListeners.Add(fixedUpdateGameListener);
        //}

        //if (gameListener is ILateUpdateGameListener lateUpdateGameListener)
        //{
        //    lateUpdateGameListeners.Add(lateUpdateGameListener);
        //}
    }

    public void RemoveListener(IGameListener gameListener)
    {
        if (!gameListeners.Contains(gameListener))
        {
            Debug.LogWarning($"[{nameof(GameManager)}] Does not contain listener");
            return;
        }

        gameListeners.Remove(gameListener);

        //if (gameListener is IUpdateGameListener updateGameListener)
        //{
        //    updateGameListeners.Remove(updateGameListener);
        //}

        //if (gameListener is IFixedUpdateGameListener fixedUpdateGameListener)
        //{
        //    fixedUpdateGameListeners.Remove(fixedUpdateGameListener);
        //}

        //if (gameListener is ILateUpdateGameListener lateUpdateGameListener)
        //{
        //    lateUpdateGameListeners.Remove(lateUpdateGameListener);
        //}
    }
}