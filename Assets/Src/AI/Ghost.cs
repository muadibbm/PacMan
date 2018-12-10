using UnityEngine;

public abstract class Ghost : MonoBehaviour {

    public PathNode start;
    public float chaseSpeed;
    public float escapeSpeed;
    public float minReachDistance;
    public float respawnDuration;

    [HideInInspector]
    public bool escape;
    
    protected AStar aStar;
    protected Player player;

    private PathNode current;
    private PathNode nextNode;
    private PathNode dest; // for debug
    private LevelManager lm;
    private Animator animator;
    private SpriteRenderer rend;
    private bool dead;
    private float elapsedTime;

    public void SetLevelManager(LevelManager lm) {
        this.lm = lm;
    }

    private void Awake() {
        this.current = this.start;
        this.aStar = new AStar(this.lm.graph);
        this.player = this.lm.player;
        this.animator = this.GetComponent<Animator>();
        this.rend = this.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            if(this.escape) {
                this.current = this.start;
                this.transform.position = this.start.transform.position;
                this.escape = false;
                this.dead = true;
            }
            else {
                this.lm.PacManLost();
            }
        }
    }

    protected abstract PathNode GetTargetDestination();

    private void Update() {
        this.rend.enabled = !this.dead;
        if (this.dead) {
            this.elapsedTime += Time.deltaTime;
            if(this.elapsedTime >= this.respawnDuration) {
                this.dead = false;
                this.elapsedTime = 0f;
            }
            return;
        }
        this.animator.SetBool("Escape", this.escape);
        if(this.escape) {
            this.dest = this.aStar.GetClosestNode((this.transform.position - this.player.transform.position).normalized * 6f);
        } else {
            this.dest = this.GetTargetDestination();
        }
        this.UpdateLocomotion();
    }

    protected void UpdateLocomotion() {
        this.nextNode = this.aStar.FindNextNode(this.current, this.dest);
        Vector3 motion = Vector3.zero;
        if (this.nextNode == null) {
            motion = (this.player.GetCurrent().transform.position - this.transform.position);
        } else {
            motion = (this.nextNode.transform.position - this.transform.position);
            if (motion.magnitude < this.minReachDistance) {
                this.current = this.nextNode;
            }
        }
        this.transform.position += motion.normalized * Time.deltaTime * ((this.escape) ? this.escapeSpeed : this.chaseSpeed);
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if(this.dest) Gizmos.DrawSphere(this.dest.transform.position, 0.25f);
    }
}