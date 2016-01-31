using UnityEngine;

public class MagicMissile : Spell {
	// public float timeVisible;

	[Header("Templates")]
	public GameObject castTemplate;

	public override void Cast (Team myTeam, Team enemyTeam) {
		Debug.Log("I cast magic missile at the darkness!");
		enemyTeam.TakeDamage(damage);

		/*GameObject magicMissileObj = Instantiate(magicMissileTemplate);
		magicMissileObj.GetComponent<LineRenderer>().SetPosition(0, myTeam.gameObject.transform.position);
		magicMissileObj.GetComponent<LineRenderer>().SetPosition(1, enemyTeam.gameObject.transform.position);
		Destroy(magicMissileObj, timeVisible);*/

		GameObject cast = Instantiate(castTemplate);
		cast.transform.position = myTeam.gameObject.transform.position;
		cast.GetComponent<SpellSling>().target = enemyTeam.gameObject;
	}
}
