using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//realiza movimentacao horizontal e pulo
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
	
	public float jForce;//força do pulo
	public float hForce;//força horizontal, andar
	//public float hVel;//velocidade caso mude de direcao ou 
	public float hMax;//velocidade horizontal maxima

	public Player target;// Player que recebe o input desse script.
	public Transform checarChao;//ponto o qual é checado se colide com o chao
	private Rigidbody2D rigi;//rigidbody do target
	private bool grounded;//verifica se está tocando no solo

	void Start () {

        // Verfica se foi passado um Player para esse script, se nao tenta achar um.
        if(target == null) 
            target = GetComponent<Player>();
		
		rigi = target.GetComponent<Rigidbody2D>();
    }

	void FixedUpdate () {
		//movimento horizontal
	//	if(!(rigi.velocity.x > 0) && target.input.horizontalAxis >0){
	//		rigi.velocity = new Vector2(hVel,0);
	//	}else if(!(rigi.velocity.x < 0) && target.input.horizontalAxis <0){
	//		rigi.velocity = new Vector2(-hVel,0);
	//	}

		rigi.AddForce( transform.right * hForce * target.input.horizontalAxis );
		

		//atingindo velocidade maxima
		if(Mathf.Abs (rigi.velocity.x) > hMax)
			rigi.velocity = new Vector2(Mathf.Sign (rigi.velocity.x) * hMax, rigi.velocity.y);
		

		
		//verificando que esta tocando no chao
		grounded = Physics2D.Linecast(transform.position, checarChao.position, 1 << LayerMask.NameToLayer("Ground"));

		//Pulo
		if(target.input.jumpButton && grounded)
			rigi.AddForce(transform.up * jForce);	
		
	}
}
