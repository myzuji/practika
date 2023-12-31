﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text.Json;
namespace office
{

    public partial class MainWindow : Window
    {
        Simulation simulation;
        public MainWindow()
        {
            InitializeComponent();
            simulation = new Simulation();
            simulation.loadFromJson("test.json");

            DataContext = simulation;
            NextStepButton.Click += NextStepButton_Click;
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            simulation.nextStep();
        }
    }
}
