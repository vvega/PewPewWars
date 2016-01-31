using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Team : MonoBehaviour {
    [Header("Params")]
    public int startingHealth;
    public int healthLeft;

    [Header("References")]
    public Transform spawnPoint;
    public HealthBar healthBar;

    public List<string> members = new List<string>();
	public List<SpellProgress> inProgressSpells = new List<SpellProgress>();
	public int otherTeamNumWins;
	public Text otherTeamWinLabel;

    [Header("Templates")]
    public GameObject halfExplosionTemplate;

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
        float prevHealthPercentage = (float)healthLeft / (float)startingHealth;

        healthLeft = Mathf.Max(healthLeft - damage, 0);
		healthBar.updatePercentage(healthLeft, startingHealth);

        float newHealthPercentage = (float)healthLeft / (float)startingHealth;
        if (prevHealthPercentage > 0.5f && newHealthPercentage <= 0.5f)
        {
            Instantiate(halfExplosionTemplate);
        }

        if (healthLeft == 0) {
            // TODO: This team LOSES, other team WINS
			otherTeamNumWins++;
			otherTeamWinLabel.text = otherTeamNumWins + " " + (otherTeamNumWins == 1 ? "win" : "wins");
			healthBar.reset();
        }
    }
}