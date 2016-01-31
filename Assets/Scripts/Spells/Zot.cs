using UnityEngine;

public class Zot : ChatCommand {
    public float timeVisible;
    public int damage;

    [Header("Templates")]
    public GameObject zotTemplate;

    public override void ProcessCommand(GameMaster gameMaster, string username, string parameters)
    {
        GameObject playerObj = gameMaster.GetPlayerObject(username);
        if (parameters.Length == 0)
        {
            Team otherTeam = gameMaster.GetOpposingTeam(playerObj.GetComponent<Player>().team);
            otherTeam.TakeDamage(damage);

            CreateZot(playerObj, otherTeam.gameObject);
        }
        else
        {
            GameObject target = gameMaster.GetPlayerObject(parameters);
            if (target != null)
            {
                CreateZot(playerObj, target);
            }
        }
    }

    private void CreateZot(GameObject caster, GameObject target)
    {
        GameObject zotObj = Instantiate(zotTemplate);
        zotObj.GetComponent<LineRenderer>().SetPosition(0, caster.transform.position);
        zotObj.GetComponent<LineRenderer>().SetPosition(1, target.transform.position);
        Destroy(zotObj, timeVisible);
    }
}
