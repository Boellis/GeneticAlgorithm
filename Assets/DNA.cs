using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour {

    //Gene for color
    //Red
    public float r;
    //Green
    public float g;
    //Blue
    public float b;
    //Records how long the person lived for
    public float timeToDie = 0;

    //Size
    public float size;

    //Checks to see if person is dead
    bool dead = false;

    //Gives access to the sprite renderer
    SpriteRenderer sRenderer;
    //Gives access to the collider2d
    Collider2D sCollider;

    

     void OnMouseDown()
    {
        dead = true;
        timeToDie = PopulationManager.elapsed;
        Debug.Log("Dead at: " + timeToDie);
        sRenderer.enabled = false;
        sCollider.enabled = false;

    }

    // Use this for initialization
    void Start () {
        //Sets our variables to the start position of the person.
        sRenderer = GetComponent<SpriteRenderer>();
        sCollider = GetComponent<Collider2D>();
        sRenderer.color = new Color(r, g, b);
        this.transform.localScale = new Vector3(size, size, size);
        //sRenderer.size = gameObject.transform.localScale = new Vector3(.5f, Random.Range(0.1f,.5f), .5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
