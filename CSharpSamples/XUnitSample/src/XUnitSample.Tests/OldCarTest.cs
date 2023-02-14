using Xunit;
using XUnitSample.Infrastructure.Services;

namespace XUnitSample.Tests;

public class OldCarTest
{
    private readonly OldCar _sut;
    public OldCarTest()
    {
        // Runs before every task
        _sut = new OldCar();
    }

    [Fact]
    public void Start_OnExecute_ChangeCurrentState()
    {
        _sut.Start();
        Assert.Contains("car", _sut.CurrentState);
    }

    [Fact]
    public void TurnOf_OnExecute_ChangeCurrentState()
    {
        _sut.TurnOff();
        Assert.Contains("car", _sut.CurrentState);
    }
}
