using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using UnityEngine.UI;

public class PhotonEngine : MonoBehaviour,IPhotonPeerListener {

	private static PhotonEngine Instance;
	private static PhotonPeer peer;
	public Text text;
	public static PhotonPeer Peer
	{
		get
		{ 
			return peer;
		}
	}
	private void Awake(){
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (this.gameObject);
		}else if (Instance != this) {
			Destroy (this.gameObject);return;
		}
	}

	void Start(){
		//连接服务器端
		//通过Listender连接服务器端的响应
		//第一个参数 指定一个Licensed（监听器）,第二个参数使用协议类型
		peer = new PhotonPeer(this,ConnectionProtocol.Udp);
		//连接 UDP的UP地址：端口号，Application的名字
		peer.Connect("193.112.155.138:5055","MyGame1");
		print (peer.PeerState);
	}
	void Update(){
		peer.Service ();
	}
	//游戏关闭时调用
	private void OnDestroy()
	{
		//如果peer不等于空并且状态为正在连接
		if (peer != null && peer.PeerState == PeerStateValue.Connected) 
		{
			peer.Disconnect ();//断开连接
		}
	}

	public void DebugReturn(DebugLevel level,string message)
	{}

	//如果客户端没有发起请求时，服务器端向客户端发送通知就会通过OnEvent来进行响应
	public void OnEvent(EventData eventData)
	{
//		switch (eventData.Code) {
//		case 1:
//			Debug.Log ("服务器直接发送了数据过来");
//			Dictionary<byte,object> data = eventData.Parameters;
//			object intValue;
//			object stringValue;
//			data.TryGetValue (1, out intValue);
//			data.TryGetValue (2, out stringValue);
//			Debug.Log ("收到服务器的数据信息:" + intValue.ToString () + stringValue.ToString ());
//			break;
//		default:
//			break;
//		}
	}
	//当客户端想服务器发送请求后，服务器通过OnOperationResponse接收处理请求，给予客户端响应
	public void OnOperationResponse(OperationResponse operationResponse)
	{
		switch (operationResponse.OperationCode) {
		case 1:
			Debug.Log ("收到服务器的响应");
			Dictionary<byte,object> data = operationResponse.Parameters;
			object intValue;
			object stringValue;
			data.TryGetValue (1, out intValue);
			data.TryGetValue (2, out stringValue);
//			data.TryGetValue (3, out stringValue);
			Debug.Log ("收到服务器的数据信息" + intValue.ToString () + stringValue.ToString ());
			text.text = intValue.ToString () + ":   " + stringValue.ToString ();
			break;
		default:
			break;
		}
	}
	//连接状态发生改变的时触发
	//连接状态有五种
	//  正在连接中(PeerStateValue.Connecting),
	//	已经连接上(PeerStateValue.Connected),
	//	正在断开连接中(PeerStateValue.Disconnecting),
	//	已经断开连接(PeerStateValue.Disconnected),
	//	正在进行初始化(PeerStateValue.InitializingApplication)
	public void OnStatusChanged(StatusCode statusCode)
	{}

}
