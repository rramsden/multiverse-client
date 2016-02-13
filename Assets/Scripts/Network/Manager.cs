using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System;

namespace Multiverse.Network
{
	public class Manager : MonoBehaviour {
		private SocketServer socket;

		void Start() {
			socket = new SocketServer (IPAddress.Parse("127.0.0.1"), 4444);
		}
			
		void OnGUI() {
			if (GUILayout.Button ("Connect")) {
				Debug.Log("Attempting to connect..");
				socket.Connect ();
			}
		}
	}
}