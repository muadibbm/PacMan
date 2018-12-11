using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Teleport : MonoBehaviour {
    
    public PathNode target;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            Player player = other.GetComponent<Player>();
            if(!player.alreadyTeleported) {
                player.Teleport(this.target);
                player.alreadyTeleported = true;
            }
        } else { // Ghost
            Ghost ghost = other.GetComponent<Ghost>();
            if (!ghost.alreadyTeleported) {
                ghost.Teleport(this.target);
                ghost.alreadyTeleported = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            Player player = other.GetComponent<Player>();
            if(player.GetCurrent() != target) {
                other.GetComponent<Player>().alreadyTeleported = false;
            }
        } else { // Ghost
            Ghost ghost = other.GetComponent<Ghost>();
            if (ghost.GetCurrent() != target) {
                ghost.alreadyTeleported = false;
            }
        }
    }
}
