using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMoveHit : MonoBehaviour {

       public Animator anim;
       public float speed = 4f;
       private Transform target;
       public int damage = 10;
       public float damageRate = 1f;
       public bool canAttack = true;
       public float attackTimer = 0;

        public Vector3 offsetAttack ;

       public int EnemyLives = 3;
       private Renderer rend;
       private GameHandler gameHandler;

       public float attackRange = 10;


       public bool isCatboy = false;

       void Start () {
              rend = GetComponentInChildren<Renderer> ();
              anim = GetComponentInChildren<Animator> ();

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
                  //anim.SetBool("Walk", true);
              }
              else{
                //anim.SetBool("Walk", false);
              }
       }

       void FixedUpdate () {
         if(canAttack) {
           attackTimer += 0.01f;
          if(attackTimer >= damageRate ) {
            gameHandler.playerGetHit(damage);
            Debug.Log("I'm Attacking!");
            attackTimer = 0;
          }
         }
       }

       public void OnTriggerStay2D(Collider2D collision){
              if (collision.gameObject.tag == "Player") {
                     anim.SetBool("Attack", true);
                     canAttack = true;
                     //rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
                     //StartCoroutine(HitEnemy());
              }
       }

       public void OnTriggerExit2D(Collider2D collision){
              if (collision.gameObject.tag == "Player") {
                     anim.SetBool("Attack", false);
                     Debug.Log("Attack stopped");
                     canAttack = false;
              }
       }

       IEnumerator HitEnemy(){
              yield return new WaitForSeconds(0.5f);
              rend.material.color = Color.white;
       }

       //DISPLAY the range of enemy's attack when selected in the Editor
       void OnDrawGizmosSelected(){
              Gizmos.DrawWireSphere(transform.position, attackRange);
       }
}
