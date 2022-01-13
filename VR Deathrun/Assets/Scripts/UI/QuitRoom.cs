using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Photon.Pun.Demo.PunBasics {
    public class QuitRoom : MonoBehaviourPunCallbacks
    {
        private GameManager gameManager;

        public void DisconnectButton() 
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}
