using UnityEngine;
using UnityEngine.Events;

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

[System.Serializable]
public class UserEvent : UnityEvent<string> { }

[System.Serializable]
public class UserMessageEvent : UnityEvent<string, string> { }

[System.Serializable]
public class ChatUser {
    public string username;
    public bool isOperator;

    public ChatUser(string name, bool op) {
        username = name;
        isOperator = op;
    }
}

public class TwitchIRC : MonoBehaviour {
    [Header("Bot Info")]
    public bool connectOnStart;
    [SerializeField]
    private string username = "";
    [SerializeField]
    private string password = "";
    [SerializeField]
    private string channel = "";

    [Header("Channel Info")]
    public List<ChatUser> userList = new List<ChatUser>();

    [Header("Events")]
    public UnityEvent OnConnected;
    public UserEvent OnUserJoinChannel;
    public UserEvent OnUserLeaveChannel;
    public UserEvent OnUserOperatorChange;
    public UserMessageEvent OnPrivateMessage; // <username, message>

    private const string TwitchServerAddress = "irc.twitch.tv";
    private const int TwitchServerPort = 6667;

    private TcpClient irc;
    private NetworkStream ircStream;
    private StreamReader reader;
    private StreamWriter writer;

    // ----------------------------------------------------------------------------- //

    public void Connect() { 
        if (irc != null) {
            Debug.LogWarning("Tried to connect when connection was already live");
            return;
        }

        irc = new TcpClient(TwitchServerAddress, TwitchServerPort);
        ircStream = irc.GetStream();
        reader = new StreamReader(ircStream);
        writer = new StreamWriter(ircStream);

        Send("USER " + username + "tmi twitch :" + username);
        Send("PASS " + password);
        Send("NICK " + username);

        StartCoroutine("Listen");
    }

    public void Disconnect() {
        irc = null;
        StopCoroutine("Listen");

        if (ircStream != null)
            ircStream.Dispose();
        if (writer != null)
            writer.Dispose();
        if (reader != null)
            reader.Dispose();
    }

    public void JoinChannel() {
        if (irc == null || irc.Connected == false) {
            Debug.LogWarning("Tried to join channel without valid connection to server");
            return;
        }

        Send("JOIN " + channel);
    }

    public void MessageChannel(string message) {
        Send("PRIVMSG " + channel + " :" + message);
    }

    public void Send(string message) {
        writer.WriteLine(message);
        writer.Flush();
    }

    public ChatUser GetChatUserByName(string username) {
        return userList.Find(delegate (ChatUser u) { return u.username == username; });
    }

    // ----------------------------------------------------------------------------- //

	void Start () {
        if (connectOnStart)
            Connect();
	}
	
	IEnumerator Listen () {
        while (true) {
            if (ircStream.DataAvailable == false) { 
                yield return null;
                continue;
            }

            string line = reader.ReadLine();
            string[] ircData = line.Split(' ');

            if (line.Substring(0, 4) == "PING") {
                Send("PONG " + ircData[1]);
                continue;
            }

            Debug.Log(line);

            switch(ircData[1]) {
                case "001": // Server welcome message
                    Send("MODE " + username + " +B");
                    Send("CAP REQ :twitch.tv/membership");
                    Send("CAP END");

                    OnConnected.Invoke();

                    JoinChannel();
                    break;

                case "353":
                    for (int i = 5; i < ircData.Length; ++i) {
                        string userNick = (i == 5 ? ircData[i].Substring(1) : ircData[i]);
                        if (GetChatUserByName(userNick) != null)
                            continue;

                        userList.Add(new ChatUser(userNick, false));
                        OnUserJoinChannel.Invoke(userNick);
                    }
                    break;

                case "MODE":
                    GetChatUserByName(ircData[4]).isOperator = (ircData[3] == "+o");
                    OnUserOperatorChange.Invoke(ircData[4]);
                    break;

                case "JOIN": { 
                        string join_sender = GetUserFromMessage(ircData[0]);
                        if (GetChatUserByName(join_sender) == null) {
                            userList.Add(new ChatUser(join_sender, false));
                            OnUserJoinChannel.Invoke(join_sender);
                        }
                    }

                    break;

                case "PART":
                    userList.RemoveAll(delegate (ChatUser u) { return u.username == GetUserFromMessage(ircData[0]); });
                    OnUserLeaveChannel.Invoke(GetUserFromMessage(ircData[0]));
                    break;

                case "PRIVMSG": {
                        string message_sender = GetUserFromMessage(ircData[0]);
                        if (GetChatUserByName(message_sender) == null) {
                            userList.Add(new ChatUser(message_sender, false));
                            OnUserJoinChannel.Invoke(message_sender);
                        }

                        OnPrivateMessage.Invoke(message_sender, ircData[3].Substring(1));
                    }
                    
                    break;
            }
        }
	}

    private string GetUserFromMessage(string id_string) {
        return id_string.Substring(1, id_string.IndexOf('!') - 1);
    }
}
