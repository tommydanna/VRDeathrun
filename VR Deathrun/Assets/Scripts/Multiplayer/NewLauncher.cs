using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;


public class NewLauncher : MonoBehaviourPunCallbacks
{
	public static NewLauncher Instance;

	/*[SerializeField] TMP_InputField roomNameInputField;
	
	[SerializeField] TMP_Text roomNameText;
	
	[SerializeField] Transform playerListContent;
	[SerializeField] GameObject PlayerListItemPrefab;
	[SerializeField] GameObject startGameButton;*/

	[SerializeField] TMP_Text playerName;
	[SerializeField] TMP_Text maxPlayersText;
	[SerializeField] TMP_Text errorText;
	[SerializeField] byte defualtMaxPlayers = 10;
	[SerializeField] Transform roomListContent;
	[SerializeField] GameObject roomListItemPrefab;

	//bool isConnecting;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		Debug.Log("Connecting to Master");
		PhotonNetwork.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster()
	{
		Debug.Log("Connected to Master");
		PhotonNetwork.JoinLobby();
		PhotonNetwork.AutomaticallySyncScene = true;

		//if (isConnecting)
		//{
		//	PhotonNetwork.JoinRandomRoom();
		//}
	}

	public override void OnJoinedLobby()
	{
		//MenuManager.Instance.OpenMenu("title");
		Debug.Log("Joined Lobby");
	}

	public void CreateRoom()
	{
		PhotonNetwork.CreateRoom(playerName.text, new RoomOptions { MaxPlayers = byte.Parse(maxPlayersText.text) });
		MenuManager.Instance.OpenMenu("loading");
	}

	public override void OnJoinedRoom()
	{
		PhotonNetwork.LoadLevel("MultiplayerLobbyRoom");//change scene name to new scene

		/*Player[] players = PhotonNetwork.PlayerList;

		foreach (Transform child in playerListContent)
		{
			Destroy(child.gameObject);
		}

		for (int i = 0; i < players.Count(); i++)
		{
			Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
		}

		startGameButton.SetActive(PhotonNetwork.IsMasterClient);*/
	}

	public void QuickPlay()
	{
		PhotonNetwork.JoinRandomRoom();
		MenuManager.Instance.OpenMenu("loading");

		//isConnecting = true;

		//if (isConnecting)
		//{
		//	PhotonNetwork.JoinRandomRoom();
		//	MenuManager.Instance.OpenMenu("loading");
		//}	
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		PhotonNetwork.CreateRoom(playerName.text, new RoomOptions { MaxPlayers = defualtMaxPlayers });
	}

	/*
	public override void OnMasterClientSwitched(Player newMasterClient)
	{
		startGameButton.SetActive(PhotonNetwork.IsMasterClient);
	}*/

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		errorText.text = "Room Creation Failed: " + message;
		Debug.LogError("Room Creation Failed: " + message);
		MenuManager.Instance.OpenMenu("error");
	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
		MenuManager.Instance.OpenMenu("loading");
	}

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		//foreach (Transform trans in roomListContent)
		//{
		//	Destroy(trans.gameObject);
		//}

		for (int i = 0; i < roomList.Count; i++)
		{
			if (roomList[i].RemovedFromList)
				continue;
			Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
		}
	}

	public void JoinRoom(RoomInfo info)
	{
		PhotonNetwork.JoinRoom(info.Name);
		MenuManager.Instance.OpenMenu("loading");
	}

	/*
	public void StartGame()
	{
		PhotonNetwork.LoadLevel(1);
	}

	

	

	public override void OnLeftRoom()
	{
		MenuManager.Instance.OpenMenu("title");
	}

	

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
	}*/
}