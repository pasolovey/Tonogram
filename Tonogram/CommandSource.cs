using ICSharpCode.AvalonEdit;
using System.Collections.Generic;
using Tonogram.Commands;

namespace Tonogram
{
    public class CommandSource
    {
        private readonly TextEditor avalon;

        public CommandSource(TextEditor avalon)
        {
            this.avalon = avalon;
        }

        public IEnumerable<IToolCommand> GetToolCommands()
        {
            yield return new NonStressCommand(avalon);
            yield return new StressCommand(avalon);
            yield return new HighFallCommand(avalon);
            yield return new MidFallCommand(avalon);
            yield return new LowFallCommand(avalon);

            yield return new LowRiseCommand(avalon);
            yield return new MidRiseCommand(avalon);
            yield return new HighRiseCommand(avalon);

            yield return new FallRiseCommand(avalon);
            yield return new MidFallRiseCommand(avalon);
            yield return new RiseFallCommand(avalon);
            yield return new SpecialRiseCommand(avalon);

            yield return new ShortPauseCommand(avalon);
            yield return new LongPauseCommand(avalon);
            yield return new BreathCommand(avalon);
        }
    }
}
