using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action OnPlayerJoined;
    public static event Action OnPlayerLeft;
    public static event Action OnGameReadyToStart;
    public static event Action OnGameStarted;
    public static event Action OnGameEnded;

    public static void InvokeOnPlayerJoined()
    {
        OnPlayerJoined?.Invoke();
    }
    public static void InvokeOnPlayerLeft()
    {
        OnPlayerLeft?.Invoke();
    }
    public static void InvokeOnGameReadyToStart()
    {
        OnGameReadyToStart?.Invoke();
    }
    public static void InvokeOnGameStarted()
    {
        OnGameStarted?.Invoke();
    }
    public static void InvokeOnGameEnded()
    {
        OnGameEnded?.Invoke();
    }
}
