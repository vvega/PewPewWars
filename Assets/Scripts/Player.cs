using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Wizardly Params")]
    public int startingHealth;
    public float walkSpeed;
    [Range(2.0f, 10.0f)]
    public float wanderTime;

    [Header("In-Game Vars")]
    public string username;
	public Team team;

    public Vector3 targetLocation;
    private int remainingHealth;
    private float remainingTime;

    [Header("Sounds")]
    public AudioClip[] death;

    [Header("Templates")]
    public GameObject deadTemplate;

    // Use this for initialization
    void Start () {
        remainingHealth = startingHealth;
        remainingTime = wanderTime;

        SetNewWanderPosition();
        transform.position = team.spawnPoint.position;
        transform.FindChild("Username").GetComponent<TextMesh>().text = username;
    }

    public void DealDamage(int damage)
    {
        if (gameObject.activeSelf == false)
            return;

        remainingHealth = Mathf.Max(remainingHealth - damage, 0);
        if (remainingHealth == 0)
        {
            Die();
        }
    }

    [ContextMenu("Die")]
    void Die()
    {
        gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(death[Random.Range(0, death.Length)], Vector3.zero);

        Instantiate(deadTemplate).transform.position = transform.position;
        Invoke("Respawn", 10.0f);

        GameObject.Find("Chat Reader").GetComponent<TwitchIRC>().MessageChannel(username + " has died!");
    }

    void Respawn()
    {
        remainingHealth = startingHealth;

        gameObject.SetActive(true);
        transform.position = team.spawnPoint.position;

        GameObject.Find("Chat Reader").GetComponent<TwitchIRC>().MessageChannel(username + " has returned!");
    }

    public void setTeam(Team team) {
		this.team = team;
		team.AddMember(username);
	}

	public Team getTeam() {
		return team;
	}

    void Update()
    {
        if (Vector3.Distance(transform.position, targetLocation) > 0.001f)
        {
            Vector3 direction = (targetLocation - transform.position).normalized;
            transform.position += direction * Time.deltaTime * walkSpeed;

            if (direction.y < -0.49f)
                SetWalkDirection(1);
            else if (direction.x > 0.49f)
                SetWalkDirection(2);
            else if (direction.y > 0.49f)
                SetWalkDirection(3);
            else
                SetWalkDirection(4);
        }
        else
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0.0f)
            {
                SetNewWanderPosition();
                remainingTime = Random.Range(2.0f, wanderTime);
            }

            SetWalkDirection(0);
        }
    }

    int GetWalkDirection()
    {
        return GetComponent<Animator>().GetInteger("walkDirection");
    }

    void SetWalkDirection(int dir)
    {
        GetComponent<Animator>().SetInteger("walkDirection", dir);
    }

    void SetNewWanderPosition()
    {
        float xPosRange = (team.name == "Red Castle") ? Random.Range(-.6F, .5F) : Random.Range(-.5F, .6F);
        Vector3 newPos = team.transform.position + new Vector3(xPosRange, Random.Range(-.5F, -.7F), 0);
        targetLocation = newPos;
    }
}
