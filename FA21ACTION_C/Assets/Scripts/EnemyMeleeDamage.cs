using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMeleeDamage : MonoBehaviour {

	public Animator anim;
	public GameObject healthLoot;
	public int maxHealth = 100;
	public int currentHealth;
	
	private float currentAlpha = 1f;
	private Renderer rend;
	
	public bool isBoss = false;

	void Start(){
		currentHealth = maxHealth;
		anim = gameObject.GetComponentInChildren<Animator>();
		rend = GetComponentInChildren<Renderer> ();
	}

	public void Update(){
		
		if (currentHealth <= (maxHealth/2)){
			currentAlpha = 0.75f;
		}
		else if (currentHealth <= (maxHealth/4)){
			currentAlpha = 0.50f;
		}
	}


	public void TakeDamage(int damage){
		currentHealth -= damage;
		anim.SetTrigger ("Hurt");
		StopCoroutine("HitEnemy");
        StartCoroutine("HitEnemy");
		if (currentHealth <= 0){
			Die();
		}
	}

	void Die(){
		Instantiate (healthLoot, transform.position, Quaternion.identity);
		anim.SetBool("isKO", true);
		GetComponent<Collider2D>().enabled = false;
		if (isBoss == false){
			GetComponent<EnemyMoveHit>().enabled = false;
		}
		else {
			GetComponent<EnemyMoveShoot>().enabled = false;
		}
		//this.enabled = false;
		StartCoroutine(Death());
	}


	IEnumerator Death(){
		yield return new WaitForSeconds(5f);
		Debug.Log("You Killed a baddie. You deserve loot!");
		Destroy(gameObject);
	}

	IEnumerator HitEnemy(){
              // color values are R, G, B, and alpha, each divided by 100
              rend.material.color = new Color(2.55f, 1f, 1f, currentAlpha);
              yield return new WaitForSeconds(0.5f);
              //rend.material.color = Color.white;
			  rend.material.color = new Color(2.55f, 2.55f, 2.55f, currentAlpha);
	}


}
