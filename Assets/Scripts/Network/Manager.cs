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
			socket = new SocketServer ("127.0.0.1", 4444);
		}
			
		void OnGUI() {
			if (GUILayout.Button ("Connect")) {
				Debug.Log("Att:empting to connect..");
				socket.Connect ();
			}

			if (GUILayout.Button ("Send")) {
				byte[] data = Encoding.ASCII.GetBytes("HELLO");
				socket.Send (data);
			}
		}
	}
}