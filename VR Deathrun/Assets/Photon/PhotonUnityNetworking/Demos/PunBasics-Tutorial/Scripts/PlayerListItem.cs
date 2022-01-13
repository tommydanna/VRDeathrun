using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.Pun.Demo.PunBasics 
{ 
	public class PlayerListItem : MonoBehaviourPunCallbacks
	{
		[SerializeField] Text text;
		Player player;

		public void SetUp(Player _player)
		{
			player = _player;
			text.text = _player.NickName;
		}

		public override void OnPlayerLeftRoom(Player otherPlayer)
		{
			if (player == otherPlayer)
			{
				Destroy(gameObject);
			}
		}

		public override void OnLeftRoom()
		{
			Destroy(gameObject);
		}
	}

}