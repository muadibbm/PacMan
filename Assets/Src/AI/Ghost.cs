using UnityEngine;

public abstract class Ghost : MonoBehaviour {

    public PathNode start;
    public float speed;
    public float minReachDistance;
    public LevelManager lm;
    
    protected AStar aStar;
    protected Player player;

    private PathNode current;
    private PathNode nextNode;
    private PathNode dest; // for debug

    public void SetLevelManager(LevelManager lm) {
        this.lm = lm;
    }

    private void Awake() {
        this.current = this.start;
        this.aStar = new AStar(this.lm.graph);
        this.player = this.lm.player;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            this.lm.PacManLost();
        }
    }

    protected abstract PathNode GetTargetDestination();

    private void Update() {
        this.UpdateLocomotion(this.GetTargetDestination());
    }

    protected void UpdateLocomotion(PathNode pDest) {
        this.nextNode = this.aStar.FindNextNode(this.current, pDest);
        this.dest = pDest;
        Vector3 motion = Vector3.zero;
        if (this.nextNode == null) {
            motion = (this.player.GetCurrent().transform.position - this.transform.position);
        } else {
            motion = (this.nextNode.transform.position - this.transform.position);
            if (motion.magnitude < this.minReachDistance) {
                this.current = this.nextNode;
            }
        }
        this.transform.position += motion.normalized * Time.deltaTime * this.speed;
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if(this.dest) Gizmos.DrawSphere(this.dest.transform.position, 0.25f);
    }
}