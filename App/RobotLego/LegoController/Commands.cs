using AsyncEV3MotorCommandsLib;
using Lego.Ev3.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LegoController
{
    /// <summary>
    /// Simple commands to manage the way the robot moves
    /// </summary>
    public class Commands
    {
        private BrickManager brickManager = Manager.brickManager;
        public Dictionary<char, OutputPort> ports = Ports.ports;
        public Dictionary<char, InputPort> inputPorts = Ports.inputPorts;

        /// <summary>
        /// Run the designed motor to check if it works
        /// </summary>
        /// <param name="port"> Implicite way to name a specific motor </param>
        /// <param name="power"> Powerness apply the the motors </param>
        public async void MotorTest(char port, int power)
        {
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(ports[port], power);
            await Task.Delay(2000);
            await brickManager.DirectCommand.StopMotorAsync(inputPorts[port]);
            await Task.Delay(2000);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(ports[port], -power);
            await Task.Delay(2000);
            await brickManager.DirectCommand.StopMotorAsync(inputPorts[port]);
        }

        /// <summary>
        /// Emergy stop of all motors
        /// </summary>
        public async void EmergencyStop()
        {
            await brickManager.DirectCommand.StopMotorAsync(InputPort.A);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.B);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.C);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.D);
        }

        /// <summary>
        /// Move (forward / backward)
        /// </summary>
        /// <param name="power"> Powerness apply the the motors </param>
        public async void MoveLinearX(int power)
        {
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.C, -power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.D, -power);
        }

        /// <summary>
        /// Move (left / right)
        /// </summary>
        /// <param name="power"> Powerness apply the the motors </param>
        public async void MoveLinearY(int power)
        {
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, -power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.C, -power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.D, power);
        }

        /// <summary>
        /// Move (up right / down left)
        /// </summary>
        /// <param name="power"> Powerness apply the the motors </param>
        public async void MoveDiagonal1(int power)
        {
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, 0);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.C, -power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.D, 0);
        }

        /// <summary>
        /// Move (up left / down right)
        /// </summary>
        /// <param name="power"> Powerness apply the the motors </param>
        public async void MoveDiagonal2(int power)
        {  
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, 0);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.C, 0);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.D, -power);
        }

        /// <summary>
        /// Rotation (left / right)
        /// </summary>
        /// <param name="power"> Powerness apply the the motors </param>
        public async void Turn(int power)
        {
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, -power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.C, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.D, -power);
        }



        /* ---------------------------------------------------------------------------- //
        // ------------------------------ Custom presets ------------------------------ //
        // ---------------------------------------------------------------------------- */


        /// <summary>
        /// Quickly shows how the robot move (forward/backward)
        /// </summary>
        /// <param name="power"> Powerness apply the the motors </param>
        public async void Command_1(int power)
        {
            EmergencyStop();
            MoveLinearX(power);
            await Task.Delay(2000);
            EmergencyStop();
            await Task.Delay(500);
            MoveLinearX(-power);
            await Task.Delay(2000);
            EmergencyStop();
        }

        /// <summary>
        /// Quickly shows how the robot move (left/right)
        /// </summary>
        /// <param name="power"> Powerness apply the the motors </param>
        public async void Command_2(int power)
        {
            EmergencyStop();
            MoveLinearY(power);
            await Task.Delay(2000);
            EmergencyStop();
            await Task.Delay(500);
            MoveLinearY(-power);
            await Task.Delay(2000);
            EmergencyStop();
        }

        /// <summary>
        /// Shows how the robot move (diagonaly)
        /// </summary>
        /// <param name="power"> Powerness apply the the motors </param>
        public async void Command_3(int power)
        {
            EmergencyStop();
            MoveDiagonal1(power);
            await Task.Delay(2000);
            EmergencyStop();
            await Task.Delay(500);
            MoveDiagonal1(-power);
            await Task.Delay(4000);
            EmergencyStop();
            await Task.Delay(500);
            MoveDiagonal1(power);
            await Task.Delay(2000);
            EmergencyStop();
            await Task.Delay(1000);
            MoveDiagonal2(power);
            await Task.Delay(2000);
            EmergencyStop();
            await Task.Delay(500);
            MoveDiagonal2(-power);
            await Task.Delay(4000);
            EmergencyStop();
            await Task.Delay(500);
            MoveDiagonal2(power);
            await Task.Delay(2000);
            EmergencyStop();
        }
    }
}
