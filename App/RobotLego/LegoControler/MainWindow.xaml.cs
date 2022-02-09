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
        private BrickManager brickManager = Manager.brickManager;
        private Commands commands = Manager.commands;

        private BluetoothDevice selectedDevice;
        event EventHandler DeviceSelected;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = brickManager;
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
        }

        public void root_Loaded(object sender, RoutedEventArgs e)
        {
            DeviceSelected += MainWindow_DeviceSelected;
        }

        // -----------------------------------------------------------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------------------------------------------------------


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button)) return;
            char port = ((sender as Button).Content as string).Last();
            if (!Ports.ports.ContainsKey(port)) return;

            commands.Test(port);
        }

        private void ButtonAction_1(object sender, RoutedEventArgs e)
        {
            commands.Command_1();
        }

        private void ButtonAction_EmergencyStop(object sender, RoutedEventArgs e)
        {
            commands.EmergencyStop();
        }

        private void ButtonAction_ForwardFull(object sender, RoutedEventArgs e)
        {
            commands.MoveLinearWithPower(100);
        }

        private void ButtonAction_ForwardHalf(object sender, RoutedEventArgs e)
        {
            commands.MoveLinearWithPower(10);
        }

        private void Action_Test_1(object sender, RoutedEventArgs e)
        { 
            commands.MoveLinearWithPower(10);
        }
    }
}
