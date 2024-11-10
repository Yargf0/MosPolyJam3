using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private List<IGameListener> gameListeners = new();

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

    public void RegisterListener(IGameListener gameListener)
    {
        if (gameListeners.Contains(gameListener))
        {
            Debug.LogWarning($"[{nameof(GameManager)}] Already contatins listener");
            return;
        }

        gameListeners.Add(gameListener);
    }

    public void RemoveListener(IGameListener gameListener)
    {
        if (!gameListeners.Contains(gameListener))
        {
            Debug.LogWarning($"[{nameof(GameManager)}] Does not contain listener");
            return;
        }

        gameListeners.Remove(gameListener);
    }
}