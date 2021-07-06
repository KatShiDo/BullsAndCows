using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace BullsAndCowsWPF
{
    public class MySuperBox : ViewModelBase
    {
        private int pos = 0;
        
        private ObservableCollection<Cell> vers = new ObservableCollection<Cell>();

        public IEnumerable<Cell> Items => vers;

        private int Pos
        {
            get => pos;
            set => Set(ref pos, value, nameof(ShiftX));
        }

        public int ShiftX => pos * 70;

        public MySuperBox()
        {
            foreach (var c in "    ")
            {
                vers.Add(new Cell{Value = c});
            }
        }
        
        public void Remove(char dig)
        {
            var cell = vers.Where(c => c.Value == dig).FirstOrDefault();
            if (cell != null)
            {
                cell.Value = ' ';
                Pos = vers.IndexOf(cell);
            }
        }

        public bool Contains(char dig) => vers.Any(c => c.Value == dig);
        public void Add(char dig)
        {
            if (!Contains(dig))
            {
                vers[pos].Value = dig;
                if (pos < 3)
                {
                    Pos++;
                }
            }
            else
            {
                Remove(dig);
            }
        }

        public Cell this[int i] => (i < 0 || i > 3) ? null : vers[i];

        public void SelectTo(Cell cell)
        {
            Pos = vers.IndexOf(cell);
        }

        public void SwapWith(Cell cell)
        {
            int index = vers.IndexOf(cell);
            (vers[pos], vers[index]) = (vers[index], vers[pos]);
        }

        public void Clear()
        {
            foreach (var c in vers)
            {
                c.Value = ' ';
            }

            Pos = 0;
        }

        public void SetHypo(string h)
        {
            int i = 0;
            foreach (var c in h.Where(ch => char.IsDigit(ch)))
            {
                vers[i].Value = c;
                i++;
            }
        }
    }

    public class Cell : ViewModelBase
    {
        private char value;

        public char Value
        {
            get => value;
            set => Set(ref this.value, value);
        }
        
        public override string ToString() => Value.ToString();
    }
}