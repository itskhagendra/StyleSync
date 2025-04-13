using Fusion;
using UnityEngine;

public class ActiveChildController : NetworkBehaviour
{
    [Networked]
    [OnChangedRender(nameof(OnActiveChildChanged))]
    public int ActiveChildIndex { get; set; }

    public GameObject[] children; // Assign your 3 child GameObjects here in Inspector

    public override void Spawned()
    {
        SetActiveChild(ActiveChildIndex);
    }

    public void OnActiveChildChanged(NetworkBehaviourBuffer previous)
    {
        var prev = GetPropertyReader<int>(nameof(ActiveChildIndex)).Read(previous);
        Debug.Log($"Active child changed from {prev} to {ActiveChildIndex}");
        SetActiveChild(ActiveChildIndex);
    }

    private void SetActiveChild(int index)
    {
        for (int i = 0; i < children.Length; i++)
        {
            children[i].SetActive(i == index);
        }
    }

    // Call this to switch the active child (can be from UI, input, etc.)
    public void SwitchToChild(int index)
    {
        Debug.Log("Switch To Child Called");
        if (HasStateAuthority && index >= 0 && index < children.Length)
        {
            ActiveChildIndex = index;
        }
    }
}
