using AsyncEV3MotorCommandsLib;
using Lego.Ev3.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LegoControler
{
    public class Commands
    {
        private BrickManager brickManager = Manager.brickManager;
        private Dictionary<char, OutputPort> ports = Ports.ports;
        private Dictionary<char, InputPort> inputPorts = Ports.inputPorts;


        public async void MotorTest(char port)
        {
            //await brickManager.Brick.DirectCommand.TurnMotorAtPowerForTimeAsync(ports[port], 100, 2000, true);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(ports[port], 100);
            await Task.Delay(2000);
            await brickManager.DirectCommand.StopMotorAsync(inputPorts[port]);
            await Task.Delay(2000);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(ports[port], -100);
            await Task.Delay(2000);
            await brickManager.DirectCommand.StopMotorAsync(inputPorts[port]);
        }

        public async void EmergencyStop()
        {
            // Arrêt de tous les moteurs
            await brickManager.DirectCommand.StopMotorAsync(InputPort.A);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.B);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.C);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.D);
        }

        public async void MoveLinearX(int power)
        {
            // Avancement sur l'axe X (forward / backward)
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.C, -power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.D, -power);
        }

        public async void MoveLinearY(int power)
        {
            // Avancement sur l'axe Y (left / right)
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, -power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.C, -power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.D, power);
        }

        public async void MoveDiagonal1(int power)
        {
            // Avancement sur la diagonale (up right / down left)
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, 0);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.C, -power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.D, 0);
        }

        public async void MoveDiagonal2(int power)
        {
            // Avancement sur la diagonale (up left / down right)
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, 0);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.C, 0);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.D, -power);
        }


        public async void Turn(int power)
        {
            // Rotation (left / right)
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, -power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.C, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.D, -power);
        }





        // ------------------------- Commandes Personnalisées ------------------------- //

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
