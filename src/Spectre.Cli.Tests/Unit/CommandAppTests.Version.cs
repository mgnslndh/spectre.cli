using Shouldly;
using Spectre.Cli.Testing;
using Spectre.Cli.Testing.Data.Commands;
using Spectre.Cli.Testing.Data.Settings;
using Xunit;

namespace Spectre.Cli.Tests
{
    public sealed partial class CommandAppTests
    {
        public sealed class Version
        {
            [Fact]
            public void Should_Output_The_Version_To_The_Console()
            {
                // Given
                var fixture = new CommandAppFixture();
                fixture.Configure(config =>
                {
                    config.AddBranch<AnimalSettings>("animal", animal =>
                    {
                        animal.AddBranch<MammalSettings>("mammal", mammal =>
                        {
                            mammal.AddCommand<DogCommand>("dog");
                            mammal.AddCommand<HorseCommand>("horse");
                        });
                    });
                });

                // When
                var (_, output, _, _) = fixture.Run(Constants.VersionCommand);

                // Then
                output.ShouldStartWith("Spectre.Cli version ");
            }
        }
    }
}