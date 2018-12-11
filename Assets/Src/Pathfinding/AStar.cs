using System.Collections.Generic;

/* Takes care of the pathfinding for the ghosts */
public class AStar {

    private PathNode[] graph;

    private class Node {
        public float g; // cost-so-far
        public float h; // heuristic
        public PathNode node; // reference to the actual node
        public PathNode parent;
        public Node(PathNode node) {
            this.node = node;
        }
    }

    private Node[] nodes;
    private int loopCounter;

    public AStar(PathNode[] graph) {
        this.graph = graph;
        this.nodes = new Node[graph.Length];
        for (int i = 0; i < this.nodes.Length; i++) {
            this.nodes[i] = new Node(this.graph[i]);
        }
    }

    public PathNode FindNextNode(PathNode current, PathNode dest) {
        List<PathNode> path = new List<PathNode>();
        PathNode currentNode = dest;

        this.CalculatePath(current, dest);

        while (currentNode != current) {
            if (currentNode == null || this.loopCounter > this.nodes.Length) break;
            path.Add(currentNode);
            currentNode = this.nodes[GetIndexWith(currentNode)].parent;
            this.loopCounter++;
        }
        this.loopCounter = 0;

        if (path.Count != 0) {
            path.Reverse();
            PathNode first = path[0];
            path.Clear();
            path = null;
            return first;
        }
        return null;
    }

    public PathNode GetClosestNode(UnityEngine.Vector3 position) {
        PathNode closest = this.graph[0];
        float closestDist = (closest.transform.position - position).magnitude;
        for(int i = 1; i < this.graph.Length; i++) {
            if((this.graph[i].transform.position - position).magnitude < closestDist) {
                closest = this.graph[i];
                closestDist = (closest.transform.position - position).magnitude;
            }
        }
        return closest;
    }
    
    private void CalculatePath(PathNode start, PathNode goal) {

        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        openList.Add(start);

        while (openList.Count != 0) {
            PathNode q = this.GetLeastCostNode(openList.ToArray());
            openList.Remove(q);
            foreach (PathNode neighbor in q.neighbors) {
                int qIndex = GetIndexWith(q);
                int neighborIndex = GetIndexWith(neighbor);
                float neighborCurrentCost = this.nodes[qIndex].g + GetEuclideanDist(q, neighbor);

                if (neighbor == goal) {
                    this.nodes[neighborIndex].parent = q;
                    return;
                }

                if (openList.Contains(neighbor)) {
                    if (this.nodes[neighborIndex].g <= neighborCurrentCost) continue;
                } else if (closedList.Contains(neighbor)) {
                    if (this.nodes[neighborIndex].g <= neighborCurrentCost) continue;
                    closedList.Remove(neighbor);
                    openList.Add(neighbor);
                } else {
                    openList.Add(neighbor);
                    this.nodes[neighborIndex].h = GetEuclideanDist(neighbor, goal);
                }
                this.nodes[neighborIndex].g = neighborCurrentCost;
                this.nodes[neighborIndex].parent = q;
            }
            closedList.Add(q);
        }
        openList.Clear();
        openList = null;
        closedList.Clear();
        closedList = null;
    }

    private float GetEuclideanDist(PathNode node1, PathNode node2) {
        return (node1.transform.position - node2.transform.position).magnitude;
    }

    private PathNode GetLeastCostNode(PathNode[] pathNodes) {
        PathNode nodeWithLeastCost = pathNodes[0];
        int nodeWithLeastCostIndex = GetIndexWith(nodeWithLeastCost);
        int nodeIndex;
        foreach (PathNode node in pathNodes) {
            nodeIndex = GetIndexWith(node);
            if (this.nodes[nodeIndex].g + this.nodes[nodeIndex].h < this.nodes[nodeWithLeastCostIndex].g + this.nodes[nodeWithLeastCostIndex].h) {
                nodeWithLeastCost = node;
                nodeWithLeastCostIndex = nodeIndex;
            }
        }
        return nodeWithLeastCost;
    }

    private int GetIndexWith(PathNode actualNode) {
        for (int i = 0; i < this.nodes.Length; i++) {
            if (nodes[i].node == actualNode)
                return i;
        }
        return -1;
    }
}