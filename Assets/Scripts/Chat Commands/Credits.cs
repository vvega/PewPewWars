using UnityEngine;

public class Credits : ChatCommand {

	public override void ProcessCommand(GameMaster gameMaster, string username, string parameters) {
		TwitchIRC irc = gameMaster.GetComponent<TwitchIRC>();
		irc.MessageChannel("Credits: (DrMrWaffles, Jaadus, SaucyDragon, DewPyrin, Flondrixa)");
	}
}
