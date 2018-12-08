using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public PathNode start;

    private GameInput gi;
    private PathNode current;

    private void Awake() {
        this.gi = Toolbox.Instance.GetTool<GameInput>("GameInput");
        this.current = this.start;
    }

    private void FixedUpdate () {
        // aquire input directions from GameInput
        float xDir = ((this.gi.rightPressed) ? 1f : -1f) + ((this.gi.leftPressed) ? -1f : 1f);
        float yDir = ((this.gi.upPressed) ? 1f : -1f) + ((this.gi.downPressed) ? -1f : 1f);
        this.UpdateLocomotion(new Vector2(xDir, yDir));
    }

    private void UpdateLocomotion(Vector2 dir) {
        PathNode target = null;
        // horizontal direction takes precedence over vertical
        if (dir.x > 0) {
            target = this.current.Right();
        } else if (dir.x < 0) {
            target = this.current.Left();
        } else if (dir.y > 0) {
            target = this.current.Up();
        } else if (dir.y < 0) {
            target = this.current.Down();
        }
        if(target) {
            // move towards target and check if it is reached
            Vector3 motion = (target.transform.position - this.transform.position);
            this.transform.position += Time.fixedDeltaTime * motion.normalized;
            if (motion.magnitude <= Mathf.Epsilon) {
                this.current = target;
            }
        }
        // set the direction of player facing towards
        this.transform.right = new Vector3(this.transform.right.x + dir.x, this.transform.right.y + dir.y, this.transform.right.z).normalized;
    }
}
