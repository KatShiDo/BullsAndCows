using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace BullsAndCowsWPF
{
    public class BullsCowsModel : ViewModelBase
    {
        private string enigma;

        public int Step { get; private set; }

        public event EventHandler WinComplete;

        public Visibility VsbBtnSend
        {
            get
            {
                if (Vers.Items.Count() == 4 && Vers.Items.Distinct().Count() == 4 && Vers.Items.All(s => int.TryParse("" + s, out var _)))
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public ObservableCollection<Hypo> Hypothesis { get; } = new ObservableCollection<Hypo>();

        public MySuperBox Vers { get; } = new MySuperBox();

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

        public event PropertyChangedEventHandler PropertyChanged;
        
        public void PressKey(char dig)
        {
            Vers.Add(dig);
            Fire(nameof(VsbBtnSend));
        }

        public void Send()
        {
            int bulls = 0, cows = 0;

            for (int i = 0; i < 4; i++)
            {
                if (Vers[i].Value == enigma[i])
                {
                    bulls++;
                }
                else if (enigma.Contains(Vers[i].Value))
                {
                    cows++;
                }
            }

            Step++;

            Hypothesis.Add(new Hypo
            {
                Step = Step,
                Num = string.Join(" " , Vers.Items),
                Bulls = bulls,
                Cows = cows
            });

            while (Hypothesis.Count > 8)
            {
                Hypothesis.RemoveAt(0);
            }

            if (bulls == 4)
            {
                WinComplete?.Invoke(this, EventArgs.Empty);
            }
            
            Vers.Clear();
        }

        public void SelectTo(Cell cell)
        {
            Vers.SelectTo(cell);
        }

        public void SwapWith(Cell cell)
        {
            Vers.SwapWith(cell);
        }

        public void SetHypo(string h)
        {
            Vers.SetHypo(h);
        }
    }
}