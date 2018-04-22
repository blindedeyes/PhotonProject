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
        GameObject playerPrefab;
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

        private GameObject localPlayer;

        public GameObject localPlayerObject{
            get{ return localPlayer;}
        }

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
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
            //Connect();
            // PhotonView view = GetComponent<PhotonView>();
            // if(view == null){
            //     //view = new PhotonView();
            //     view = gameObject.AddComponent<PhotonView>();
            //     view.
            // }

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Connect()
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
            Debug.Log("Connected to Master.");
            PhotonNetwork.JoinLobby();
            PhotonNetwork.JoinRandomRoom();
        }
        
        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg){
            Debug.Log("Failed to join room.");
            
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom, PublishUserId = true }, TypedLobby.Default);            
        }
        public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
        {
            Debug.Log("Failed to join room.");
            
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom, PublishUserId = true }, TypedLobby.Default);
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby");

        }

        public override void OnCreatedRoom(){
            Debug.Log("OnCreatedRoom");
            
            // localPlayer = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity, 0);                        
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom");
            
            localPlayer = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity, 0);
            
        }

        void LoadArena()
        {
            if (!PhotonNetwork.isMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
                return;
            }
            Debug.Log("PhotonNetwork : Loading Level : " + PhotonNetwork.room.PlayerCount);
            PhotonNetwork.LoadLevel(1);


        }
        public override void OnPhotonPlayerConnected(PhotonPlayer other)
        {
            Debug.Log("OnPhotonPlayerConnected() " + other.NickName); // not seen if you're the player connecting


            // if (PhotonNetwork.isMasterClient)
            // {
            //     Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


            //     LoadArena();
            // }
        }


        public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
        {
            Debug.Log("OnPhotonPlayerDisconnected() " + other.NickName); // seen when other disconnects


            // if (PhotonNetwork.isMasterClient)
            // {
            //     Debug.Log("OnPhotonPlayerDisonnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


            //     LoadArena();
            // }
        }
    }
}