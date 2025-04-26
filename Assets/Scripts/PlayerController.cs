using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private bool m_FacingRight = true;
    public Animator animator;
    public Rigidbody2D player;
    public GameObject bullet;
    public GameObject shootingAnimation;
    public GameObject fireAnimationPlayerDie;
    private float startTime;
    public float shootingRate;
    public float bulletSpeed;
    public float playerSpeed;
    public float jump_multiplier;
    public GameObject fireCollision;
    public GameObject shoulder;
    public Vector2 startTouchPosition;
    private float shootingAngle;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        bullet.SetActive(false);
        shootingAnimation.SetActive(false);
        fireAnimationPlayerDie.SetActive(false);
        fireCollision.SetActive(false);
        shoulder.SetActive(false);
    }

    void Update(){
        if (Input.touchCount > 0 && !GameManager.gamePaused())
        {
            int i;
            bool touchOutUIButtons = false;
            Touch touch = Input.GetTouch(0);

            for(i = 0; i<Input.touchCount;i++){
                touch = Input.GetTouch(i);
                if(!checkOverlap(touch.fingerId)){
                    touchOutUIButtons = true;
                    break;
                }
            }
            if(touchOutUIButtons){
                // Handle finger movements based on TouchPhase
                Debug.Log(touch.phase);
                switch (touch.phase)
                {
                    //When a touch has first been detected, change the message and record the starting position
                    case UnityEngine.TouchPhase.Began:
                        // Record initial touch position.
                        animator.SetBool("isShooting", true);
                        startTouchPosition = touch.position;
                        shoulder.SetActive(true);
                        shootingAngle = 0;
                        break;

                    //Determine if the touch is a moving touch
                    case UnityEngine.TouchPhase.Moved:
                        // Determine direction by comparing the current touch position with the initial one
                        shootingAngle = Vector2.SignedAngle(Vector2.right, touch.position - startTouchPosition);
                        break;

                    case UnityEngine.TouchPhase.Ended:
                        // Report that the touch has ended when it ends
                        animator.SetBool("isShooting", false);
                        break;
                }
            }/*else{
                animator.SetBool("isShooting", false);
            }*/
        }else{
            animator.SetBool("isShooting", false);
            shootingAngle = -1;
        }
        int isMoving = animator.GetInteger("isMoving");
        if (isMoving < 0){
            player.transform.position += Vector3.left * playerSpeed * Time.deltaTime;
        }else if (isMoving > 0) {
            player.transform.position += Vector3.right * playerSpeed * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float tmpShootingAngle;
        if(player.linearVelocity.y > 0){
            animator.SetBool("isJumping", true);
            animator.SetBool("isLanding", false);
        }else if(player.linearVelocity.y == 0){
            animator.SetBool("isJumping", false);
            animator.SetBool("isLanding", false);
        }else if(player.linearVelocity.y < 0){
            animator.SetBool("isLanding", true);
            animator.SetBool("isJumping", false);
        }
        if(animator.GetInteger("isMoving") > 0){
            if(!m_FacingRight){
                Flip();
            }
        }else if(animator.GetInteger("isMoving") < 0){
            if(m_FacingRight){
                Flip();
            }
        }
        if(animator.GetBool("isShooting")){
            shootingAnimation.SetActive(false);
            tmpShootingAngle = shootingAngle;
            if((tmpShootingAngle >= 90 || tmpShootingAngle <= -90)){
                if(m_FacingRight){
                    Flip();
                }
                if(tmpShootingAngle > 0){
                    tmpShootingAngle -= 215;
                }else if(tmpShootingAngle < 0){
                    tmpShootingAngle += 145;
                }
                shoulder.transform.rotation = Quaternion.Euler(0, 0, tmpShootingAngle);
                generateBullets(tmpShootingAngle);
            }else if(tmpShootingAngle < 90 && tmpShootingAngle > -90){
                if(!m_FacingRight){
                    Flip();
                }
                shoulder.transform.rotation = Quaternion.Euler(0, 0, tmpShootingAngle + 35);
                generateBullets(tmpShootingAngle + 35);
            }
        }else{
            shoulder.SetActive(false);
        }
    }

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
        FlipGameObject(bullet);
	}

    private void FlipGameObject(GameObject b)
	{
		// Multiply the bullet's x local scale by -1.
		Vector3 theScale = b.transform.localScale;
		theScale.x *= -1;
		b.transform.localScale = theScale;
	}

    private bool checkOverlap(int id){
        return EventSystem.current.IsPointerOverGameObject(id);
    }

    public void generateBullets(float angle){
        GameObject tmpBullet, tmpShootingAnimation;
        Vector2 tmp;
        if(Time.time - startTime >= 1/shootingRate){
            startTime = Time.time;
            float radians = shootingAngle * Mathf.Deg2Rad;
            tmp = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            tmpBullet = Instantiate(bullet, shoulder.transform.position, Quaternion.Euler(0, 0, angle));
            tmpBullet.SetActive(true);
            tmpBullet.GetComponentInChildren<Rigidbody2D>().linearVelocity = tmp.normalized * bulletSpeed;
            tmpShootingAnimation = Instantiate(shootingAnimation, shoulder.transform, true);
            tmpShootingAnimation.SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "background" || collision.gameObject.tag == "fireball")
        {
            fireAnimationPlayerDie.SetActive(true);
            fireAnimationPlayerDie.transform.position = player.position;
            ContactPoint2D cp = collision.GetContact(0);
            float angle = Vector2.SignedAngle(cp.normal, Vector2.up);
            fireAnimationPlayerDie.transform.Rotate(0, 0, -angle);
            GameManager.PauseGame();
        }
    }

    public void OnMove(InputAction.CallbackContext context) {
        float direction = context.ReadValue<Vector2>().x;
        Debug.Log(direction);
        if(!GameManager.gamePaused()){
            if (context.phase == InputActionPhase.Canceled){
                animator.SetInteger("isMoving", 0);
            }else {
                if(direction < 0){
                    animator.SetInteger("isMoving", -1);
                }else if(direction > 0){
                    animator.SetInteger("isMoving", 1);
                }
            }
        }
    }
    public void OnJump(InputAction.CallbackContext context) {
        if(player.linearVelocity.y == 0 && !GameManager.gamePaused()){
            player.AddForce(Vector2.up * jump_multiplier);
        }
    }
    public void OnAim(InputAction.CallbackContext context){
        shootingAngle = Vector2.SignedAngle(Vector2.right, context.ReadValue<Vector2>());
    }
    public void OnFire(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Canceled){
            animator.SetBool("isShooting", false);
            shoulder.SetActive(false);
        }else{
            animator.SetBool("isShooting", true);
            shoulder.SetActive(true);
        }
    }
}
