using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreSnippets.Concurrency;

namespace NetCoreSnippets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConcurrentController : ControllerBase
    {
        private readonly IConcurrentBox _concurrentBox;

        public ConcurrentController(IConcurrentBox concurrentBox)
        {
            _concurrentBox = concurrentBox;

        }

        /// <summary>    
        /// Returning a value from a completed Task.  
        /// </summary>     
        [HttpGet]
        [Route("GetValueAsync")]
        public async Task<string> GetValueAsync()
        {
            return await _concurrentBox.GetValueAsync();
        }

        /// <summary>    
        /// Returning a completed Task without a value.  
        /// </summary>     
        [HttpGet]
        [Route("DoSomethingAsync")]
        public Task DoSomethingAsync()
        {
            return _concurrentBox.DoSomethingAsync();
        }

        /// <summary>    
        /// Waiting for a set of tasks to complete.  
        /// </summary>     
        [HttpGet]
        [Route("WhenAllAsync")]
        public Task WhenAllAsync()
        {
            return _concurrentBox.WhenAllAsync();
        }

        /// <summary>    
        /// Waiting for a set of tasks to complete that evaluates through linq.  
        /// </summary>     
        [HttpGet]
        [Route("WhenAllAsyncLinq")]
        public Task WhenAllAsyncLinq()
        {
            return _concurrentBox.WhenAllAsyncLinq();
        }

        /// <summary>    
        /// Waiting for the first task to complete.  
        /// </summary>     
        [HttpGet]
        [Route("WhenAnyAsync")]
        public Task WhenAnyAsync()
        {
            return _concurrentBox.WhenAnyAsync();
        }

        /// <summary>    
        /// Processing Tasks as they complete doing some processing on each task after it completes.  
        /// </summary>     
        [HttpGet]
        [Route("ProcessTasksAsync")]
        public Task ProcessTasksAsync()
        {
            return _concurrentBox.ProcessTasksAsync();
        }
        
        /// <summary>    
        /// Handling an exception from an async Task method.  
        /// </summary>     
        [HttpGet("HandlingExceptionAsync")]
        public Task HandlingExceptionAsync()
        {
            return _concurrentBox.HandlingExceptionAsync();
        }
        
        /// <summary>    
        /// Handling all exception from a Task list.  
        /// </summary>     
        [HttpGet]
        [Route("HandlingAggregateExceptionAsync")]
        public Task HandlingAggregateExceptionAsync()
        {
            return _concurrentBox.HandlingAggregateExceptionAsync();
        }
    }
}
