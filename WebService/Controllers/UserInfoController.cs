using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DALServer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        public UserInfoController(UserInfoServer schoolUserInfoContext)
        {
            _userInfoContext = schoolUserInfoContext;

        }
        // GET: api/<UserInfoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        private readonly UserInfoServer _userInfoContext;
            
        public List<UserInfo> GetUserInfo(int page = 1, int limit = 5, string userName = "")
        {
            var totalCount = 0;
            List<UserInfo> listData = null;
            if (!string.IsNullOrWhiteSpace(userName))
            {
                listData = _userInfoContext.UserInfos.Where(x => x.UserName.Contains(userName)).OrderByDescending(m => m.Id).ToList();//.OrderByDescending(m => m.Id).ToList();//.Skip((page - 1) * limit).Take(limit).ToList();
                totalCount = _userInfoContext.UserInfos.Count(x => x.UserName.Contains(userName));
            }
            return listData;
        }
        // GET api/<UserInfoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserInfoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserInfoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserInfoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
