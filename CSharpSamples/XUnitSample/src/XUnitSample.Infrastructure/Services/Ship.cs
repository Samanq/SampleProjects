namespace XUnitSample.Infrastructure.Services;

public class Ship
{
	public string CurrentState { get; set; } = string.Empty;

    public Ship()
	{
		Thread.Sleep(2);
	}


	public void Start()
	{
		CurrentState = "Ship has started";
	}
    public void TurnOff()
    {
        CurrentState = "Ship has turned off";
    }
}
