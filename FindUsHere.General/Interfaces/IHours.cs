namespace FindUsHere.General.Interfaces
{
    public interface IHours
    {
        TimeSpan Time_Open { get; }
        TimeSpan Time_Closed { get; }
        string Day { get; }
    }
}
