using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour {
	
	public static HeroRabit lastRabit = null;

	public float speed = 3;
	Rigidbody2D myBody = null;
	Animator animator = null;
	SpriteRenderer spriteRenderer;
	//Transform myTransform;
	Transform heroParent = null;
	bool isGrounded = false;
	bool JumpActive = false;
	bool isBig = false;
	bool isSuperRabit = false;
	float JumpTime = 0f;
	public float MaxJumpTime = 2f;
	public float JumpSpeed = 2f;
	float MaxSuperPowerTime = 4f;
	float SuperPowerTimer = 0;

	public bool IsBig { get{ return isBig;} private set{ isBig = value;}}
	public bool SuperRabit{ get { return isSuperRabit; } set{isSuperRabit = value; }}

	void Awake(){
		lastRabit = this;
	}

	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		this.heroParent = this.transform.parent;
		LevelController.current.setStartPosition (transform.position);
		this.SuperPowerTimer = this.MaxSuperPowerTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.SuperRabit) {
			SuperPowerTimer -= Time.deltaTime;
			if (this.SuperPowerTimer >= 0) {
				spriteRenderer.color = Color.red;
			} else {
				spriteRenderer.color = Color.white;
				this.SuperPowerTimer = this.MaxSuperPowerTime;
				this.SuperRabit = false;
			}
		}
	}

	void FixedUpdate()
	{
		float value = Input.GetAxis("Horizontal");
		if (Mathf.Abs (value) > 0) {
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		}
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		if(value < 0){
			sr.flipX = true;
		}
		else if(value > 0)
		{
			sr.flipX = false;
		}
		if (Mathf.Abs (value) > 0) {
			animator.SetBool ("run", true);
		} else {
			animator.SetBool ("run", false);
		}

		checkGrounding ();
		checkJump ();
	}

	void checkGrounding(){
		Vector3 from = this.transform.position + Vector3.up * 0.3f;
		Vector3 to = this.transform.position + Vector3.down * 0.1f;
		int layer_id = 1 << LayerMask.NameToLayer ("Ground");
		RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
		if(hit) {
			isGrounded = true;
			if(hit.transform != null
				&& hit.transform.GetComponent<MovingPlatform>() != null){
				//Приліпаємо до платформи
				SetNewParent(this.transform, hit.transform);
			}
		} else {
			isGrounded = false;
			SetNewParent(this.transform, this.heroParent);
		}
		//Debug.DrawLine (from, to, Color.red);
	}

	void checkJump(){
		if(Input.GetButtonDown("Jump") && isGrounded) {
			this.JumpActive = true;
		}
		if(this.JumpActive) {
			//Якщо кнопку ще тримають
			if(Input.GetButton("Jump")) {
				this.JumpTime += Time.deltaTime;
				if (this.JumpTime < this.MaxJumpTime) {
					Vector2 vel = myBody.velocity;
					vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
					myBody.velocity = vel;
				}
			} else {
				this.JumpActive = false;
				this.JumpTime = 0;
			}
		}
		Animator animator = GetComponent<Animator> ();
		if(this.isGrounded) {
			animator.SetBool ("jump", false);
		} else {
			animator.SetBool ("jump", true);
		}
	}

	static void SetNewParent(Transform obj, Transform new_parent) {
		if(obj.transform.parent != new_parent) {
			Vector3 pos = obj.transform.position;
			obj.transform.parent = new_parent;
			obj.transform.position = pos;
		}
	}

	public void BecomeBigger(){
		this.transform.localScale = new Vector3 (2, 2, 1);
		this.isBig = true;
	}

	public void BecomeSmaller(){
		this.transform.localScale = new Vector3 (1, 1, 1);
		this.isBig = false;
	}
		
	public void DieWithAnimation(){
		animator.SetBool("death",true);
		IEnumerator corountine = WaitForDeathAnimation ();
		StartCoroutine(corountine);
	}

	IEnumerator WaitForDeathAnimation()
	{
		yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
		animator.SetBool("death",false);
		yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
		LevelController.current.OnRabitDeath (this);
	}
}

