using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon;
using TMPro;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text createRoom;
    [SerializeField] private TMP_Text joinRoom;

    [SerializeField] private int maxPlayers=6;
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoom.text);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoom.text);
    }

    public override void  OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Prototype 4");
    }
}
