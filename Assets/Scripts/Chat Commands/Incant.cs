using UnityEngine;
using System.Collections;

public class Incant : ChatCommand {
	
	void Start () {
		this.Command = "cast";
	}
	
	public override void ProcessCommand(GameMaster gameMaster, string username, string parameters)
	{
		Debug.Log(username+" incanted "+parameters);

		GameObject playerObj = gameMaster.GetPlayerObject(username);
		if (playerObj == null)
			return;

		// TODO: get team and try to be useful
	}
}
