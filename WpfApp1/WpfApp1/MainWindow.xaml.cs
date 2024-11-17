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

        // MasterMind-03: Vul de ComboBoxen met de beschikbare kleuren
        private void InitializeGame()
        {
            // Vul de ComboBoxen met de 6 beschikbare kleuren
            ComboBox1.ItemsSource = _colors;
            ComboBox2.ItemsSource = _colors;
            ComboBox3.ItemsSource = _colors;
            ComboBox4.ItemsSource = _colors;

            // Genereer een willekeurige code van 4 kleuren
            _generatedCode = GenerateRandomCode(); ;

            // MasterMind-02: Toon de gegenereerde code in de titel van het venster (voor testdoeleinden)
            Title = $"Mastermind - Code: {string.Join(", ", _generatedCode)}";
        }

        // MasterMind-04: Wanneer er een kleur gekozen wordt uit een ComboBox, toon de kleur in het label
        private void OnColorSelected(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is string selectedColor)
            {
                // Map de ComboBox naar het bijbehorende Label om de geselecteerde kleur weer te geven
                if (comboBox == ComboBox1) Label1.Content = selectedColor;
                if (comboBox == ComboBox2) Label2.Content = selectedColor;
                if (comboBox == ComboBox3) Label3.Content = selectedColor;
                if (comboBox == ComboBox4) Label4.Content = selectedColor;
            }
        }

        // MasterMind-05: Controleer de ingegeven code wanneer de gebruiker op de knop klikt
        private void CheckCode(object sender, RoutedEventArgs e)
        {
            // Verkrijg de geselecteerde kleuren uit de ComboBoxen
            var guesses = new[] {
                ComboBox1.SelectedItem as string,
                ComboBox2.SelectedItem as string,
                ComboBox3.SelectedItem as string,
                ComboBox4.SelectedItem as string
            };

            var labels = new[] { Label1, Label2, Label3, Label4 };

            // Loop door elke ComboBox en vergelijk de keuzes met de gegenereerde code
            for (int i = 0; i < 4; i++)
            {
                if (guesses[i] == null) continue;  // Sla over als geen kleur geselecteerd is

                // Controleer of de gok correct is en handel de randkleur af
                if (guesses[i] == _generatedCode[i])
                {
                    labels[i].BorderBrush = Brushes.DarkRed;  // Correcte kleur en positie
                    labels[i].BorderThickness = new Thickness(2);  // Stel randdikte in
                }
                else if (_generatedCode.Contains(guesses[i]))
                {
                    labels[i].BorderBrush = Brushes.Wheat;  // Correcte kleur, verkeerde positie
                    labels[i].BorderThickness = new Thickness(2);  // Stel randdikte in
                }
                else
                {
                    labels[i].BorderBrush = Brushes.Black;  // Onjuiste kleur
                    labels[i].BorderThickness = new Thickness(1);  // Stel randdikte in
                }
            }
        }
    }
}
