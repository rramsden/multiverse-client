using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System;
using Multiverse.Network.Packets;

namespace Multiverse.Network
{
	public class Manager : MonoBehaviour {
		private SocketClient socket;

		void Start() {
			socket = new SocketClient ("127.0.0.1", 4444);
		}

		void OnGUI() {
			if (GUILayout.Button ("Connect")) {
				Debug.Log("Att:empting to connect..");
				socket.Connect ();
			}

			if (GUILayout.Button ("Send")) {
        byte[] data = new CMSG_HANDSHAKE().Stream;
				socket.Send (data);
			}
		}
	}
}
