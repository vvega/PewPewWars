using UnityEngine;
using System.Collections;

public class SpellSling : MonoBehaviour {

	public float speed;
	public GameObject target;

	void Update () {
		gameObject.GetComponent<SpriteRenderer>().flipX = (target.transform.position.x > transform.position.x);

		float step = speed * Time.deltaTime;
		transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
		if (transform.position == target.transform.position) {
			Destroy(gameObject);
		}
	}
}
