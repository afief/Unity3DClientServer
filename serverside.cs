using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class serverside : MonoBehaviour {
	
	private TcpListener server;
	
	public String host = "200.200.200.40";
	public int port = 83;
	
	private List<NetworkStream> netStreams = new List<NetworkStream>();
	
	private IPAddress ipaddress;

	// Use this for initialization
	void Start () {
		ipaddress = IPAddress.Parse(host);
		
		server = (TcpListener) new TcpListener(ipaddress, port);
		server.Start();
		
		server.BeginAcceptTcpClient(new AsyncCallback(tcpClientMasuk), server);
	}
	void tcpClientMasuk(IAsyncResult ar) {
		TcpListener _tcpListener = (TcpListener) ar.AsyncState;
		TcpClient _client = _tcpListener.EndAcceptTcpClient(ar);
		
		netStreams.Add(_client.GetStream());

		Debug.Log("Connected");
	}
	
	void FixedUpdate() {
		for (int i = 0; i < netStreams.Count; i++) {
			readStream(netStreams[i]);
		}
	}
	void readStream(NetworkStream ns) {
		StreamReader _reader = new StreamReader(ns);
		while (ns.CanRead && ns.DataAvailable) {
			Debug.Log(_reader.ReadLine());
		}
	}
	
	void Update () {
	
	}
}
