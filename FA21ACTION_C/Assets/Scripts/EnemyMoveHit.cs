using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMoveHit : MonoBehaviour {

       public Animator anim;
       public float speed = 4f;
       private Transform target;
       public int damage = 10;

       public int EnemyLives = 3;
       private Renderer rend;
       private GameHandler gameHandler;

       public float attackRange = 10;

       void Start () {
              rend = GetComponentInChildren<Renderer> ();

              if (GameObject.FindGameObjectWithTag ("Player") != null) {
                     target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
              }

              if (GameObject.FindWithTag ("GameHandler") != null) {
                  gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler> ();
              }
       }

       void Update () {
              float DistToPlayer = Vector3.Distance(transform.position, target.position);

              if ((target != null) && (DistToPlayer <= attackRange)){
				  Vector3 offsetAttack = new Vector3 (0.5f, 0.5f, 0f);
                     transform.position = Vector2.MoveTowards (transform.position, target.position + offsetAttack, speed * Time.deltaTime);
              }
       }

       public void OnTriggerEnter2D(Collider2D collision){
              if (collision.gameObject.tag == "Player") {
                     anim.SetBool("Attack", true);
                     gameHandler.playerGetHit(damage);
                     //rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
                     //StartCoroutine(HitEnemy());
              }
       }

       public void OnTriggerExit2D(Collider2D collision){
              if (collision.gameObject.tag == "Player") {
                     anim.SetBool("Attack", false);
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