using UnityEngine;

public class PathNode : MonoBehaviour {

    public PathNode[] neighbors;

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(this.transform.position, 0.1f);
        if(this.neighbors != null)
            for (int i = 0; i < this.neighbors.Length; i++)
                Gizmos.DrawLine(this.transform.position, this.neighbors[i].transform.position);
    }

    public PathNode Right() {
        for(int i = 0; i < this.neighbors.Length; i++) {
            if (this.neighbors[i].transform.position.x > this.transform.position.x)
                return neighbors[i];
        }
        return null;
    }

    public PathNode Left() {
        for (int i = 0; i < this.neighbors.Length; i++) {
            if (this.neighbors[i].transform.position.x < this.transform.position.x)
                return neighbors[i];
        }
        return null;
    }

    public PathNode Up() {
        for (int i = 0; i < this.neighbors.Length; i++) {
            if (this.neighbors[i].transform.position.y > this.transform.position.y)
                return neighbors[i];
        }
        return null;
    }

    public PathNode Down() {
        for (int i = 0; i < this.neighbors.Length; i++) {
            if (this.neighbors[i].transform.position.y < this.transform.position.y)
                return neighbors[i];
        }
        return null;
    }
}