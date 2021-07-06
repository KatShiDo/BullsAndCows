namespace BullsAndCowsWPF
{
    public class Hypo
    {
        public string Num { get; set; }
        public int Bulls { get; set; }
        public int Cows { get; set; }
        public int Step { get; internal set; }

        public override string ToString() => Num;
    }
}