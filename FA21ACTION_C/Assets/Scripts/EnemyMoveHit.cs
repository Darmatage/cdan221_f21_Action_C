using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMoveHit : MonoBehaviour {

	public Animator anim;
	public float speed = 4f;
	private Transform target;
	public int damage = 5;
	public float damageRate = 0.2f;
	public bool canAttack = false;
	public float attackTimer = 0;

	public Vector3 offsetAttack ;
	public float pushBackAmt = 3;

	public int EnemyLives = 3;
	private Renderer rend;
	private GameHandler gameHandler;

	public float attackRange = 10;

	public float scaleX; 

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

		if ((target != null) && (DistToPlayer <= attackRange)){
			//Vector3 offsetAttack = new Vector3 (0.5f, 0.5f, 0f);
			transform.position = Vector2.MoveTowards (transform.position, target.position + offsetAttack, speed * Time.deltaTime);
			//if enemy is passing through colliders, change transform.position to rigidbody.addforce
			anim.SetBool("Walk", true);
			if (target.position.x > gameObject.transform.position.x){
				gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
			} else {
				gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
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
			//anim.SetBool("Attack", true);
            gameHandler.playerGetHit(damage);
            Debug.Log("I'm Attacking!");
            attackTimer = 0;
			float pushBack = 0;
				if (target.position.x > gameObject.transform.position.x){
					pushBack = pushBackAmt;
				}
				else {
					pushBack = pushBackAmt *-1;
				}
					target.position = new Vector3(transform.position.x + pushBack, transform.position.y + 1, 0);
			}
		}
	}

	public void OnCollisionStay2D(Collision2D collision){
              if (collision.gameObject.tag == "Player") {
                     anim.SetBool("Attack", true);
                     canAttack = true;
                     rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
                     StartCoroutine(ResetColor());
              }
       }

       public void OnCollisionExit2D(Collision2D collision){
              if (collision.gameObject.tag == "Player") {
                     anim.SetBool("Attack", false);
                     Debug.Log("Attack stopped");
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
