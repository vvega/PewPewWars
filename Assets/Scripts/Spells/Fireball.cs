using UnityEngine;

public class Fireball : Spell {

	[Header("Templates")]
	public GameObject castTemplate;

	public override void Cast(Team myTeam, Team enemyTeam) {
		Debug.Log("BOOM");
		enemyTeam.TakeDamage(damage);
		int killCount = Mathf.Max(enemyTeam.members.Count/10, 1);
		int startIndex = Mathf.Max(Random.Range(0, enemyTeam.members.Count-(killCount-1)), 0);
		foreach (string enemyName in enemyTeam.members.GetRange(startIndex, killCount)) {
			gameObject.GetComponent<GameMaster>().GetPlayerObject(enemyName).GetComponent<Player>().DealDamage(damage);
		}

		GameObject cast = Instantiate(castTemplate);
		cast.transform.position = myTeam.gameObject.transform.position;
		cast.GetComponent<SpellSling>().target = enemyTeam.gameObject;
    }
}
