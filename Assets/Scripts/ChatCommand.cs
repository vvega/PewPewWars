using UnityEngine;

public abstract class ChatCommand : MonoBehaviour {
    public string Command;

    public virtual void ProcessCommand(GameMaster gameMaster, string username, string parameters) { }
}
