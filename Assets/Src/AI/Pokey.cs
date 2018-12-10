
public class Pokey : Ghost {

    public float minDistToScatter;

    // based on http://gameinternals.com/post/2072558330/understanding-pac-man-ghost-behavior
    protected override PathNode GetTargetDestination() {
        if((this.transform.position - this.player.transform.position).magnitude < this.minDistToScatter) {
            return this.aStar.GetClosestNode(this.player.transform.position + 8f * this.player.transform.right);
        }
        return this.player.GetCurrent();
    }
}