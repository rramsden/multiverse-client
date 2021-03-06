﻿using UnityEngine;
using Multiverse.Network.Packets;

namespace Multiverse.Network
{
    public class Manager : MonoBehaviour {
        public static Manager instance = null;
        private SocketClient socket;

        void Awake() {
            if (instance == null) {
                instance = this;
            }

            DontDestroyOnLoad(gameObject); // don't destroy on scene change
            socket = new SocketClient ("127.0.0.1", 4444);
        }

        void OnGUI() {
            if (GUILayout.Button ("Connect")) {
                Debug.Log("Attempting to connect..");
                socket.Connect ();
            }

            if (GUILayout.Button ("Send")) {
                var data = new HANDSHAKE().Stream;
                socket.Send (data);
            }
        }

        public void Send(Packet packet) {
            socket.Send(packet.Stream);
        }
    }
}
