// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Launcher.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking Demos
// </copyright>
// <summary>
//  Used in "PUN Basic tutorial" to handle typical game management requirements
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Realtime;
using System.Collections;

namespace Photon.Pun.Demo.PunBasics
{
	#pragma warning disable 649

	/// <summary>
	/// Game manager.
	/// Connects and watch Photon Status, Instantiate Player
	/// Deals with quiting the room and the game
	/// Deals with level loading (outside the in room synchronization)
	/// </summary>
	public class GameManager : MonoBehaviourPunCallbacks
    {

		#region Public Fields

		static public GameManager Instance;

		public int readyPlayers = 0;

		PhotonView pView;

		#endregion

		#region Private Fields

		private GameObject instance;

        [Tooltip("The prefab to use for representing the player")]
        [SerializeField]
        private GameObject playerPrefab;

		bool isReady;

        #endregion

        #region MonoBehaviour CallBacks

        void Start()
		{
			Instance = this;

			PhotonNetwork.AutomaticallySyncScene = true;

			if (!PhotonNetwork.IsConnected)
			{
				SceneManager.LoadScene("HomeRoom");

				return;
			}

			if (playerPrefab == null) { // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.

				Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
			} else {


				if (PlayerManager.LocalPlayerInstance==null)
				{
				    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);

					// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
					PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
										
				}
				else
				{

					Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
				}


			}

		}

        #endregion

        #region Photon Callbacks

        /// <summary>
        /// Called when a Photon Player got connected. We need to then load a bigger scene.
        /// </summary>
        /// <param name="other">Other.</param>
        public override void OnPlayerEnteredRoom( Player other  )
		{
			Debug.Log( "OnPlayerEnteredRoom() " + other.NickName); // not seen if you're the player connecting

			if ( PhotonNetwork.IsMasterClient )
			{
				Debug.LogFormat( "OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient ); // called before OnPlayerLeftRoom
			}
		}

		/// <summary>
		/// Called when a Photon Player got disconnected. We need to load a smaller scene.
		/// </summary>
		/// <param name="other">Other.</param>
		public override void OnPlayerLeftRoom( Player other  )
		{
			Debug.Log( "OnPlayerLeftRoom() " + other.NickName ); // seen when other disconnects

			if ( PhotonNetwork.IsMasterClient )
			{
				Debug.LogFormat( "OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient ); // called before OnPlayerLeftRoom
			}
		}

		public override void OnLeftRoom()
		{
			SceneManager.LoadScene("HomeLauncher");
		}

		#endregion

		#region Public Methods

		public void LeaveRoom()
		{
            if (SceneManager.GetSceneByName("MultiplayerLobbyRoom").isLoaded && isReady)
            {
				SendUnreadyRPC();
            }

			PhotonNetwork.LeaveRoom();
		}

		public void QuitApplication()
		{
			if (SceneManager.GetSceneByName("MultiplayerLobbyRoom").isLoaded && isReady)
			{
				SendUnreadyRPC();
			}

			Application.Quit();
		}

		IEnumerator WaitForRPCsToLoadMaps()
        {
			yield return new WaitForSeconds(.5f);

			if (SceneManager.GetSceneByName("MultiplayerLobbyRoom").isLoaded)
			{
				if (PhotonNetwork.CurrentRoom.PlayerCount == readyPlayers) /*all players in lobby are ready*/
				{

					//Chose random map from list depending on starting number of players in lobby
					if (PhotonNetwork.CurrentRoom.PlayerCount >= 8)
					{
						ChoseMapFromList1();
					}
					else if (PhotonNetwork.CurrentRoom.PlayerCount >= 6)
					{
						ChoseMapFromList2();
					}
					else if (PhotonNetwork.CurrentRoom.PlayerCount >= 4)
					{
						ChoseMapFromList3();
					}
					else
					{
						//load showdown/battle royal map
						PhotonNetwork.LoadLevel(2);
					}
				}
			}
			else
			{
				//Chose random map from list depending on starting number of players in lobby
				if (PhotonNetwork.CurrentRoom.PlayerCount >= 8)
				{
					ChoseMapFromList1();
				}
				else if (PhotonNetwork.CurrentRoom.PlayerCount >= 6)
				{
					ChoseMapFromList2();
				}
				else if (PhotonNetwork.CurrentRoom.PlayerCount >= 4)
				{
					ChoseMapFromList3();
				}
				else
				{
					PhotonNetwork.LoadLevel(2);
				}
			}
		}

		public void LoadMaps()
        {
			StartCoroutine(WaitForRPCsToLoadMaps());
		}

		void ChoseMapFromList1()
        {
			//Scene index that want to be chosen from
			int randMap = Random.Range(2, 3);

			//placeholder for testing
			PhotonNetwork.LoadLevel(randMap);
        }

		void ChoseMapFromList2()
		{
			//Scene index that want to be chosen from
			int randMap = Random.Range(4, 5);

			//placeholder for testing
			PhotonNetwork.LoadLevel(randMap);
		}

		void ChoseMapFromList3()
		{
			//Scene index that want to be chosen from
			int randMap = Random.Range(6, 7);

			//placeholder for testing
			PhotonNetwork.LoadLevel(randMap);
		}

		void ChoseMapFromList4()
        {
			//battle royal/showdown maps that are loaded when there are 3 or less players left
        }

		public void SendReadyRPC()
        {
			pView = GameObject.Find("PlayerController").GetComponent<PhotonView>();
			pView.RPC("ReadyPlayer", RpcTarget.AllBufferedViaServer); //get my photonview
			isReady = true;
        }

		public void SendUnreadyRPC()
		{
			pView.RPC("UnreadyPlayer", RpcTarget.AllBufferedViaServer);
			isReady = false;
		}

		#endregion
	}

}