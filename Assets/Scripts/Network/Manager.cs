using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

namespace Multiverse.Network
{
	public class Manager : MonoBehaviour {
		public static Manager instance = null;

		private Connection connection;
		private string serverMsg;
		public string msgToServer;

		void Awake() {
			if (instance == null) {
				connection = gameObject.AddComponent<Connection> ();
				instance = this;
			} else {
				Destroy (gameObject);
			}
		}
			
		void Update () {
			SocketResponse ();
		}
			
		void OnGUI() {
			if (connection.socketReady == false) {
				if (GUILayout.Button ("Connect")) {
					Debug.Log("Attempting to connect..");
					connection.setupSocket();
				}
			}

			if (connection.socketReady == true) {
				msgToServer = GUILayout.TextField(msgToServer);

				if (GUILayout.Button ("Write to server", GUILayout.Height(30))) {
					SendToServer(msgToServer);
				}
			}
		}
			
		public void SendToServer(string str) {
			if (connection.socketReady) {
				connection.writeSocket (str);
				Debug.Log ("[CLIENT] -> " + str);
			}
		}

		private void SocketResponse() {
			if (connection.socketReady) {
				string serverSays = connection.readSocket();
				if (serverSays != "") {
					Debug.Log("[SERVER]" + serverSays);
				}
			}
		}
	}
}