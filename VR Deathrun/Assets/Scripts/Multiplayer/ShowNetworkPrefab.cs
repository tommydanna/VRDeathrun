using UnityEngine;
using Photon.Pun;

public class ShowNetworkPrefab : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject localPlayerPrefab;
    [SerializeField]
    private GameObject networkPlayerPrefab;

    void Start()
    {
        if (photonView.IsMine)
        {
            Destroy(networkPlayerPrefab);
        }
        else 
        {
            Destroy(localPlayerPrefab);
        }
    }
}
