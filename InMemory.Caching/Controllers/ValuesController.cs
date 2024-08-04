using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace InMemory.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        //[HttpGet("set/{name}")]
        //public void SetName(string name)
        //{
        //    _memoryCache.Set("name", name);

        //}

        //[HttpGet]
        //public string GetName()
        //{
        //    if (_memoryCache.TryGetValue<string>("name", out string name))
        //    {
        //        return name.Substring(0, name.Length - 1);
        //    }
        //    return "";
        //}

        [HttpGet("setDate")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration=TimeSpan.FromSeconds(5)
            });
        }

        [HttpGet("getDate")]
        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }



        [HttpGet("setDist")]
        public  IActionResult Set(string name, string surname)
        {
             _memoryCache.Set("name", name, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(15),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
             _memoryCache.Set("surname", surname, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(15),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
            return Ok();
        }

        [HttpGet("getDist")]
        public IActionResult Get()
        {
            var name =  _memoryCache.Get("name");
            var surname = _memoryCache.Get("surname");
            return Ok(new
            {
                name,
                surname
            });
        }

    }
}