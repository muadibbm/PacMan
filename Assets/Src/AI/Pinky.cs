
public class Pinky : Ghost {

    // based on http://gameinternals.com/post/2072558330/understanding-pac-man-ghost-behavior
    protected override PathNode GetTargetDestination() {
        return this.aStar.GetClosestNode(this.player.transform.position + 4f * this.player.transform.right);
    }
}
