using System.Threading.Tasks;

public interface IConcurrentBox
{
    Task<string> GetValueAsync();
    Task DoSomethingAsync();
    Task ProgressTask();
    Task WhenAllAsync();
    Task WhenAllAsyncLinq();
    Task WhenAnyAsync();
    Task ProcessTasksAsync();
}