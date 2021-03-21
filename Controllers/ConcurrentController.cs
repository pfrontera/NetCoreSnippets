using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
    }
}
