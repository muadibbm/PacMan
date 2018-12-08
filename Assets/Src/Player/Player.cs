using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;

    private GameInput gi;
    private CharacterController cc;

    private void Awake() {
        this.gi = Toolbox.Instance.GetTool<GameInput>("GameInput");
        this.cc = this.GetComponent<CharacterController>();
    }

    private void FixedUpdate () {
        // aquire input directions from GameInput
        float xDir = ((this.gi.rightPressed) ? 1f : -1f) + ((this.gi.leftPressed) ? -1f : 1f);
        float yDir = ((this.gi.upPressed) ? 1f : -1f) + ((this.gi.downPressed) ? -1f : 1f);
        if (xDir != 0) { // horizontal direction takes precedence over vertical
            this.UpdateLocomotion(new Vector2(xDir, 0f));
        } else if (yDir != 0) {
            this.UpdateLocomotion(new Vector2(0f, yDir));
        }
    }

    private void UpdateLocomotion(Vector2 dir) {
        // move the player based on given input and speed
        this.cc.Move(this.speed * Time.fixedDeltaTime * new Vector3(dir.x, dir.y));
        // set the direction of player facing towards
        this.transform.right = new Vector3(this.transform.right.x + dir.x, this.transform.right.y + dir.y, this.transform.right.z).normalized;
    }
}
