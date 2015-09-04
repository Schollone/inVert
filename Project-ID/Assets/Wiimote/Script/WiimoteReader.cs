using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using WiimoteClassLib;

/*
 * Client that gets data from server (ReadWiimoteData.exe)
 */
public class WiimoteReader : MonoBehaviour
{

	[SerializeField]
	private bool _useWiiBoard = true;
	public static bool UseWiiBoard = true;

	public static int port = 8000;
	private static string filePath = "";
	private static bool running;
	private static Socket socket;

	private static System.Diagnostics.Process myProcess;

	[SerializeField] private float horizontalRestPercentage = 0.15f;
	[SerializeField] private float verticalRestPercntage = 0.2f;
	[SerializeField] private float verticalOffset = 0.2f;
	[SerializeField] private float weightOffset = 10f;

	private static WiimoteBalanceBoard b;

	public static bool BalanceBoardIsReady = false;

	private static WiimoteReader _instance = null;

	public static class Board
	{
		
		public enum Axis
		{
			Horizontal,
			Vertical,
			Left,
			Right,
			Top,
			Bottom
		}
		public enum AxisRaw
		{
			TopLeft,
			TopRight,
			BottomLeft,
			BottomRight,
			Weight
		}
		public enum Button
		{
			Left,
			Right,
			TopLeft,
			TopRight,
			BottomLeft,
			BottomRight,
			Jump
		}
	}

	public static class Controller
	{
		public enum Button
		{
			A,
			B,
			Up,
			Down,
			Right,
			Left,
			Minus,
			Plus,
			Home,
			One,
			Two
		}
		public enum Axis
		{
			Horizontal,
			Vertical
		}
		public enum AxisRaw
		{
			X,
			Y,
			Z
		}
		public enum IRRaw
		{
			X1,
			X2,
			Y1,
			Y2
		}
	}

	public static WiimoteBalanceBoard GetBalanceBoard ()
	{
		return b;
	}

	public static WiimoteReader Instance {
		get {
			if(_instance == null) {
				_instance = GameObject.FindObjectOfType<WiimoteReader>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}

	public static void StartReader ()
	{		
		try {
			//IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
			IPHostEntry ipHostInfo = Dns.GetHostEntry ("127.0.0.1");
			IPAddress ipAddress = ipHostInfo.AddressList [0];
			IPEndPoint remoteEP = new IPEndPoint (ipAddress, port);

			Debug.Log ("Port: " + port);

			// Create a TCP/IP  socket.
			socket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			// Connect the socket to the remote endpoint. Catch any errors.
			socket.Connect (remoteEP);

			Debug.Log ("Connected");


		} catch (Exception e) {
			Debug.Log (e.ToString ());
		}

		if (socket.Connected) {
			Read ();
		}

	}

	private static void Read ()
	{
		while (running) {
			Message message = new Message ();
			int received = socket.Receive (message.buffer);

			if (received > 0) {
				for (int i = 0; i < received; i++) {
					message.TransmissionBuffer.Add (message.buffer [i]);
				}
			}

			Message send = message.DeSerialize ();
			BalanceBoardMessage balanceBoard = send.getBalanceBoard ();

			if (balanceBoard.isConnected ()) {
				WiimoteReader.BalanceBoardIsReady = true;
				b.SetData (balanceBoard);
			}
		}
	}

	private static void startServer ()
	{
		filePath = Application.dataPath + "/Wiimote/ReadWiimoteData.exe";
		Debug.Log(filePath);
		System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo (filePath);
		startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;	
		startInfo.Arguments = "" + port;
		Debug.Log (startInfo.Arguments);
		
		myProcess = System.Diagnostics.Process.Start (startInfo);
	}

	void Awake() {
		if(_instance == null) {
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		} else {
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance) {
				Destroy(this.gameObject);
			}
		}
	}
	
	void Start ()
	{
		Debug.LogWarning("Start WiimoteReader");
		if (!_useWiiBoard) {
			UseWiiBoard = false;
			return;
		}

		if (b == null) {
			b = new WiimoteBalanceBoard (horizontalRestPercentage, verticalRestPercntage, verticalOffset);
		}

		startServer ();

		Invoke ("connectToServer", 2);
	}

	private void connectToServer ()
	{
		Debug.Log ("connect to Server");

		running = true;

		b.SetJumpOffset (weightOffset);

		Thread readThread = new Thread (new ThreadStart (StartReader));
		readThread.Start ();
	}
	
	void OnApplicationQuit ()
	{
		if (this._useWiiBoard) {
			Debug.LogWarning ("Disconnected/Closed");
			running = false;
			myProcess.Kill ();
		}

		WiimoteReader._instance = null;
	}    
}
