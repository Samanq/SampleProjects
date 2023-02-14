using Xunit;
using XUnitSample.Infrastructure.Services;

namespace XUnitSample.Tests;

[Collection("Ship collection")]
public class ShipTest : IClassFixture<Ship>
{
    private readonly Ship _shipFixture;

	public ShipTest(Ship ship)
	{
		// Instantiate once in ShipFixture class
		_shipFixture = ship;
	}

	[Fact]
	public void Start_OnExecute_ChangeStateToStart()
	{
		_shipFixture.Start();
		Assert.Equal("Ship has started", _shipFixture.CurrentState);
	}

    [Fact]
    public void TurnOff_OnExecute_ChangeStateToTurnOff()
    {
        _shipFixture.TurnOff();
        Assert.Equal("Ship has turned off", _shipFixture.CurrentState);
    }
}
