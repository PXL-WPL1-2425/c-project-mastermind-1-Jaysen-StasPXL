using System;
using System.Linq;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MastermindGame
{
    public partial class MainWindow : Window
    {
        private readonly string[] _colors = { "Red", "Yellow", "Orange", "White", "Green", "Blue" };
        private string[] _generatedCode; // Private variable to store the generated code

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        // MasterMind-02: Genereer een willekeurige code van 4 kleuren
        private string[] GenerateRandomCode()
        {
            var random = new Random();
            return Enumerable.Range(0, 4)
                             .Select(_ => _colors[random.Next(_colors.Length)])
                             .ToArray();
        }
