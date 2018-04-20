namespace BlindPUN
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using ExitGames.Client.Photon;
    using UnityEngine;
    public class PUNManager : Photon.PunBehaviour
    {
        [SerializeField]
        string typeName;
        [SerializeField]
        string gameName;
        [SerializeField]
        string gameVersion;
        [SerializeField]
        byte MaxPlayersPerRoom = 4;
        [SerializeField]
        PhotonLogLevel Loglevel = PhotonLogLevel.Informational;
        void Awake()
        {
            PhotonNetwork.logLevel = Loglevel;
            // #Critical
            // we don't join the lobby. There is no need to join a lobby to get the list of rooms.
            PhotonNetwork.autoJoinLobby = false;
            
            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.automaticallySyncScene = true;
        }

        // Use this for initialization
        void Start()
        {
            Connect();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Connect()
        {
            if (!PhotonNetwork.connected)
                PhotonNetwork.ConnectUsingSettings(gameVersion);

        }

        public override void OnConnectedToPhoton()
        {
            //we have connected, now what.
        }
        public override void OnConnectedToMaster()
        {
            //PhotonNetwork.lobby
            //PhotonNetwork.CreateRoom(null, new RoomOptions() { }, LobbyType);
            PhotonNetwork.JoinLobby();
            PhotonNetwork.JoinRandomRoom();
        }
        public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
        {
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom, PublishUserId = true }, TypedLobby.Default);
        }
        public override void OnJoinedLobby()
        {
            
        }
        public override void OnJoinedRoom()
        {
            //stuff and things
            //Load game lobby scene stuff?
            
        }
    }
}