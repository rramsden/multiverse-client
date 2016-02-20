using UnityEngine;
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
                Debug.Log("Attempting to connect..");
                socket.Connect ();
            }

            if (GUILayout.Button ("Send")) {
                var data = new HANDSHAKE().Stream;
                socket.Send (data);
            }
        }
    }
}
