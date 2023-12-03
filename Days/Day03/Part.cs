namespace AdventOfCode2023
{
    public class Part
    {
        public string Text { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }
        public int Int => int.Parse(Text);
        public Part(string text, int row, int column)
        {
            Text = text;
            Row = row;
            Column = column;
        }
    }
}
