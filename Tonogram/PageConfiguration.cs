namespace Tonogram
{
    public class PageConfiguration
    {
        public double PageWidth { get; set; }

        public double PageHeight { get; set; }

        //     A System.Double that represents the distance from the left edge of the page to
        //     the left edge of the imageable area in pixels (1/96 of an inch).
        public double OriginWidth { get; set; }
        
        //     A System.Double that represents the distance from the top edge of the page to
        //     the top of the imageable area in pixels (1/96 of an inch).
        public double OriginHeight { get; set; }
       
        //     A System.Double that represents the width of the imageable area in pixels (1/96
        //     of an inch).
        public double ExtentWidth { get; set; }
        
        //     A System.Double that represents the height of the imageable area in pixels (1/96
        //     of an inch).
        public double ExtentHeight { get; set; }
       
        //     A System.String that represents the property values of the System.Printing.PageImageableArea.
        public override string ToString()
        {
            return $"{nameof(OriginWidth)}:{OriginWidth};{nameof(OriginHeight)}:{OriginHeight};{nameof(ExtentWidth)}:{ExtentWidth};{nameof(ExtentHeight)}:{ExtentHeight}";
        }

        public static PageConfiguration GetA4Page()
        {
            // ((X mm) / (25.4mm/in)) x (Y pixels/in)
            var horizontal = (210 / 25.4) * 96;
            var vertical = (297 / 25.4) * 96;
            var originH = (10 / 25.4) * 96;
            var originV = originH;

            PageConfiguration pageConfiguration = new PageConfiguration
            {
                OriginWidth = originH,
                OriginHeight = originV,
                ExtentWidth = horizontal - 2 * originH,
                ExtentHeight = vertical - 2 * originV,
                PageWidth = horizontal,
                PageHeight = vertical
            };

            return pageConfiguration;
        }
    }
}
