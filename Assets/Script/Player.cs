using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float velocidade;

	public Transform player;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = player.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Movimentar ();
	} 

	void Movimentar() {

		animator.SetFloat ("run", Mathf.Abs(Input.GetAxis("Horizontal")));

		if (Input.GetAxis ("Horizontal") > 0) {
			transform.Translate (Vector2.right * velocidade * Time.deltaTime);
			transform.eulerAngles = new Vector2 (0, 0);
		}

		if (Input.GetAxis ("Horizontal") < 0) {
			transform.Translate (Vector2.right * velocidade * Time.deltaTime);
			transform.eulerAngles = new Vector2 (0, 180);
		}

	}
}
