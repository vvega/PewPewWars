using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public float current;
	public float max;

	private float height;

	// Use this for initialization
	void Start () {
		max = this.gameObject.GetComponent<RectTransform>().rect.width;
		height = this.gameObject.GetComponent<RectTransform>().rect.height;

		reset();
	}

	public void reset() {
		current = max;
		adjustWidth ();
	}

	public void updatePercentage(int currentHealth, int maxHealth) {
		current = ((float)currentHealth/(float)maxHealth) * max;
		adjustWidth ();
	}

	private void adjustWidth() {
		this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(current, height);
	}

	// Update is called once per frame
	void Update () {
	}
}