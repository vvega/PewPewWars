using UnityEngine;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {
    [Header("References")]
    public Team redTeam;
    public Team blueTeam;

    [Header("Templates")]
    public GameObject playerTemplate;

    public Dictionary<string, GameObject> userObjectDict = new Dictionary<string, GameObject>();
    private Dictionary<string, ChatCommand> chatCommandDict = new Dictionary<string, ChatCommand>();

    public void OnUserJoin(string username) {

        Team newPlayerTeam = (redTeam.members.Count > blueTeam.members.Count ? blueTeam : redTeam);

		GameObject newUser = Instantiate(playerTemplate);
		Player player = newUser.GetComponent<Player>();
		player.username = username;
        player.setTeam(newPlayerTeam);

        userObjectDict.Add(username, newUser);
    }

    public void OnUserQuit(string username) {
        if (userObjectDict.ContainsKey(username) == false)
            return;

		Player player = userObjectDict[username].GetComponent<Player>();
		player.getTeam().RemoveMember(username);

        Destroy(userObjectDict[username]);
        userObjectDict.Remove(username);
    }

    public void OnPrivateMessage(string username, string message) {
        if (message.StartsWith("!") == false)
            return;

        int paramStartIdx = message.IndexOf(' ');
        string command = message.Substring(0, (paramStartIdx >= 0 ? paramStartIdx : message.Length));
        string parameters = (paramStartIdx >= 0 ? message.Substring(command.Length).Trim() : "");

        if (chatCommandDict.ContainsKey(command) == false)
            return;

        chatCommandDict[command].ProcessCommand(this, username, parameters);
    }

    public GameObject GetPlayerObject(string username) {
        if (userObjectDict.ContainsKey(username) == false)
            return null;

        return userObjectDict[username];
    }

    public Team GetOpposingTeam(Team t)
    {
        return (t == redTeam ? blueTeam : redTeam);
    }

    void Start() {
        ChatCommand[] commands = GetComponents<ChatCommand>();

        foreach (ChatCommand command in commands) {
            chatCommandDict[command.Command] = command;
        }
    }
}
