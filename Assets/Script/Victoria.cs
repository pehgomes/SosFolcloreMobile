using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victoria : MonoBehaviour {

	private Rigidbody2D rBody;

	private Animator animator;

	private bool isLadoDireito;

	[SerializeField]
	private float velocidade = 0f;

	public float impulso = 200f;

	float sentidoX;

	public LayerMask plataforma;

	public Vector2 pontoColisao = Vector2.zero;

	public bool isEstaNoChao;

	public float raio;

	public Color debugColisao = Color.red;


	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		isLadoDireito = transform.localScale.x > 0;

	}

	// Update is called once per frame
	void FixedUpdate () {
		sentidoX = Input.GetAxis("Horizontal");

		movimentar (sentidoX);
		mudarSentido (sentidoX);
		estaNoChao ();
		ControlarLayers ();
		controleEntrada ();
	}

	private void estaNoChao() {
		var pontoPosicao = pontoColisao;
		pontoPosicao.x += transform.position.x; 
		pontoPosicao.y += transform.position.y; 

		isEstaNoChao = Physics2D.OverlapCircle (pontoPosicao, raio, plataforma);
		cair ();
	}

	private void movimentar (float sentido) {
		rBody.velocity = new Vector2 (sentido * velocidade , rBody.velocity.y);
		animator.SetFloat ("veloc", Mathf.Abs(sentido));
		if (rBody.velocity.y == 0) {
			rBody.gravityScale = 1;
		}
	}

	private void pular() {
		rBody.gravityScale = 1.6f;
		if (isEstaNoChao && rBody.velocity.y <= 0) 
		{
			rBody.AddForce (new Vector2(0, impulso));
			animator.SetTrigger ("pular");
		}
	}

	private void cair() {
		if (!isEstaNoChao && rBody.velocity.y <= 0) {
			animator.SetBool ("cair", true);
			animator.ResetTrigger ("pular");
		} 

		if (isEstaNoChao) {
			animator.SetBool ("cair", false);
		}
	}

	private void controleEntrada() {
		if (Input.GetButtonDown ("Jump")) {
			pular ();
		}
	}

	private void mudarSentido(float horizontal) {
		if (horizontal > 0 && !isLadoDireito || horizontal < 0 && isLadoDireito) {
			isLadoDireito = !isLadoDireito;
			transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
	}


	void OnDrawGizmos() {
		Gizmos.color = debugColisao;
		var pontoPosicao = pontoColisao;
		pontoPosicao.x += transform.position.x; 
		pontoPosicao.y += transform.position.y; 

		isEstaNoChao = Physics2D.OverlapCircle (pontoPosicao, raio, plataforma);
		Gizmos.DrawWireSphere (pontoPosicao, raio);
	}

	void ControlarLayers(){
		if (!isEstaNoChao) {
			animator.SetLayerWeight (1, 1);
		} else {
			animator.SetLayerWeight (1, 0);
		}
	}
}
