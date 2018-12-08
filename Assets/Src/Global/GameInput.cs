using UnityEngine;

public class GameInput : MonoBehaviour{

	public bool upPressed;
	public bool downPressed;
	public bool leftPressed;
	public bool rightPressed;

    private void Update() {
        this.upPressed = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        this.downPressed = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        this.leftPressed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        this.rightPressed = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
    }
}