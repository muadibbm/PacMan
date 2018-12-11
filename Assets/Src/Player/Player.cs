using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public float minReachDistance;
    public PathNode start;

    [HideInInspector]
    public bool alreadyTeleported;

    private GameInput gi;
    private PathNode current;
    private PathNode target;
    private PathNode prevNode;
    private bool xMotion;
    private bool yMotion;

    private void Awake() {
        this.gi = Toolbox.Instance.GetTool<GameInput>("GameInput");
        this.current = this.start;
    }

    private void Update () {
        // aquire input directions from GameInput
        float xDir = ((this.gi.rightPressed) ? 1f : -1f) + ((this.gi.leftPressed) ? -1f : 1f);
        float yDir = ((this.gi.upPressed) ? 1f : -1f) + ((this.gi.downPressed) ? -1f : 1f);
        this.UpdateLocomotion(new Vector2(xDir, yDir));
    }

    public void Teleport(PathNode target) {
        this.prevNode = this.current;
        this.current = target;
        this.transform.position = this.current.transform.position;
    }

    public PathNode GetPrevNode() {
        return this.prevNode;
    }

    public PathNode GetCurrent() {
        return this.current;
    }

    private void UpdateLocomotion(Vector2 dir) {
        // horizontal direction takes precedence over vertical
        if(dir.x != 0) {
            if (this.yMotion) return;
            this.xMotion = true;
            this.target = (dir.x > 0) ? this.current.right : this.current.left;
        } else if(dir.y != 0) {
            if (this.xMotion) return;
            this.yMotion = true;
            this.target = (dir.y > 0) ? this.current.up : this.current.down;
        }
        if(this.target && dir.magnitude != 0f) {
            // move towards target and check if it is reached
            Vector3 motion = (this.target.transform.position - this.transform.position);
            if (motion.magnitude <= this.minReachDistance) {
                this.current = this.target;
                this.target = null;
            }
            if ((this.transform.position - this.current.transform.position).magnitude <= this.minReachDistance) {
                this.xMotion = this.yMotion = false;
            }
            this.transform.position += motion.normalized * this.speed * Time.deltaTime;
            // set the direction of player facing towards
            this.transform.right = new Vector3(this.transform.right.x + ((this.xMotion) ? dir.x : 0f), 
                                               this.transform.right.y + ((this.yMotion) ? dir.y : 0f), 
                                               this.transform.right.z).normalized;
        }
    }
}