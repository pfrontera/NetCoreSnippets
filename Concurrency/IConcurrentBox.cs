using System.Threading.Tasks;

namespace NetCoreSnippets.Concurrency;

public interface IConcurrentBox
{
    Task<string> GetValueAsync();
    Task DoSomethingAsync();
    Task WhenAllAsync();
    Task WhenAllAsyncLinq();
    Task WhenAnyAsync();
    Task ProcessTasksAsync();
    Task HandlingExceptionAsync();
    Task HandlingAggregateExceptionAsync();
}