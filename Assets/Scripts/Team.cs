using UnityEngine;
using System.Collections.Generic;

public class Team : MonoBehaviour {
    public int startingHealth;
    public int healthLeft;

    public Transform spawnPoint;
    public List<string> members = new List<string>();
	public List<SpellProgress> inProgressSpells = new List<SpellProgress>();

	void Start()
    {
        healthLeft = startingHealth;
    }

	public void AddMember(string username) {
		members.Add(username);
	}

	public void PlaceMember(GameObject newMember) {
        newMember.transform.position = spawnPoint.position;

		float xPosRange = gameObject.name == "Red Castle" ? Random.Range(-.6F, .5F) : Random.Range(-.5F, .6F);
		Vector3 newPos = transform.position + new Vector3 (xPosRange, Random.Range(-.5F, -.7F), 0);
        newMember.GetComponent<Player>().targetLocation = newPos;
    }

	public void RemoveMember(string username) {
		//TODO: dead bod slingshot
		members.Remove(username);
	}

	private void AddToBoard(GameObject newMember) {
		//TODO: position player on board
	}

    public void TakeDamage(int damage) {
        healthLeft = Mathf.Max(healthLeft - damage, 0);
        if (healthLeft == 0) {
            // TODO: This team LOSES, other team WINS
        }
    }
}