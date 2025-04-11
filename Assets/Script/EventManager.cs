using System;
using UnityEngine;
using ReadyPlayerMe.Samples.AvatarCreatorWizard;

public class EventManager : MonoBehaviour
{
    public static event Action OnPlayerJoined;
    public static event Action OnPlayerLeft;
    public static event Action OnGameReadyToStart;
    public static event Action OnGameStarted;
    public static event Action OnGameEnded;

    void OnEnable()
    {
        GameManager.OnAvatarLoaded += SetAvatarPosition;
    }
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

    void SetAvatarPosition(GameObject Character)
    {
       Character.SetActive(false);
    }
}
