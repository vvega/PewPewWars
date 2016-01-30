using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private string username;
	public Team team;

	// Use this for initialization
	void Start () {
		Debug.Log ("New player!");
		Debug.Log (this);
		Debug.Log (team);
	}

	public void setTeam(Team team) {
		this.team = team;
	}

	public Team getTeam() {
		return team;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
