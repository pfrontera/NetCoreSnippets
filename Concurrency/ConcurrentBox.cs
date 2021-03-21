using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public class ConcurrentBox : IConcurrentBox
{
    private readonly ILogger<ConcurrentBox> _logger;
    
    public ConcurrentBox(ILogger<ConcurrentBox> logger)
    {
        _logger = logger;
    }

    public Task<string> GetValueAsync()
    {
        var message = "Hello Newman...";
        _logger.LogInformation($"Returning : {message}");
        return Task.FromResult(message);
    }
        

    /* Returning a completed Task without a value */
    public Task DoSomethingAsync() =>
        Task.CompletedTask;

    /* Get Progress about a Task */
    public async Task ProgressTask()
    {
        //TODO
    }

    /* Waiting for a set of tasks to complete */
    public async Task WhenAllAsync()
    {
        var task1 = Morpheus("First task");
        var task2 = Morpheus("Second Task");
        var task3 = Morpheus("Third Task");

        await Task.WhenAll(task1, task2, task3);  
    }

    /* Waiting for a set of tasks to complete */
    public async Task WhenAllAsyncLinq()
    {
        var list = new List<string>() { "First Task", "Second Task", "Third Task" };
        
        /* Define the action to do */
        var unstartedTasks = list.Select(x => Morpheus(x));
        
        /* Start the tasks simultaneously */
        var startedTasks = unstartedTasks.ToArray();

        /* Asynchronously wait for all tasks to complete*/
        await Task.WhenAll(startedTasks); // Finished tasks
    }

    /* Waiting for the first task to complete */
    public async Task WhenAnyAsync()
    {
        var task1 = ReturnStringAsync("First WhenAny task");
        var task2 = ReturnStringAsync("Second WhenAny Task");
        var task3 = ReturnStringAsync("Third WhenAny Task");

        var completedTask = await Task.WhenAny(task1, task2, task3);
        var result = await completedTask;
        _logger.LogInformation($"First Task to finalize is : {result}");
    }

    private Task Morpheus(string message)
    {
        _logger.LogInformation(message);
        return Task.Delay(2000);
    } 

    private Task<string> ReturnStringAsync(string message) =>
        Task.FromResult(message);
}