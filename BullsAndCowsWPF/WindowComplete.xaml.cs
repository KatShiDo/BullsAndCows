using System.Windows;

namespace BullsAndCowsWPF
{
    public partial class WindowComplete : Window
    {
        public WindowComplete()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}