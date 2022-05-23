using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NetCoreSnippets.Concurrency
{
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
        
        public Task DoSomethingAsync() =>
            Task.CompletedTask;

        public async Task WhenAllAsync()
        {
            var task1 = Morpheus(1, "First task");
            var task2 = Morpheus(2, "Second Task");
            var task3 = Morpheus(3, "Third Task");

            await Task.WhenAll(task1, task2, task3).ConfigureAwait(false);  
        }

        /* Waiting for a set of tasks to complete */
        public async Task WhenAllAsyncLinq()
        {
            var list = new List<(int Seconds,string Message)>() 
                { (1, "First Task"), (2, "Second Task"), (3, "Third Task") };
        
            /* Define the action to do */
            var unstartedTasks = list.Select(i => Morpheus(i.Seconds, i.Message));
        
            /* Start the tasks simultaneously */
            var startedTasks = unstartedTasks.ToArray();

            /* Asynchronously wait for all tasks to complete*/
            await Task.WhenAll(startedTasks).ConfigureAwait(false); // Finished tasks
        }

        public async Task WhenAnyAsync()
        {
            var task1 = DelayAndReturnStringAsync(1, "First WhenAny task");
            var task2 = DelayAndReturnStringAsync(2, "Second WhenAny Task");
            var task3 = DelayAndReturnStringAsync(3, "Third WhenAny Task");

            var completedTask = await Task.WhenAny(task1, task2, task3).ConfigureAwait(false);
            var result = await completedTask.ConfigureAwait(false);
            _logger.LogInformation($"First Task to finalize is : {result}");
        }

        public async Task ProcessTasksAsync()
        {
            var taskA = DelayAndReturnStringAsync(2,"Task A");
            var taskB = DelayAndReturnStringAsync(3, "Task B");
            var taskC = DelayAndReturnStringAsync(1, "Task C");

            var tasks = new List<Task<string>>() { taskA, taskB, taskC };

            var processingTasks = tasks.Select(async t =>
            {
                var result = await t.ConfigureAwait(false);
                _logger.LogInformation(result);
            }).ToArray();
        
            await Task.WhenAll(processingTasks).ConfigureAwait(false);
        }

        public async Task HandlingExceptionAsync()
        {
            try
            {
                await ThrowExceptionAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
        
        public async Task HandlingAggregateExceptionAsync()
        {
            var taskA = ThrowExceptionAsync();
            var taskB = ThrowExceptionAsync();
            var taskC = ThrowExceptionAsync();
            var allTasks = Task.WhenAll(taskA, taskB, taskC);
            try
            {
                await allTasks;
            }
            catch
            {
                _logger.LogError(allTasks.Exception.Message);
            }
        }

        private Task Morpheus(int seconds, string message)
        {
            _logger.LogInformation(message);
            return Task.Delay(TimeSpan.FromSeconds(seconds));
        } 

        private static async Task<string> DelayAndReturnStringAsync(int seconds, string message)
        {
            await Task.Delay(TimeSpan.FromSeconds(seconds)).ConfigureAwait(false);
            return message;
        }

        private async Task ThrowExceptionAsync()
        {
            await Morpheus(2, "Exception");
            throw new Exception("Throwing an exception");
        }
    }
}