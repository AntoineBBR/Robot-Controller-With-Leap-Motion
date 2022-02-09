using AsyncEV3MotorCommandsLib;
using BluetoothDevicesScanner;
using Lego.Ev3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LegoControler
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BluetoothDevice selectedDevice;
        event EventHandler DeviceSelected;

        BrickManager brickManager = new BrickManager();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = brickManager;
        }

        internal static Dictionary<char, OutputPort> ports = new Dictionary<char, OutputPort>()
        {
            {'A', OutputPort.A},
            {'B', OutputPort.B},
            {'C', OutputPort.C},
            {'D', OutputPort.D}
        };

        internal static Dictionary<char, InputPort> inputPorts = new Dictionary<char, InputPort>()
        {
            {'A', InputPort.A},
            {'B', InputPort.B},
            {'C', InputPort.C},
            {'D', InputPort.D}
        };

        internal static List<string> directionRobot = new List<string>()
        {
            "HAUT",
            "BAS",
            "GAUCHE",
            "DROITE"
        };

        public BluetoothDevice SelectedDevice
        {
            get { return selectedDevice; }
            set
            {
                selectedDevice = value;
                if (value != null)
                {
                    if (brickManager.Connected)
                        brickManager.Disconnect();
                    DeviceSelected?.Invoke(this, null);
                }
            }
        }

        private void root_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (brickManager.Connected)
                brickManager.Disconnect();
        }

        private void root_Loaded(object sender, RoutedEventArgs e)
        {
            DeviceSelected += MainWindow_DeviceSelected;
        }

        private async void MainWindow_DeviceSelected(object sender, EventArgs e)
        {
            await brickManager.ConnectAsync(SelectedDevice?.DeviceName, SelectedDevice?.COMPort);

            if (brickManager.Connected)
            {
                // Le son est affreux, mes oreilles SVP
                await brickManager.PlayThirdKindAsync(volume: 1, duration: 100);
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button)) return;
            char port = ((sender as Button).Content as string).Last();
            if (!ports.ContainsKey(port)) return;

            //await brickManager.Brick.DirectCommand.TurnMotorAtPowerForTimeAsync(ports[port], 100, 2000, true);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(ports[port], 100);
            await Task.Delay(500);
            await brickManager.DirectCommand.StopMotorAsync(inputPorts[port]);
            await Task.Delay(500);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(ports[port], -100);
            await Task.Delay(500);
            await brickManager.DirectCommand.StopMotorAsync(inputPorts[port]);
        }

        private async void ButtonAction_1(object sender, RoutedEventArgs e)
        {
            // Test
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, 100);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, 100);
            await Task.Delay(2000);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.A);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.B);
        }

        private async void ButtonAction_EmergencyStop(object sender, RoutedEventArgs e)
        {
            // Trouver comment arrêter seulement les port connectés
            // Surement un for avec une condition mais pas sûr que ça soit mieux niveau performance
            await brickManager.DirectCommand.StopMotorAsync(InputPort.A);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.B);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.C);
            await brickManager.DirectCommand.StopMotorAsync(InputPort.D);
        }

        private async void ButtonAction_ForwardFull(object sender, RoutedEventArgs e)
        {
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, 100);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, 100);
        }

        private async void ButtonAction_ForwardHalf(object sender, RoutedEventArgs e)
        {
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, 10);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, 10);
        }

        private void Action_Test_1(object sender, RoutedEventArgs e)
        { 
            MoveLinearWithPower(10);
        }

        private async void MoveLinearWithPower(int power)
        {
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.A, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.B, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.C, power);
            await brickManager.Brick.DirectCommand.TurnMotorAtPowerAsync(OutputPort.D, power);
        }



        private async void ActionMain(object sender, RoutedEventArgs e)
        {

        }
    }
}
