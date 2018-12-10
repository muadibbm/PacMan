using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    [HideInInspector]
    public PathNode[] graph;
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public Ghost [] ghosts;

    private Dot[] dots;
    private Fruit[] fruits;

    private void Awake() {
        this.graph = FindObjectsOfType<PathNode>();
        this.dots = FindObjectsOfType<Dot>();
        this.ghosts = FindObjectsOfType<Ghost>();
        this.fruits = FindObjectsOfType<Fruit>();
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        for (int i = 0; i < this.ghosts.Length; i++)
            this.ghosts[i].SetLevelManager(this);
        for (int i = 0; i < this.fruits.Length; i++)
            this.fruits[i].SetLevelManager(this);
    }

    private void Update() {
        if(this.AllDotsAreEaten()) {
            this.ReloadLevel();
        }
    }

    public void PacManLost() {
        this.ReloadLevel();
    }

    public void TriggerEscapeMode(float duration) {
        this.StartCoroutine(this.EscapeMode(duration));
    }

    private IEnumerator EscapeMode(float duration) {
        for(int i = 0; i < this.ghosts.Length; i++) {
            this.ghosts[i].escape = true;
        }
        yield return new WaitForSeconds(duration);
        for (int i = 0; i < this.ghosts.Length; i++) {
            this.ghosts[i].escape = false;
        }
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