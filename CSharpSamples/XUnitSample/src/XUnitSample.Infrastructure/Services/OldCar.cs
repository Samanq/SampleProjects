namespace XUnitSample.Infrastructure.Services;

public class OldCar
{
    public string Name { get; set; } = string.Empty;
	public string CurrentState { get; set; } = string.Empty;

	public OldCar()
	{
		Thread.Sleep(2000);
	}

	public void Start()
	{
		CurrentState =  "The car has started.";
	}
    public void TurnOff()
    {
        CurrentState = "The car has turned off.";
    }
}
