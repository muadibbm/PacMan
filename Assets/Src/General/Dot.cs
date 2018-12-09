using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Dot : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D player) {
        Destroy(this.gameObject);
    }
}