using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Maui.Controls;

namespace JM_Apuntes.Views
{
    public partial class StatsPage : ContentPage
    {
        public StatsPage()
        {
            InitializeComponent();
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            string inputText = InputEntry.Text;

            int totalCharacters = inputText.Length;
            int totalNumbers = inputText.Count(char.IsDigit);
            int totalLetters = inputText.Count(char.IsLetter);
            int totalVowels = inputText.Count(c => "aeiouAEIOU".Contains(c));
            int totalUppercase = inputText.Count(char.IsUpper);
            int totalLowercase = inputText.Count(char.IsLower);

            string resultText = $"Total caracteres: {totalCharacters}\n" +
                                $"Total números: {totalNumbers}\n" +
                                $"Total letras: {totalLetters}\n" +
                                $"Total vocales: {totalVowels}\n" +
                                $"Total mayúsculas: {totalUppercase}\n" +
                                $"Total minúsculas: {totalLowercase}";

            ResultLabel.Text = resultText;
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            InputEntry.Text = string.Empty;
            ResultLabel.Text = string.Empty;
        }
    }
}
