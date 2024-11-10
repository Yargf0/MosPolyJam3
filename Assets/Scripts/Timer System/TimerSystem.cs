using System.Collections.Generic;
using UnityEngine;

public class TimerSystem : Singleton<TimerSystem>
{
    private List<ITimer> updateTimers = new();
    private List<ITimer> lateUpdateTimers = new();
    private List<ITimer> fixedUpdateTimers = new();

    private void Update()
    {
        Tick(updateTimers, Time.deltaTime);
    }

    private void LateUpdate()
    {
        Tick(lateUpdateTimers, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Tick(fixedUpdateTimers, Time.fixedDeltaTime);
    }

    public bool Register(ITimer timer, TimerUpdateType updateType)
    {
        var timersList = GetTimersList(updateType);

        if (timersList == null)
        {
            Debug.LogError($"[{nameof(TimerSystem)}] Timer type not found");
            return false;
        }

        foreach (var registeredTimer in timersList)
        {
            if (registeredTimer == timer)
            {
                Debug.LogError($"[{nameof(TimerSystem)}] Timer already registered");
                return false;
            }
        }

        timersList.Add(timer);

        return true;
    }

    public bool Remove(ITimer timer, TimerUpdateType updateType)
    {
        var timersList = GetTimersList(updateType);

        if (timersList == null)
        {
            Debug.LogError($"[{nameof(TimerSystem)}] Timer type not found");
            return false;
        }

        for (int i = 0; i < timersList.Count; i++)
        {
            if (timersList[i] == timer)
            {
                timersList.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    private void Tick(List<ITimer> timers, float deltaTime)
    {
        foreach (var timer in timers)
            timer.Tick(deltaTime);
    }

    private List<ITimer> GetTimersList(TimerUpdateType updateType)
    {
        return updateType switch
        {
            TimerUpdateType.Update => updateTimers,
            TimerUpdateType.LateUpdate => lateUpdateTimers,
            TimerUpdateType.FixedUpdate => fixedUpdateTimers,
            _ => null,
        };
    }
}