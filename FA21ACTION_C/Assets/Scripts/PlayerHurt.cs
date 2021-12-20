using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerHurt: MonoBehaviour{

      public Animator anim;
      public Rigidbody2D rb2D;

      void Start(){
           anim = gameObject.GetComponentInChildren<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();           
      }

      public void playerHit(){
            anim.SetTrigger ("Hurt");
      }

      public void playerDead(){
            rb2D.isKinematic = true;
            anim.SetBool ("KO", true);
      }
}