using UnityEngine;
using System.Collections;
using System.IO;
using System.Net.Sockets;

public class SimpleSocket : MonoBehaviour {

	private TcpClient m_Client;
	private StreamWriter m_Writer;
	private StreamReader m_Reader;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Connect() {
		m_Client = new TcpClient ("127.0.0.1", 4444);
		m_Writer = new StreamWriter(m_Client.GetStream ());
	}

	public void Send() {
		m_Writer.WriteLine ("HELLO");
		m_Writer.Flush ();
	}
}
