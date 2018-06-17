using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//realiza movimentacao horizontal e pulo
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
	
	public float alturaPulo;//altura maxima do pulo
	private float gravidade;
	public float hAce;//aceleracao horizontal(andar)
	public float hMax;//velocidade horizontal maxima

	public Player target;// Player que recebe o input desse script.
	public Transform checarChao;//ponto o qual é checado se colide com o chao
	public Transform checarChao2;//ponto o qual é checado se colide com o chao
	private Rigidbody2D rigi;//rigidbody do target
	private bool grounded;//verifica se está tocando no solo

	private float hNewVel;//nova velocidade horizontal do player
	private float vNewVel;//nova velocidade vertical do player

	void Start () {

        // Verfica se foi passado um Player para esse script, se nao tenta achar um.
        if(target == null) 
            target = GetComponent<Player>();
		
		rigi = target.GetComponent<Rigidbody2D>();
		gravidade =rigi.gravityScale;
    }

	void FixedUpdate () {

		//movimento horizontal
		hNewVel = hAce*target.input.horizontalAxis + rigi.velocity.x;
		vNewVel = rigi.velocity.y;
		
		//atingindo velocidade maxima
		if(Mathf.Abs (rigi.velocity.x) > hMax)
			hNewVel = Mathf.Sign (rigi.velocity.x) * hMax;
		

		//verificando que esta tocando no chao
		grounded = Physics2D.Linecast(transform.position, checarChao.position, 1 << LayerMask.NameToLayer("Ground")) ||
		Physics2D.Linecast(transform.position, checarChao2.position, 1 << LayerMask.NameToLayer("Ground"));

		//evitando a soma de addForces no pulo
		if(target.input.jumpButton && grounded){
			vNewVel = 0;	
		}

		rigi.velocity = new Vector2(hNewVel,vNewVel);
		
		//Pulo
		if(target.input.jumpButton && grounded){
			rigi.AddForce(Mathf.Sqrt(2*alturaPulo*gravidade)*Vector2.up,ForceMode2D.Impulse);	
		}
	}
}
