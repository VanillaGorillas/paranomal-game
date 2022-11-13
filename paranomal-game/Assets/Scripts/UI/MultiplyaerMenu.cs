using FishNet;
using UnityEngine;
using UnityEngine.UI;

public sealed class MultiplyaerMenu : MonoBehaviour
{
    [SerializeField]
    private Button hostButton;

    [SerializeField]
    private Button connectButton;

    // DEF: Hosting - Is the process starting a server and connecting a client to it.
    private void Start()
    {
        hostButton.onClick.AddListener(() =>
        {
            InstanceFinder.ServerManager.StartConnection();

            InstanceFinder.ClientManager.StartConnection();
        });

        connectButton.onClick.AddListener(() =>
        {
            InstanceFinder.ClientManager.StartConnection();
        });
    }

}
