using AsyncEV3MotorCommandsLib;
using Lego.Ev3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoControler
{
    class Commands
    {
        private readonly BrickManager brickManager = Manager.brickManager;

        private Dictionary<char, OutputPort> ports = Ports.ports;
        private Dictionary<char, InputPort> inputPorts = Ports.inputPorts;

        public async void Test(char port)
        {
            //await brickManager.Brick.DirectCommand.TurnMotorAtPowerForTimeAsync(ports[port], 100, 2000, true);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(ports[port], 100);
            await Task.Delay(500);
            await brickManager.DirectCommand.StopMotorAsync(inputPorts[port]);
            await Task.Delay(500);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(ports[port], -100);
            await Task.Delay(500);
            await brickManager.DirectCommand.StopMotorAsync(inputPorts[port]);
        }

        public async void EmergencyStop()
        {
            // Trouver comment arrêter seulement les port connectés
            // Surement un for avec une condition mais pas sûr que ça soit mieux niveau performance
            await brickManager.DirectCommand.StopMotorAsync(InputPort.A);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.B);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.C);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.D);
        }

        public async void MoveLinearWithPower(int power)
        {
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.C, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.D, power);
        }

        public async void Command_1()
        {
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, 100);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, 100);
            await Task.Delay(2000);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.A);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.B);
        }
    }
}
