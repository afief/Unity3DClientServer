using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections;


public class clientside : MonoBehaviour {

	private TcpClient client;
	private NetworkStream stream;
	private StreamReader reader;
	private StreamWriter writer;
	
	private String host = "200.200.200.40";
	private int port = 83;

	// Use this for initialization
	void Start () {
		client = (TcpClient) new TcpClient(host, port);
		stream = client.GetStream();
		reader = new StreamReader(stream);
		writer = new StreamWriter(stream);
	}
	void writeSocket(String teks) {
		writer.Write(teks + "\n\r");
		writer.Flush();
	}
	void readSocket() {
		if (stream.CanRead && stream.DataAvailable) {
			Debug.Log(reader.ReadLine());
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate() {
		readSocket();
		writeSocket("kompi-" + Time.deltaTime.ToString());
	}
}
