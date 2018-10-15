using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot.Common;

namespace Dzyra.Nazar.RobotChallange.Test {

    [TestClass]
    public class TestAlgorithm {
        //1
        [TestMethod]
        public void TestDictance() {

            Position pos1 = new Position(0, 0);
            Position pos2 = new Position(2, 4);

            Assert.AreEqual(20, DistanceHelper.FindDistance(pos1, pos2));
            
        }

        //2
        [TestMethod]
        public void TestMoveCommand() {
            DzyraNazarAlgorithm algorithm = new DzyraNazarAlgorithm();

            Map map = new Map();
            Position stationPosition = new Position(1, 1);

            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 50,
                    Position = new Position(2, 3),
                }
            };

            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsTrue(command is MoveCommand);
            Assert.AreEqual(((MoveCommand)command).NewPosition, stationPosition);
        }

        //3
        [TestMethod]
        public void TestCollectEnergyCommand() {
            DzyraNazarAlgorithm algorithm = new DzyraNazarAlgorithm();

            Map map = new Map();
            Position stationPosition = new Position(1, 1);

            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 50,
                    Position = stationPosition,
                    
                }
            };

            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsTrue(command is CollectEnergyCommand);
        }

        //4
        [TestMethod]
        public void TestCreateNewRobotCommand() {
            DzyraNazarAlgorithm algorithm = new DzyraNazarAlgorithm();

            Map map = new Map();
            Position stationPosition = new Position(1, 1);

            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 500,
                    Position = new Position(2, 3),
                }
            };

            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsTrue(command is CreateNewRobotCommand);
        }

        //5
        [TestMethod]
        public void TestNeedToCreateRobotFunction() {
            DzyraNazarAlgorithm algorithm = new DzyraNazarAlgorithm();

            Map map = new Map();
            Position stationPosition = new Position(1, 1);

            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 1000,
                    Position = new Position(2, 3),
                }
            };

            bool action = algorithm.NeedToCreateRobot(robots[0]);

            Assert.IsTrue(action);
        }

        //6
        [TestMethod]
        public void TestNeedToCollectEnergyFunction() {
            DzyraNazarAlgorithm algorithm = new DzyraNazarAlgorithm();

            Map map = new Map();
            Position stationPosition = new Position(1, 1);

            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 1000,
                    Position = stationPosition,
                }
            };

            bool action = algorithm.NeedToCollectEnergy(robots[0], map);

            Assert.IsTrue(action);
        }

        //7
        [TestMethod]
        public void TestGetMovePointFunction() {
            DzyraNazarAlgorithm algorithm = new DzyraNazarAlgorithm();

            Map map = new Map();
            Position stationPosition = new Position(1, 1);

            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 1000,
                    Position = new Position(2,3),
                }
            };


            Position expectedPos = new Position(1, 1);
            Position newPos = algorithm.GetMovePoint(robots[0], map, robots);

            Assert.AreEqual(expectedPos, newPos);
        }

        //8
        [TestMethod]
        public void TestIsStationFreeFunction() {
            DzyraNazarAlgorithm algorithm = new DzyraNazarAlgorithm();
            Map map = new Map();
            Position stationPosition = new Position(1, 1);

            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 1000,
                    Position = new Position(2,3),
                }
            };

            bool action = algorithm.IsStationFree(map.Stations[0], robots[0], robots);

            Assert.IsTrue(action);
        }

        //9
        [TestMethod]
        public void TestIsCellFreeFunction() {
            DzyraNazarAlgorithm algorithm = new DzyraNazarAlgorithm();
            Map map = new Map();
            Position stationPosition = new Position(1, 1);

            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 1000,
                    Position = new Position(2,3),
                }
            };

            bool action = algorithm.IsCellFree(stationPosition, robots[0], robots);

            Assert.IsTrue(action);
        }

        //10s
        [TestMethod]
        public void TestRobotCountFunction() {
            DzyraNazarAlgorithm algorithm = new DzyraNazarAlgorithm();
            Map map = new Map();
            Position stationPosition = new Position(1, 1);

            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>();

            int count = algorithm.MyRobotsCount;

            Assert.AreEqual(0, count);
        }
    }
}
