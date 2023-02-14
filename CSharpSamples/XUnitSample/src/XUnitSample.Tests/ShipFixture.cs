using XUnitSample.Infrastructure.Services;

namespace XUnitSample.Tests;

public class ShipFixture : IDisposable
{
    public Ship Ship { get; private set; }

    public ShipFixture()
    {
        Ship = new Ship();
    }

    public void Dispose()
    {
        // It will runs after the last test.
    }
}
