namespace RenderTest.Model
{
    public class PausableItem : ModelItem, IPausable
    {
        public int PauseCount { get; set; }
    }
}
