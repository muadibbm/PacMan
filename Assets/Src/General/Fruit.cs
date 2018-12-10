using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Fruit : MonoBehaviour {

    public float duration;

    private LevelManager lm;

    private void OnTriggerEnter2D(Collider2D player) {
        this.lm.TriggerEscapeMode(this.duration);
        Destroy(this.gameObject);
    }

    public void SetLevelManager(LevelManager lm) {
        this.lm = lm;
    }
}