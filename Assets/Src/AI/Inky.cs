
public class Inky : Ghost {

    public Blinky blinky;

    // based on http://gameinternals.com/post/2072558330/understanding-pac-man-ghost-behavior
    protected override PathNode GetTargetDestination() {
        return this.aStar.GetClosestNode(
            ((this.player.transform.position + 2f * this.player.transform.right) - this.blinky.transform.position) * 2f);
    }
}
