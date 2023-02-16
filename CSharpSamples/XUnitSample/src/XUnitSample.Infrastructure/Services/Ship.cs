namespace XUnitSample.Infrastructure.Services;

public class Ship
{
	public string CurrentState { get; set; } = string.Empty;

	public DateTime CreationDate { get; set; } = new DateTime(2020, 2, 15, 14, 20, 01);

	public Ship()
	{
		Thread.Sleep(1000);
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
