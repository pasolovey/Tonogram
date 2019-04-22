namespace RenderTest.Model
{
    public class ModelItem
    {
        public string Text { get; set; }
        public int Type { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
    }

    public interface IPausable
    {
        int PauseCount { get; set; }
    }


    public class PausableItem : ModelItem, IPausable
    {
        public int PauseCount { get; set; }
    }
}
