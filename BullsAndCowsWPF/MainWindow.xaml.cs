using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
            DataContext = model;
        }
    }

    public class BullsCowsModel : INotifyPropertyChanged
    {
        private string enigma;
        
        private string currentVersion;
        
        public int Step { get; private set; }

        public ObservableCollection<Hypo> Hypothesis { get; } = new ObservableCollection<Hypo>();

        public BullsCowsModel()
        {
            Init();
        }

        public void Init()
        {
            Random rnd = new Random();
            char[] digits = "0123456789".ToCharArray();
            for (int i = 0; i < 4; i++)
            {
                int j = rnd.Next(i, 10);
                (digits[i], digits[j]) = (digits[j], digits[i]);
            }

            enigma = new string(digits, 0, 4);
        }

        public string CurrentVersion
        {
            get => currentVersion;
            set
            {
                if (value != currentVersion)
                {
                    currentVersion = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(currentVersion)));

                    if (currentVersion.Length == 4 && currentVersion.Distinct().Count() == 4 &&
                        currentVersion.All(ch => char.IsDigit(ch)))
                    {
                        int bulls = 0, cows = 0;

                        for (int i = 0; i < 4; i++)
                        {
                            if (currentVersion[i] == enigma[i])
                            {
                                bulls++;
                            }
                            else if (enigma.Contains(currentVersion[i]))
                            {
                                cows++;
                            }
                        }

                        Step++;

                        Hypothesis.Add(new Hypo
                        {
                            Step = Step,
                            Num = currentVersion,
                            Bulls = bulls,
                            Cows = cows
                        });

                        while (Hypothesis.Count > 8)
                        {
                            Hypothesis.RemoveAt(0);
                        }
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        
    }

    public class Hypo
    {
        public string Num { get; set; }
        public int Bulls { get; set; }
        public int Cows { get; set; }
        public int Step { get; internal set; }

        public override string ToString() => Num;
    }
}