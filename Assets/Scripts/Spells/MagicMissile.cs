using UnityEngine;

public class MagicMissile : Spell {
	public float timeVisible;

	[Header("Templates")]
	public GameObject magicMissileTemplate;

	public override void Cast (Team myTeam, Team enemyTeam) {
		enemyTeam.TakeDamage(damage);
		Debug.Log("I cast magic missile at the darkness!");

		GameObject magicMissileObj = Instantiate(magicMissileTemplate);
		magicMissileObj.GetComponent<LineRenderer>().SetPosition(0, myTeam.gameObject.transform.position);
		magicMissileObj.GetComponent<LineRenderer>().SetPosition(1, enemyTeam.gameObject.transform.position);
		Destroy(magicMissileObj, timeVisible);
	}
}
