using FluentAssertions;
using FluentAssertions.Extensions;
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
    public void Ship_OnInitialization_PopulateDate()
    {
        DateTime expectedDate = new DateTime(2020, 2, 15, 14, 20, 01);
        var date = 15.February(2020);

        _shipFixture.CreationDate.Should().BeOnOrAfter(expectedDate);
        _shipFixture.CreationDate.Should().BeOnOrBefore(expectedDate);
        _shipFixture.CreationDate.Should().Be(expectedDate);
        _shipFixture.CreationDate.Should().BeSameDateAs(date);
        //_shipFixture.CreationDate.Should().Be(15.February(2020).At(14, 20));
        ////_shipFixture.CreationDate.Should().Be(1.Days().Before(16.February(2020)));
        _shipFixture.CreationDate.Should().HaveYear(2020);
        _shipFixture.CreationDate.Should().HaveMonth(2);
        _shipFixture.CreationDate.Should().HaveDay(15);
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
