using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMoveHit : MonoBehaviour {

	public Animator anim;
	public float speed = 4f;
	public Transform target;
	public int damage = 5;
	public float damageRate = 0.2f;
	public bool canAttack = false;
	public float attackTimer = 0;

	public Vector3 offsetAttack;
	public float pushBackAmt = 2;

	public int EnemyLives = 3;
	private Renderer rend;
	private GameHandler gameHandler;

	public float attackRange = 8;

	private float scaleX; 
	public bool isDead = false; 
	public bool isCatboy = false;


	void Start () {
		rend = GetComponentInChildren<Renderer> ();
		anim = GetComponentInChildren<Animator> ();
		scaleX = gameObject.transform.localScale.x;

		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		}

		if (GameObject.FindWithTag ("GameHandler") != null) {
			gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler> ();
		}

		offsetAttack = new Vector3 (0.5f, 0.5f, 0f);
	}

	void Update () {
		float DistToPlayer = Vector3.Distance(transform.position, target.position);

		if ((target != null) && (DistToPlayer <= attackRange) && (isDead == false)){
			//Vector3 offsetAttack = new Vector3 (0.5f, 0.5f, 0f);
			transform.position = Vector2.MoveTowards (transform.position, target.position + offsetAttack, speed * Time.deltaTime);
			//if enemy is passing through colliders, change transform.position to rigidbody.addforce
			anim.SetBool("Walk", true);
			if (target.position.x > gameObject.transform.position.x){
				gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
				if ((canAttack)&&(isCatboy)){
					//transform.position = new Vector2(transform.position.x +1, transform.position.y);
					Vector2 shift = new Vector2(transform.position.x + 0.5f, transform.position.y);
					Vector2 pos = Vector2.Lerp ((Vector2)transform.position, (Vector2)shift, Time.fixedDeltaTime);
					transform.position = new Vector2 (pos.x, pos.y);
					}
			} else {
				gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
				if ((canAttack)&&(isCatboy)){
					//transform.position = new Vector2(transform.position.x -1, transform.position.y);
					Vector2 shift = new Vector2(transform.position.x - 0.5f, transform.position.y);
					Vector2 pos = Vector2.Lerp ((Vector2)transform.position, (Vector2)shift, Time.fixedDeltaTime);
					transform.position = new Vector2 (pos.x, pos.y);
					}
			}
		}
		else{
			anim.SetBool("Walk", false);
		}
	}

	void FixedUpdate () {
		if(canAttack) {
			attackTimer += 0.01f;
			if(attackTimer >= damageRate ) {
				anim.SetTrigger("Attack");
				gameHandler.playerGetHit(damage);
				//Debug.Log("I'm Attacking!");
				attackTimer = 0;
				
				float pushBack = 0;
				if (isCatboy == false){
					if (target.position.x > gameObject.transform.position.x){
						pushBack = pushBackAmt;
					}
					else {
						pushBack = pushBackAmt *-1;
					}
					target.position = new Vector3(target.position.x + pushBack, target.position.y + 1, 0);
				}
			}
		}
	}

	//student fighters 
	public void OnCollisionStay2D(Collision2D collision){
		if (collision.gameObject.tag == "Player") {
			anim.SetBool("Attack", true);
			canAttack = true;
			//rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
			//StartCoroutine(ResetColor());
		}
	}

	public void OnCollisionExit2D(Collision2D collision){
		if (collision.gameObject.tag == "Player") {
			anim.SetBool("Attack", false);
			//Debug.Log("Attack stopped");
			canAttack = false;
		}
	}

	//RA fighter
	public void OnTriggerStay2D(Collider2D collision){
		if (collision.gameObject.tag == "Player") {
			//anim.SetBool("Attack", true);
			canAttack = true;
			//rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
			//StartCoroutine(ResetColor());
		}
	}

	public void OnTriggerExit2D(Collider2D collision){
		if (collision.gameObject.tag == "Player") {
			//anim.SetBool("Attack", false);
			//Debug.Log("Attack stopped");
			canAttack = false;
		}
	}


	IEnumerator ResetColor(){
		yield return new WaitForSeconds(0.5f);
		rend.material.color = Color.white;
	}

       //DISPLAY the range of enemy's attack when selected in the Editor
	void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
}
