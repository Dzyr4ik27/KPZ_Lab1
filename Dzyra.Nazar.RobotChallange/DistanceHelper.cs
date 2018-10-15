using Robot.Common;

namespace Dzyra.Nazar.RobotChallange {
    public class DistanceHelper {

        public static int FindDistance(Position a, Position b) {
            return (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
        }
    }
}
