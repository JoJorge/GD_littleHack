using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    #region Singleton
    static private Player instance;
    static public Player Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType (typeof(Player)) as Player;
                if (instance == null) {
                    GameObject player = new GameObject ("Player");
                    instance = player.AddComponent<Player> ();
                }
            }
            return instance;
        }
    }
    #endregion

    #region Variables
	private RaycastHit2D[] groundHits;
	protected bool freezed;
	private Collider2D playerCollider;
	[SerializeField] protected float walkSpeed;
	[SerializeField] protected float jumpSpeed;
	[SerializeField] protected float readyJumpTime;
	[SerializeField] protected float maxTimeBeforeStop = 0.1f;
	protected float moveTime;
	protected bool onFloor;
	protected bool readyToJump;
	protected bool jumping;
	protected GameObject floor;
	protected Rigidbody2D rb;
	protected Animator animator;
    #endregion

    #region Accessors
    public bool isStandingOn(Collision2D collision){
		
        float lowestY = playerCollider.bounds.min.y;

		foreach (ContactPoint2D contactPoint in collision.contacts) {
            if (contactPoint.point.y > lowestY) {
                return false;
            }
        }
        return true;
	}
	public bool isFreezed() {
		return freezed;
	}
	public bool isOnFloor() {
		return onFloor;
	}
    #endregion

    #region Behaviors
    public void Awake() {
        if (Player.Instance != this) {
            Destroy (gameObject);
        }
			
		playerCollider = GetComponent<Collider2D> ();
        readyToJump = false;
        jumping = false;
		groundHits = new RaycastHit2D[32];
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
    }

    void Update() {
        Vector2 move = rb.velocity;
		if (readyToJump) {
			InputGameController.Instance.setInput (true);
			readyToJump = false;
			move.y = jumpSpeed;
		}
		if (!onFloor && !jumping && move.y > 0) {
			move.y = 0;
		}
        rb.velocity = move;  

		if (Time.time - moveTime > maxTimeBeforeStop) {
			stop ();
		}
    }

    void OnCollisionEnter2D(Collision2D collision) {
        //when a collision occurs, detect whether the player is touching the top of another collider
        //if so, it means the player is standing on some plane
        if (collision.gameObject.CompareTag ("Floor")) {   
			if (isStandingOn (collision)) {
                floor = collision.gameObject;
                onFloor = true;
                jumping = false;
				animator.SetBool ("jumping", false);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        //detect if player is touching anything in Default layer(floor and plane)
        //if so, it means the player has left the floor

		Collider2D[] colliders = new Collider2D[16];
		int size = playerCollider.GetContacts(colliders);
		for(int i=0;i<size;i++) {
			if (colliders[i].gameObject.tag == "Floor") {
				return;
			}
		}
		onFloor = false;
		floor = null;
    }
    #endregion

    #region Functions
	public IEnumerator jump(){
		if (jumping || !onFloor)
			yield break;
		jumping = true;
		InputGameController.Instance.setInput (false);
		rb.velocity = Vector3.zero;
		animator.SetBool ("jumping", true);
		yield return new WaitForSeconds (readyJumpTime);
		readyToJump = true;

    }
	public void look(int direction){
		Vector2 tmpScale = transform.localScale;
		tmpScale.x = Mathf.Abs(tmpScale.x) * direction;
		transform.localScale = tmpScale;
	}

    public void move(int direction) {               // -1: left, 1: right
		
		// turn player depending on direction
		look(direction);
		if (rb) {
			// move player depending on direction
			Vector2 move = rb.velocity;
			move.x = direction * walkSpeed;
			rb.velocity = move;
		}
        // set player walking animation
		animator.SetBool ("walking", true);
		moveTime = Time.time;
    }
    public void stop() {
        // stop player walking movement
		if (rb) {
			Vector2 move = rb.velocity;
			move.x = 0;
			rb.velocity = move;
		}
        // unset player walking animation
		animator.SetBool ("walking", false);
    }
        
	public virtual void teleport(Vector3 position, bool toFloor) {
		//calculate how the distance between player and floor
		// toFloor: put player on floor after teleport
		// not toFloor: remain the distance
		BoxCollider2D boxCollider = GetComponent<BoxCollider2D> ();
		float height = boxCollider.bounds.size.y / 2 + boxCollider.edgeRadius + 0.01f;
		float oldY = height;
		// distance before teleport
		groundHits = Physics2D.BoxCastAll(transform.position, boxCollider.bounds.size, 0, Vector2.down);
		foreach (RaycastHit2D hit in groundHits) {
			if (hit.collider.tag == "Floor") {
				oldY = hit.distance;
				break;
			}
		}
		// distance after teleport
		groundHits = Physics2D.BoxCastAll(position, boxCollider.bounds.size, 0, Vector2.down);
		foreach (RaycastHit2D hit in groundHits) {
			if (hit.collider.tag == "Floor") {
				if (toFloor) {
					position.y = hit.point.y + height;
				} else {
					position.y = position.y - hit.distance + oldY;
				}
				break;
			}
		}
		// move player
		transform.position = position;
	}
	public int overlapAreaAll(Collider2D[] hits) {
		BoxCollider2D collider = GetComponent<BoxCollider2D>();
		if (collider == null) {
			return 0;
		}
		Vector2 topLeft = (Vector2)transform.position + collider.offset - new Vector2(collider.size.x / 2, collider.size.y / 2);
		Vector2 botRight = (Vector2)transform.position + collider.offset + new Vector2(collider.size.x / 2, collider.size.y / 2);
		return Physics2D.OverlapAreaNonAlloc(topLeft, botRight, hits);
	}
    #endregion    

}
