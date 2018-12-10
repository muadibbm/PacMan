
public class Blinky : Ghost {

    // based on http://gameinternals.com/post/2072558330/understanding-pac-man-ghost-behavior
    protected override PathNode GetTargetDestination() {
        return this.player.GetCurrent();
    }
}