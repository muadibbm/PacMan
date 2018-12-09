using UnityEngine;

public class GameManager : MonoBehaviour {

    public PathNode[] graph;

    private Dot[] dots;

    private void Awake() {
        this.graph = FindObjectsOfType<PathNode>();
        this.dots = FindObjectsOfType<Dot>();
    }

    private void Update() {
        // TODO : check for win condition
    }
}