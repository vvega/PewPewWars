using UnityEngine;

public class Dead : MonoBehaviour 
{
    public float floatTime;
    public float floatSpeed;
    private float timeLeft;

    void Start()
    {
        timeLeft = floatTime;
    }
	
	void Update ()
    {
        transform.position += new Vector3(0, floatSpeed * Time.deltaTime, 0);
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Color clr = renderer.color;
        clr.a = (timeLeft / floatTime);
        renderer.color = clr;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
            Destroy(gameObject);
	}
}
