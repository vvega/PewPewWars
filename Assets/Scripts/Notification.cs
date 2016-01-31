using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Notification : MonoBehaviour {

	float timeShow = 2.0f;
	bool isShowing = false;
	public Text uiMessage;

	string[] leaveMessages = {
		"let us all down.",
		"fled.",
		"returned to peasant life.",
		"had to go get dinner or something."
	};

	string[] joinMessages = {
		"wants in on this.",
		"finally showed up.",
		"was on their way to dinner but decided to stop and battle.",
		"wanted to try this game out."
	};

	// Use this for initialization
	void Start () {
		clearMessage();
	}

	public void announceUserJoin(string username) {
		int idx = Random.Range(0, joinMessages.Length - 1);
		string msg = joinMessages[idx];
		showMessage (username + " " + msg);
	}

	public void announceUserLeave(string username) {
		int idx = Random.Range(0, leaveMessages.Length - 1);
		string msg = leaveMessages[idx];
		showMessage (username + " " + msg);
	}

	public void announceUserDeath(string username) {
		//TODO
	}

	void showMessage(string message) {
		uiMessage.text = message;
		timeShow = 2.0f;
		isShowing = true;
	}

	void clearMessage() {
		uiMessage.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if(isShowing) 
		{
			timeShow-=Time.deltaTime;
			if (timeShow<0.0f)
			{
				isShowing = false;
				clearMessage();
			}
		}
	}
}
