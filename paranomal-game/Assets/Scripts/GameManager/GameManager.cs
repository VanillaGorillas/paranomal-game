using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Linq;
using UnityEngine;

public sealed class GameManager : NetworkBehaviour
{
    public static GameManager instance { get; private set; }

    [SyncObject]
    public readonly SyncList<InputManager> players = new ();

    [SyncVar]
    // All players are ready
    public bool canStart;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!IsServer) return;

        canStart = players.All(player => player.isReady);

        Debug.Log($"Can Start = {canStart}");
    }
}
