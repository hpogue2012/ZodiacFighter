using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fighter : MonoBehaviour
{

	[System.Serializable]
	public class Attack
	{
		public int damage;
		public float prep;
		public GameObject projectile;
		public float recovery;
		public float reach;
		public float armor;
		public float armorBreak;
		public float knockback;
	}
	
	public float health;
	public float armor;
	public float speed;
	public Attack lightAttack;
	public Attack mediumAttack;
	public Attack heavyAttack;
	public float cooldown;
	public float jumpPower;
	public float direction;
	public float jumping;
	private bool isGrounded = true;
	public int playID;
	public int stars;
	public int starMax;
	public GameObject SpawnPoint;
	public float exJump;
	public float negAcc;
	public float maxHealth;
	public float dodgeCool;

	public bool blocking;
	public bool attacking;

	// Use this for initialization
	void Start ()
	{

		health = maxHealth;
		dodgeCool = 0;
		cooldown = 0;
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (health <= 0) {
		
			StartCoroutine ("Death");

		} else {
			if(blocking == true){

				GetComponentInChildren<Animator>().SetBool("Block", true);
				if(direction>.3 && dodgeCool<=0){
			
					StartCoroutine("Dodge", 1);

				}else if(direction<-.3 && dodgeCool<=0){

					StartCoroutine("Dodge", -1);

				}
			
			}
			if(blocking==false && attacking == false){
				GetComponentInChildren<Animator>().SetBool("Block", false);
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (direction * speed * 0.25f, GetComponent<Rigidbody2D> ().velocity.y);
				if(Time.timeScale>0){transform.LookAt (transform.position + new Vector3 (0, 0, direction));}
				if (jumping > 0) {
					if (isGrounded) {
						
						isGrounded = false;
						GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpPower));
						
					} else if (!isGrounded) {
						
						GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, exJump));
						if (exJump > 0)
							exJump -= negAcc;
						
					}
				}
			}
			if (cooldown > 0)
				cooldown -= Time.deltaTime;
			if(dodgeCool > 0)
				dodgeCool -= Time.deltaTime;
			Debug.DrawLine (transform.position, transform.position + transform.right * 1);
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		isGrounded = true;
		exJump = 1200;

//		if (coll.gameObject.tag == "Player") {
//		
//			coll.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
//			coll.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (coll.gameObject.GetComponent<Fighter>().direction * coll.gameObject.GetComponent<Fighter>().speed * 0.25f*(-1)+50000, coll.gameObject.GetComponent<Rigidbody2D> ().velocity.y);
//			coll.transform.position = new Vector2(transform.position.x+(coll.GetComponent<Fighter>().direction
//		
//		}

	}

//	void OnCollisionExit2D(Collision2D coll){
//	
//		if (coll.gameObject.tag == "Player") {
//			
//			coll.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
//
//		}
//	
//	}
	
	IEnumerator PerformAttack (Attack attack)
	{
		float tempSpeed = speed;
		float waitedTime;
		bool armorBroken = false;
		print ("Whoosh from " + gameObject.name);
		armor = attack.armor;
		attacking = true;
		cooldown = attack.recovery + attack.prep;
		speed = 0;
		for (waitedTime=0; waitedTime<attack.prep; waitedTime+=.1f) {
		
			if(armor<=0){
				armorBroken = true;
				break;
			}
			yield return new WaitForSeconds(.1f);
		
		}
		if (!armorBroken) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.right, attack.reach);
			if (hit)
				Debug.DrawLine (transform.position, hit.transform.position, Color.blue, 3);
			if (hit.collider != null && hit.collider != gameObject.GetComponent<Collider2D> ()) {
				print ("Pow from " + gameObject.name);
				hit.collider.SendMessage ("Damage", attack.damage);
				hit.collider.SendMessage ("ArmorDamage", attack.armorBreak);
				hit.collider.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (direction * attack.knockback, 0), ForceMode2D.Impulse);
			}
			if (attack.projectile)
				Instantiate (attack.projectile, transform.position, transform.rotation);
		}
		print ("Attack ended");
		armor = 0;
		speed = tempSpeed;
		attacking = false;
	}
	
	public void LightAttack ()
	{
		if (cooldown <= 0) {
			StartCoroutine (PerformAttack (lightAttack));
			this.GetComponentInChildren<Animator> ().SetTrigger ("Light");
		}
	}
	
	public void MediumAttack ()
	{
		if (cooldown <= 0) {
			StartCoroutine (PerformAttack (mediumAttack));
			this.GetComponentInChildren<Animator> ().SetTrigger ("Medium");
		}
	}
	
	public void HeavyAttack ()
	{
		if (cooldown <= 0) {
			StartCoroutine (PerformAttack (heavyAttack));
			this.GetComponentInChildren<Animator> ().SetTrigger ("Heavy");
		}
	}
	
	void Damage (float amount)
	{
		if (blocking == false) {
			health -= amount;
		}
	}

	void ArmorDamage (float amount)
	{
		armor -= amount;
	}

	IEnumerator Death(){

		yield return new WaitForSeconds(.1f);
		List<GameObject> children = new List<GameObject>();
		foreach (Transform child in transform) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));
		Destroy (this);

	}

	public void Ringout(){

		if (health > (2 * (maxHealth / 10))) {
		
			health -= (maxHealth / 10);
		
		} else {
		
			health = maxHealth/10;
		
		}
		this.transform.position = SpawnPoint.transform.position;

	}

	IEnumerator Dodge(float dir){

		dodgeCool = 1f;
		this.GetComponentInChildren<Animator> ().SetTrigger ("Dodge");
		this.gameObject.layer = LayerMask.NameToLayer("Dodge");
		for (int i=0; i<15; i++) {
			this.transform.position = new Vector2(this.transform.position.x + (dir * .5f), this.transform.position.y);
			yield return new WaitForSeconds (.01f);
		}
		this.gameObject.layer = LayerMask.NameToLayer("Player");

	}

}
