﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody rigidBody1;
	private Animator animator1;
	private Rigidbody rigidBody2;
	private Animator animator2;
    private RageManager ragemanager;

    public float maxSpeed = 5.0f;
    private bool facingRight1 = false;
	private bool facingRight2 = false;

	private GameObject player1;
	private GameObject player2;

	private SpriteRenderer sprite1;
	private SpriteRenderer sprite2;
    // Use this for initialization
    void Start () {
		player1 = GameObject.Find ("Player1");
		player2 = GameObject.Find ("Player2");

        rigidBody1 = player1.GetComponent<Rigidbody>();
		animator1 = player1.GetComponentInChildren<Animator> ();
		rigidBody2 = player2.GetComponent<Rigidbody>();
		animator2 = player2.GetComponentInChildren<Animator> ();

		sprite1 = player1.GetComponentInChildren<SpriteRenderer> ();
		sprite2 = player2.GetComponentInChildren<SpriteRenderer> ();

        ragemanager = GameObject.Find("GlobalScript").GetComponent<RageManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        //Valeur entre -1 et 1 selon intentsité de frappe sur l'axe horizontal
        float h1 = Input.GetAxis("Horizontal_P1");
		float v1 = Input.GetAxis ("Vertical_P1");
		float h2 = Input.GetAxis("Horizontal_P2");
		float v2 = Input.GetAxis ("Vertical_P2");
        //Fonction responsable du mouvement

		MovePlayer (h1, v1, 1);
		MovePlayer (h2, v2, 2);

		float distance = Vector3.Distance (player1.transform.position, player2.transform.position);
		float direction1 = Vector3.Dot ((player2.transform.position - player1.transform.position).normalized, new Vector3(h1, 0, v1).normalized);
		float direction2 = Vector3.Dot ((player1.transform.position - player2.transform.position).normalized, new Vector3(h2, 0, v2).normalized);

		if (Input.GetAxis ("Fire1_P1") > 0) {
			Fire ();
			if (distance < 3.0f && Mathf.Abs (direction1) > 0.9f) {

                ragemanager.AddRage(10f, 2);
				Debug.Log ("Hit !");
			}
		}
		if (Input.GetAxis ("Fire1_P2") > 0) {
			Fire ();
			if (distance < 3.0f && Mathf.Abs (direction2) > 0.9f)
            {
                ragemanager.AddRage(10f, 1);
                Debug.Log ("Hit !");
			}
		}
    }

	private void MovePlayer( float h, float v, int player)
	{
		if (player == 1) {
			rigidBody1.velocity = new Vector3 (h * maxSpeed, 0, v * maxSpeed);
			SetBool_H_V (h, v, 1);
			//Si on veut utiliser un miroir avec les sprites il faut ces lignes de code
			if ((h > 0 && facingRight1) || (h < 0 && !facingRight1)) {
				Flip (1);
			}
		} else {
			rigidBody2.velocity = new Vector3 (h * maxSpeed, 0,  v * maxSpeed);
			SetBool_H_V (h, v, 2);
			//Si on veut utiliser un miroir avec les sprites il faut ces lignes de code
			if ((h > 0 && facingRight2) || (h < 0 && !facingRight2)) {
				Flip (2);
			}
		}
	}

    private void PutBool4Directions_False(int player) {
        if (player == 1) {
            animator1.SetBool("GoUp", false);
            animator1.SetBool("GoDown", false);
            animator1.SetBool("GoLeft", false);
            animator1.SetBool("GoRight", false);
        } else {
            animator2.SetBool("GoUp", false);
            animator2.SetBool("GoDown", false);
            animator2.SetBool("GoLeft", false);
            animator2.SetBool("GoRight", false);
        }
    }

	private void SetBool_V(float h, float v, int player)
	{
		if (player == 1) {
			if (v > 0) {
				animator1.SetBool ("GoUp", true);
				animator1.SetBool ("GoDown", false);
                if (h > 0) {
                    animator1.SetBool("GoUpRight", true);
                    animator1.SetBool("GoUpLeft", false);
                    PutBool4Directions_False(1);
                } else if (h < 0) {
                    animator1.SetBool("GoUpLeft", true);
                    animator1.SetBool("GoUpRight", false);
                    PutBool4Directions_False(1);
                } else {
                    animator1.SetBool("GoUpLeft", false);
                    animator1.SetBool("GoUpRight", false);
                }
			} else if (v < 0) {
				animator1.SetBool ("GoUp", false);
				animator1.SetBool ("GoDown", true);
                if (h > 0) {
                    animator1.SetBool("GoDownRight", true);
                    animator1.SetBool("GoDownLeft", false);
                    PutBool4Directions_False(1);
                } else if (h < 0) {
                    animator1.SetBool("GoDownLeft", true);
                    animator1.SetBool("GoDownRight", false);
                    PutBool4Directions_False(1);
                } else {
                    animator1.SetBool("GoDownLeft", false);
                    animator1.SetBool("GoDownRight", false);
                }
            } else {
				animator1.SetBool ("GoUp", false);
                animator1.SetBool("GoDown", false);
                animator1.SetBool("GoDownLeft", false);
                animator1.SetBool("GoDownRight", false);
                animator1.SetBool("GoUpLeft", false);
                animator1.SetBool("GoUpRight", false);
            }
		} else {
            if (v > 0)
            {
                animator2.SetBool("GoUp", true);
                animator2.SetBool("GoDown", false);
                if (h > 0)
                {
                    animator2.SetBool("GoUpRight", true);
                    animator2.SetBool("GoUpLeft", false);
                    PutBool4Directions_False(2);
                }
                else if (h < 0)
                {
                    animator2.SetBool("GoUpLeft", true);
                    animator2.SetBool("GoUpRight", false);
                    PutBool4Directions_False(2);
                }
                else
                {
                    animator2.SetBool("GoUpLeft", false);
                    animator2.SetBool("GoUpRight", false);
                }
            }
            else if (v < 0)
            {
                animator2.SetBool("GoUp", false);
                animator2.SetBool("GoDown", true);
                if (h > 0)
                {
                    animator2.SetBool("GoDownRight", true);
                    animator2.SetBool("GoDownLeft", false);
                    PutBool4Directions_False(2);
                }
                else if (h < 0)
                {
                    animator2.SetBool("GoDownLeft", true);
                    animator2.SetBool("GoDownRight", false);
                    PutBool4Directions_False(2);
                }
                else
                {
                    animator2.SetBool("GoDownLeft", false);
                    animator2.SetBool("GoDownRight", false);
                }
            }
            else
            {
                animator2.SetBool("GoUp", false);
                animator2.SetBool("GoDown", false);
                animator2.SetBool("GoDownLeft", false);
                animator2.SetBool("GoDownRight", false);
                animator2.SetBool("GoUpLeft", false);
                animator2.SetBool("GoUpRight", false);
            }
        }
	}

	private void SetBool_H_V(float h, float v, int player)
	{
		if (player == 1) {
			if (h > 0) {
				animator1.SetBool ("GoLeft", false);
				animator1.SetBool ("GoRight", true);
				SetBool_V (h, v, 1);
			} else if (h < 0) {
				animator1.SetBool ("GoLeft", true);
				animator1.SetBool ("GoRight", false);
				SetBool_V (h, v, 1);
			} else {
				animator1.SetBool ("GoLeft", false);
				animator1.SetBool ("GoRight", false);
				SetBool_V (h, v, 1);
			}
            if (h == 0 && v == 0) {
                animator1.SetBool("Move", false);
            } else {
                animator1.SetBool("Move", true);
            }
		} else {
			if (h > 0) {
				animator2.SetBool ("GoLeft", false);
				animator2.SetBool ("GoRight", true);
				SetBool_V (h, v, 2);
			} else if (h < 0) {
				animator2.SetBool ("GoLeft", true);
				animator2.SetBool ("GoRight", false);
				SetBool_V (h, v, 2);
			} else {
				animator2.SetBool ("GoLeft", false);
				animator2.SetBool ("GoRight", false);
				SetBool_V (h, v, 2);
			}
            if (h == 0 && v == 0) {
                animator2.SetBool("Move", false);
            } else {
                animator2.SetBool("Move", true);
            }
        }
	}

	private void Flip(int player)
	{
		if (player == 1) {
			sprite1.flipX = !sprite1.flipX;
			facingRight1 = !facingRight1;
		} else {
			sprite2.flipX = !sprite2.flipX;
			facingRight2 = !facingRight2;
		}
	}

	private void Fire(){
	}
}