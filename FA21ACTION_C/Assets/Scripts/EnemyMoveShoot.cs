using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMoveShoot : MonoBehaviour {

	public Animator anim;
	public float speed = 2f;
	public float stoppingDistance = 4f; // when enemy stops moving towards player
	public float retreatDistance = 3f; // when enemy moves away from approaching player
	private float timeBtwShots;
	public float startTimeBtwShots = 2;
	public GameObject projectile;
	public Transform throwPoint;
	
	public Transform slashPnt;
	public int slashDamage = 5;
	public LayerMask playerLayer;
	public float pushBackAmt = 3;

	private Rigidbody2D rb;
	private Transform player;
	private Vector2 PlayerVect;

	private GameHandler gameHandler;

	public float attackRange = 20;
	public float slashRange = 10; 
	public bool isAttacking = false;

	private float scaleX; 

	void Start () {
	Physics2D.queriesStartInColliders = false;

		rb = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		PlayerVect = player.transform.position;
		scaleX = gameObject.transform.localScale.x;

		timeBtwShots = startTimeBtwShots;

		anim = GetComponentInChildren<Animator> ();

        if (GameObject.FindWithTag ("GameHandler") != null) {
            gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler> ();
        }
	}

	void Update () {
		float DistToPlayer = Vector2.Distance(transform.position, player.position);
		if ((player != null) && (DistToPlayer <= attackRange)) {
			// approach player
			if (Vector2.Distance (transform.position, player.position) > stoppingDistance) {
				transform.position = Vector2.MoveTowards (transform.position, player.position, speed * Time.deltaTime);
				if (isAttacking == false) {
					anim.SetBool("Walk", true);
				}
				
				//Vector2 lookDir = PlayerVect - rb.position;
				//float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
				//rb.rotation = angle;
			}
			// stop moving
			else if (Vector2.Distance (transform.position, player.position) < stoppingDistance && Vector2.Distance (transform.position, player.position) > retreatDistance) {
				transform.position = this.transform.position;
				anim.SetBool("Walk", false);
			}

			// retreat from player
			else if (Vector2.Distance (transform.position, player.position) < retreatDistance) {
				transform.position = Vector2.MoveTowards (transform.position, player.position, -speed * Time.deltaTime);
				if (isAttacking == false) {
					anim.SetBool("Walk", true);
				}
			}

			//attack timer and manager
			if (timeBtwShots <= 0) {
				isAttacking = true;
				if (DistToPlayer > slashRange){
					//throw attack
					anim.SetBool("Throw", true);
					Instantiate (projectile, throwPoint.position, Quaternion.identity);
				}
				else{
					//slash attack
					anim.SetBool("Attack", true);
					Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(slashPnt.position, slashRange, playerLayer);
					
					foreach(Collider2D aPlayer in hitPlayer){
						Debug.Log("Boss hit " + aPlayer.name);
						gameHandler.playerGetHit(slashDamage);
						float pushBack = 0;
						Transform target = aPlayer.gameObject.transform;
						if (aPlayer.gameObject.transform.position.x > gameObject.transform.position.x){
							pushBack = pushBackAmt;
						}
						else {
							pushBack = pushBackAmt *-1;
						}
						aPlayer.gameObject.transform.position = new Vector3(transform.position.x + pushBack, transform.position.y + 1, 0);
						Debug.Log("push player back: " + pushBack);
					}
				}
				timeBtwShots = startTimeBtwShots;	
			} else {
				timeBtwShots -= Time.deltaTime;
				isAttacking = false;
				anim.SetBool("Throw", false);
			}
			
			if (player.position.x > gameObject.transform.position.x){
				gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
			} else {
				gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
			}
		}
	}


	//DISPLAY the range of enemy's attack when selected in the Editor
    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(transform.position, attackRange);
		Gizmos.DrawWireSphere(transform.position, slashRange);
	}
	
}