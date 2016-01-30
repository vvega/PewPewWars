using UnityEngine;

public class NewPosition : ChatCommand {
    public override void ProcessCommand(GameMaster gameMaster, string username, string parameters)
    {
        GameObject playerObj = gameMaster.GetPlayerObject(username);
        if (playerObj == null)
            return;

        playerObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 10));
    }
}
