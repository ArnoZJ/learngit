using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhotonText : MonoBehaviour {

	void Update () {
		//点击触发服务器请求
		if (Input.GetMouseButtonDown(0)) {
			SendRequest ();
		}
		if (Input.touchCount==1) {
			if (Input.touches[0].phase==TouchPhase.Began) {
				SendRequest ();
			}
		}
	}

	void SendRequest()
	{
		Dictionary<byte,object> data = new Dictionary<byte,object> ();
		data.Add (1, 100);
		data.Add (2, "abc你好");
		//参数1 opcode表示发起请求的编号，便于接受查找，参数2 客户端需要向服务器传递的数据，是否稳定（如果为false表示数据丢失一两条也没啥）
		PhotonEngine.Peer.OpCustom(1,data,true);//测试，传递了空的数据
	}
}
