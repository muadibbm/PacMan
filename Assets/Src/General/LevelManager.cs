using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public int scoreDot;
    public int scoreGhost;

    public TextMesh scoreText;

    [HideInInspector]
    public PathNode[] graph;
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public Ghost [] ghosts;

    private int score;
    private Dot[] dots;
    private Fruit[] fruits;
    private Coroutine escapeRoutine;

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
        for (int i = 0; i < this.dots.Length; i++)
            this.dots[i].SetLevelManager(this);
    }

    private void Update() {
        this.scoreText.text = "Score   " + this.score.ToString();
        if (this.AllDotsAreEaten()) {
            this.ReloadLevel();
        }
    }

    public void ScoreDot() {
        this.score += this.scoreDot;
    }

    public void ScoreGhost() {
        this.score += this.scoreGhost;
    }

    public void PacManLost() {
        this.ReloadLevel();
    }

    public void TriggerEscapeMode(float duration) {
        if (this.escapeRoutine != null) this.StopCoroutine(this.escapeRoutine);
        this.escapeRoutine = this.StartCoroutine(this.EscapeMode(duration));
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