using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMeleeDamage : MonoBehaviour {

	public Animator anim;
	public GameObject healthLoot;
	public int maxHealth = 100;
	public int currentHealth;
	
	public bool isBoss = false;

	void Start(){
		currentHealth = maxHealth;
		anim = gameObject.GetComponentInChildren<Animator>();
	}

	public void TakeDamage(int damage){
		currentHealth -= damage;
		anim.SetTrigger ("Hurt");
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

}
