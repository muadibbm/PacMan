using UnityEngine;

public class LevelManager : MonoBehaviour {
    
    [HideInInspector]
    public PathNode[] graph;
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public Ghost [] ghosts;

    private Dot[] dots;

    private void Awake() {
        this.graph = FindObjectsOfType<PathNode>();
        this.dots = FindObjectsOfType<Dot>();
        this.ghosts = FindObjectsOfType<Ghost>();
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        for (int i = 0; i < this.ghosts.Length; i++)
            this.ghosts[i].SetLevelManager(this);
    }

    private void Update() {
        if(this.AllDotsAreEaten()) {
            this.ReloadLevel();
        }
    }

    public void PacManLost() {
        this.ReloadLevel();
    }

    private void ReloadLevel() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private bool AllDotsAreEaten() {
        if (this.dots == null) return false;
        for(int i = 0; i < this.dots.Length; i++) {
            if (this.dots[i] != null)
                return false;
        }
        return true;
    }
}