using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public sealed class GameManager : NetworkBehaviour
{
    public static GameManager instance { get; private set; }

    [SyncObject]
    public readonly SyncList<InputManager> players = new ();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Debug.Log(players.Count);
    }
}
