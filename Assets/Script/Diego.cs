using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diego : MonoBehaviour {

	private Rigidbody2D rBody;


	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		movimentar ();
	}

	private void movimentar () {
		rBody.velocity = Vector2.right;
	}
}
