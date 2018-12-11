using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Dot : MonoBehaviour {

    private LevelManager lm;

    private void OnTriggerEnter2D(Collider2D player) {
        this.lm.ScoreDot();
        Destroy(this.gameObject);
    }

    public void SetLevelManager(LevelManager lm) {
        this.lm = lm;
    }
}