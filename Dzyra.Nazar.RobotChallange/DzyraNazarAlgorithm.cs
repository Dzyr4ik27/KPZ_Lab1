using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robot.Common;


namespace Dzyra.Nazar.RobotChallange {

    public class DzyraNazarAlgorithm : IRobotAlgorithm {
        private readonly string robotOwnerName = "Nazar Dzyra";
        private readonly int maxRobotsCount = 80;

        private int myRobotsCount;
        public int MyRobotsCount { get; set; }

        private int minEnergyToSpawn = 300;
        private int minRobotEnergy = 0;

        private Position movePoint;
        private Position stationPosition;




        public string Author
        {
            get { return robotOwnerName; }
        }

        public string Description
        {
            get { return "This is my perfect algorithm without bugs...almost;)"; }
        }

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map) {

            Robot.Common.Robot movingRobot = robots[robotToMoveIndex];

            //CountRobots(robots); //comment when testing

            Position pos = FindNearestFreeStation(movingRobot, map, robots);
            if (DistanceHelper.FindDistance(movingRobot.Position, pos) < 400) 
                if (NeedToCreateRobot(movingRobot)) {
                    return new CreateNewRobotCommand();
                }

            stationPosition = GetMovePoint(movingRobot, map, robots);

            if (stationPosition == null) {
                return null;
            }

            if (NeedToCollectEnergy(movingRobot, map)) {
                return new CollectEnergyCommand();
            }
            else {
                return new MoveCommand() { NewPosition = stationPosition };
            }
        }


        #region FindStation 
        public Position FindNearestFreeStation(Robot.Common.Robot movingRobot,
            Map map, IList<Robot.Common.Robot> robots) {

            EnergyStation nearestStation = null;
            int minDistance = int.MaxValue;

            foreach (EnergyStation station in map.Stations) {
                if (IsStationFree(station, movingRobot, robots)) {
                    int distance = DistanceHelper.FindDistance(station.Position, movingRobot.Position);

                    if (distance < minDistance) {
                        minDistance = distance;
                        nearestStation = station;
                    }
                }
            }

            return nearestStation == null ? null : nearestStation.Position;
        }

        public bool IsStationFree(EnergyStation station, Robot.Common.Robot movingRobot,
            IList<Robot.Common.Robot> robots) {
            return IsCellFree(station.Position, movingRobot, robots);
        }

        //comment when testing
        public bool IsCellFree(Position cell, Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots) {

            //foreach (Robot.Common.Robot robot in robots) {                         
            //    if (robot.Owner.Name.Equals(robotOwnerName)) {                    
            //        if (robot.Position == cell) {
            //            return false;
            //        }
            //    }
            //}

            return true;
        }
        #endregion

        #region MyCode

        public bool NeedToCreateRobot(Robot.Common.Robot movingRobot) {
            return movingRobot.Energy >= minEnergyToSpawn && myRobotsCount < maxRobotsCount;
        }

        public bool NeedToCollectEnergy(Robot.Common.Robot movingRobot, Map map) {
            foreach (EnergyStation station in map.Stations) {
                if (station.Position == movingRobot.Position) {
                    return true;
                }
            }

            return false;

        }

        public void CountRobots(IList<Robot.Common.Robot> robots) {
            myRobotsCount = 0;
            foreach (Robot.Common.Robot robot in robots) {
                if (robot.Owner.Name.Equals(robotOwnerName)) {
                    myRobotsCount++;
                }
            }
        }

        public Position GetMovePoint(Robot.Common.Robot movingRobot,
            Map map, IList<Robot.Common.Robot> robots) {

            movePoint = FindNearestFreeStation(movingRobot, map, robots);

            for (int i = 0; i < 10; i++) {
                if (DistanceHelper.FindDistance(movePoint, movingRobot.Position) <= movingRobot.Energy + minRobotEnergy) {
                    return movePoint;
                }
                else {
                    movePoint.X = (movePoint.X + movingRobot.Position.X) / 2;
                    movePoint.Y = (movePoint.Y + movingRobot.Position.Y) / 2;
                }
            }

            return null;
        }

        #endregion
    }

}
