using UnityEngine;

public class PathNode : MonoBehaviour {

    public PathNode[] neighbors;
    public enum Type { General, Player, NPC }
    public Type type; // determines which type of agent can use this pathnode

#if UNITY_EDITOR
    public Color gizmosColor;
#endif

    public PathNode right {
        get {
            for (int i = 0; i < this.neighbors.Length; i++) {
                if ((int)this.neighbors[i].transform.position.x > (int)this.transform.position.x)
                    return neighbors[i];
            }
            return this;
        }
    }

    public PathNode left {
        get {
            for (int i = 0; i < this.neighbors.Length; i++) {
                if ((int)this.neighbors[i].transform.position.x < (int)this.transform.position.x)
                    return neighbors[i];
            }
            return this;
        }
    }

    public PathNode up {
        get {
            for (int i = 0; i < this.neighbors.Length; i++) {
                if ((int)this.neighbors[i].transform.position.y > (int)this.transform.position.y)
                    return neighbors[i];
            }
            return this;
        }
    }

    public PathNode down {
        get {
            for (int i = 0; i < this.neighbors.Length; i++) {
                if ((int)this.neighbors[i].transform.position.y < (int)this.transform.position.y) {
                    return neighbors[i];
                }
            }
            return this;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.color = this.gizmosColor;
        Gizmos.DrawSphere(this.transform.position, 0.1f);
        if (this.neighbors != null)
            for (int i = 0; i < this.neighbors.Length; i++)
                if(this.neighbors[i].IsNeighbor(this))
                    Gizmos.DrawLine(this.transform.position, this.neighbors[i].transform.position);
    }

    public bool IsNeighbor(PathNode pathNode) {
        for (int i = 0; i < this.neighbors.Length; i++)
            if (this.neighbors[i] == pathNode)
                return true;
        return false;
    }
#endif
}