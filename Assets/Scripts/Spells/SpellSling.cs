using UnityEngine;
using System.Collections;

public class SpellSling : MonoBehaviour {

	public float speed;
	public GameObject target;

	void Update () {
		float step = speed * Time.deltaTime;
		transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
		if (transform.position == target.transform.position) {
			Destroy(gameObject);
		}
	}
}
