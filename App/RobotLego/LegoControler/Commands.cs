using AsyncEV3MotorCommandsLib;
using Lego.Ev3.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LegoControler
{
    class Commands
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
            // Trouver comment arrêter seulement les port connectés
            // Surement un for avec une condition mais pas sûr que ça soit mieux niveau performance
            await brickManager.DirectCommand.StopMotorAsync(InputPort.A);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.B);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.C);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.D);
        }

        public async void MoveLinearX(int power)
        {
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.C, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.D, power);
        }

        public async void MoveLinearY(int power)
        {
            // TODO: Faire la mise au point pour gérer le sens d'avancement
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, -power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.C, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.D, -power);
        }

        public async void MoveCombinedXY(int power)
        {
            // TODO: Faire la mise au point pour gérer le sens d'avancement
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.C, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.D, power);
        }

        public async void TurnLeft(int power)
        {
            // TODO: Faire la mise au point pour gérer le sens d'avancement
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.C, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.D, power);
        }

        public async void TurnRight(int power)
        {
            // TODO: Faire la mise au point pour gérer le sens d'avancement
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.C, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.D, power);
        }





        // ------------------------- Commandes Personnalisés ------------------------- //

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
