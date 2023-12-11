namespace Todo;

public class Todo
{
    public Guid TaskId {get;set;}  = Guid.NewGuid();

    public string? Task {get;set;}

    public string? Start {get;set;}

    public string? Duration {get;set;}

}
