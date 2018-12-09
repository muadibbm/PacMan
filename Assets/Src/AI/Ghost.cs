using UnityEngine;

public class Ghost : MonoBehaviour {

    public PathNode start;
    public float speed;
    public float minReachDistance;

    private Player player;
    private AStar aStar;
    private PathNode current;

    private void Awake() {
        this.current = this.start;
        this.aStar = new AStar(Toolbox.Instance.GetTool<GameManager>("GameManager").graph);
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void FixedUpdate() {
        PathNode nextNode = this.aStar.FindNextNode(this.current, this.player.GetCurrent());
        Vector3 motion;
        if (nextNode == null) {
            motion = (player.transform.position - this.transform.position);
        } else {
            motion = (nextNode.transform.position - this.transform.position);
            if (motion.magnitude < this.minReachDistance) {
                this.current = nextNode;
            }
        }
        this.transform.position += motion.normalized * Time.fixedDeltaTime * this.speed;
    }
}