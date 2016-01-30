using UnityEngine;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {
    [Header("Templates")]
    public GameObject playerTemplate;

    private Dictionary<string, GameObject> userObjectDict = new Dictionary<string, GameObject>();
    private Dictionary<string, ChatCommand> chatCommandDict = new Dictionary<string, ChatCommand>();

    public void OnUserJoin(string username) {
        GameObject newUser = Instantiate(playerTemplate);
        newUser.transform.FindChild("Username").GetComponent<TextMesh>().text = username;

        userObjectDict.Add(username, newUser);
    }

    public void OnUserQuit(string username) {
        if (userObjectDict.ContainsKey(username) == false)
            return;

        Destroy(userObjectDict[username]);
        userObjectDict.Remove(username);
    }

    public void OnPrivateMessage(string username, string message) {
        if (message.StartsWith("!") == false)
            return;

        int paramStartIdx = message.IndexOf(' ');
        string command = message.Substring(0, (paramStartIdx >= 0 ? paramStartIdx : message.Length));
        string parameters = (paramStartIdx >= 0 ? message.Substring(command.Length) : "");

        if (chatCommandDict.ContainsKey(command) == false)
            return;

        chatCommandDict[command].ProcessCommand(this, username, parameters);
    }

    public GameObject GetPlayerObject(string username) {
        if (userObjectDict.ContainsKey(username) == false)
            return null;

        return userObjectDict[username];
    }

    void Start() {
        ChatCommand[] commands = GetComponents<ChatCommand>();
        foreach (ChatCommand command in commands) {
            chatCommandDict[command.Command] = command;
        }
    }
}
