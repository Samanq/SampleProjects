using Xunit;

namespace XUnitSample.Tests;

[Collection("Ship collection")]
public class ShipDeepTest
{
    private readonly ShipFixture _shipFixture;

    public ShipDeepTest(ShipFixture ship)
    {
        // Instantiate once in ShipFixture class
        _shipFixture = ship;
    }

    [Fact]
    public void Start_OnExecute_ChangeStateToStart()
    {
        _shipFixture.Ship.Start();
        Assert.Equal("Ship has started", _shipFixture.Ship.CurrentState);
    }

    [Fact]
    public void TurnOff_OnExecute_ChangeStateToTurnOff()
    {
        _shipFixture.Ship.TurnOff();
        Assert.Equal("Ship has turned off", _shipFixture.Ship.CurrentState);
    }
}
