public class Solution {
    public int CarFleet(int target, int[] position, int[] speed) {
        var cars = new (int position, double time)[position.Length];
        for (var i = 0; i < position.Length; ++i) {
            cars[i] = (position[i], 1.0 * (target - position[i]) / speed[i]);
        }
        Array.Sort(cars, (a, b) => a.position - b.position);
        var numOfFleets = 0;
        var turtuleTime = 0.0;
        for (var i = cars.Length - 1; i >= 0; --i) {
            if (turtuleTime < cars[i].time) {
                turtuleTime = cars[i].time;
                numOfFleets ++;
            }
        }
        return numOfFleets;
    }
}
