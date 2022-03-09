using AsyncEV3MotorCommandsLib;
using BluetoothDevicesScanner;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LegoController
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Manager manager = new Manager();
        private Commands commands = Manager.commands;
        private BrickManager brickManager = Manager.brickManager;

        private BluetoothDevice selectedDevice;
        event EventHandler DeviceSelected;


        public MainWindow()
        {
            InitializeComponent();
            DataContext = brickManager;
            manager.LaunchDetection();

        }

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

        public async void MainWindow_DeviceSelected(object sender, EventArgs e)
        {
            await brickManager.ConnectAsync(SelectedDevice?.DeviceName, SelectedDevice?.COMPort);

            if (brickManager.Connected)
            {
                // Le son est affreux, mes oreilles SVP
                await brickManager.PlayThirdKindAsync(volume: 1, duration: 100);
            }
        }

        public void root_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (brickManager.Connected)
                brickManager.Disconnect();
            manager.t.Abort();
            manager.t2.Abort();
        }

        public void root_Loaded(object sender, RoutedEventArgs e)
        {
            DeviceSelected += MainWindow_DeviceSelected;
        }



        // ----------------------------------------------------------------------------------------------------------------------------------------------- //
        // ----------------------------------------------------------------------------------------------------------------------------------------------- //
        // ----------------------------------------------------------------------------------------------------------------------------------------------- //

        private int power = 50;
        private string command = "";

        private void sliderPower_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            power = Convert.ToInt32(sliderPower.Value);
            switch (command)
            {
                case "":
                    break;
                case "moveLinearX":
                    commands.MoveLinearX(power);
                    break;
                case "moveLinearX2":
                    commands.MoveLinearX(-power);
                    break;
                case "moveLinearY":
                    commands.MoveLinearY(power);
                    break;
                case "moveLinearY2":
                    commands.MoveLinearY(-power);
                    break;
                case "moveDiagonal11":
                    commands.MoveDiagonal1(power);
                    break;
                case "moveDiagonal21":
                    commands.MoveDiagonal2(power);
                    break;
                case "moveDiagonal22":
                    commands.MoveDiagonal2(-power);
                    break;
                case "moveDiagonal12":
                    commands.MoveDiagonal1(-power);
                    break;
                case "turnLeft":
                    commands.Turn(power);
                    break;
                case "turnRight":
                    commands.Turn(-power);
                    break;
            }
        }

        private void ButtonAction_TestMotor(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button)) return;
            char port = ((sender as Button).Content as string).Last();
            if (!Ports.ports.ContainsKey(port)) return;

            commands.MotorTest(port, power);
        }

        private void ButtonAction_EmergencyStop(object sender, RoutedEventArgs e)
        {
            commands.EmergencyStop();
            command = "";
        }

        private void ButtonAction_Forward(object sender, RoutedEventArgs e)
        {
            commands.MoveLinearX(power);
            command = "moveLinearX";
        }

        private void ButtonAction_Backward(object sender, RoutedEventArgs e)
        {
            commands.MoveLinearX(-power);
            command = "moveLinearX2";
        }

        private void ButtonAction_Left(object sender, RoutedEventArgs e)
        {
            commands.MoveLinearY(power);
            command = "moveLinearY";
        }

        private void ButtonAction_Right(object sender, RoutedEventArgs e)
        {
            commands.MoveLinearY(-power);
            command = "moveLinearY2";
        }

        private void ButtonAction_UpLeft(object sender, RoutedEventArgs e)
        {
            commands.MoveDiagonal1(power);
            command = "moveDiagonal11";
        }

        private void ButtonAction_UpRight(object sender, RoutedEventArgs e)
        {
            commands.MoveDiagonal2(power);
            command = "moveDiagonal21";
        }

        private void ButtonAction_DownLeft(object sender, RoutedEventArgs e)
        {
            commands.MoveDiagonal2(-power);
            command = "moveDiagonal22";
        }

        private void ButtonAction_DownRight(object sender, RoutedEventArgs e)
        {
            commands.MoveDiagonal1(-power);
            command = "moveDiagonal12";
        }


        private void ButtonAction_TurnLeft(object sender, RoutedEventArgs e)
        {
            commands.Turn(power);
            command = "turnLeft";
        }

        private void ButtonAction_TurnRight(object sender, RoutedEventArgs e)
        {
            commands.Turn(-power);
            command = "turnRight";
        }




        // ------------------ SPECIALS ACTIONS ------------------ //

        private void ButtonAction_1(object sender, RoutedEventArgs e)
        {
            commands.Command_1(power);
        }

        private void ButtonAction_2(object sender, RoutedEventArgs e)
        {
            commands.Command_2(power);
        }

        private void ButtonAction_3(object sender, RoutedEventArgs e)
        {
            commands.Command_3(power);
        }
    }
}
