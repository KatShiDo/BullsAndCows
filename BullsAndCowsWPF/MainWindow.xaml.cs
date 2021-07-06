using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BullsAndCowsWPF.Annotations;

namespace BullsAndCowsWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private BullsCowsModel model;
        
        public MainWindow()
        {
            InitializeComponent();
            model = new BullsCowsModel();
            model.WinComplete += Model_WinComplete;
            DataContext = model;
            icKeys.ItemsSource = "1234567890";
        }

        private void Model_WinComplete(object sender, EventArgs e)
        {
            var wnd = new WindowComplete();
            /*wnd.Width = 300;
            wnd.Height = 200;
            var sp = new StackPanel();
            sp.Children.Add(new TextBlock
            {
                Text = "You Win!!!"
            });
            var btn = new Button {Content = " New Game! "};
            btn.Click += (ss, se) => wnd.Close();
            sp.Children.Add(btn);
            wnd.Content = sp;*/
            wnd.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var chb = sender as Button;
            var dig = chb.Content.ToString()[0];
            model.PressKey(dig);
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            model.Send();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var cell = (sender as FrameworkElement).DataContext as Cell;
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                model.SwapWith(cell);
            }
            else
            {
                model.SelectTo(cell);
            }
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var h = (sender as FrameworkElement).DataContext.ToString();
            model.SetHypo(h);
        }
    }
}