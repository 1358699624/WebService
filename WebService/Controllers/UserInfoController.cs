using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwaggerAPI.DALServer;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SwaggerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        public UserInfoController(UserInfoServer schoolUserInfoContext)
        {
            _userInfoContext = schoolUserInfoContext;

        }
        private readonly UserInfoServer _userInfoContext;

        /// <summary>
        /// tdk
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("{GetTable}")]
        public List<UserInfo> GetUserInfo(int page = 1, int limit = 5, string userName = "")
        {
            var totalCount = 0;
            List<UserInfo> listData = null;
            if (!string.IsNullOrWhiteSpace(userName))
            {
                listData = _userInfoContext.UserInfos.Where(x => x.UserName.Contains(userName)).Skip((page - 1) * limit).Take(limit).OrderByDescending(m => m.Id).ToList();//.OrderByDescending(m => m.Id).ToList();//
                totalCount = _userInfoContext.UserInfos.Count(x => x.UserName.Contains(userName));
            }
            else
            {
                listData = _userInfoContext.UserInfos.Skip((page - 1) * limit).Take(limit).OrderByDescending(m => m.Id).ToList();//.OrderByDescending(m => m.Id).ToList();//
            }
            return listData;
        }

        /// <summary>
        /// 自带的保存
        /// </summary>
        /// <param name="userInfos"></param>
        /// <returns></returns>
        //新增数据 insert
        [HttpPost]
        public bool Post([FromBody] UserInfo userInfos)
        {
            _userInfoContext.UserInfos.Add(userInfos);
            return _userInfoContext.SaveChanges() > 0 ? true : false;
        }

        /// <summary>
        /// 自带的保存
        /// </summary>
        /// <param name="userInfos"></param>
        /// <returns></returns>
        // PUT api/<UserInfoController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] UserInfo userInfos)
        {

            _userInfoContext.UserInfos.Update(userInfos);
            return _userInfoContext.SaveChanges() > 0 ? true : false;
        }
        /// <summary>
        /// 自带的删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userInfos"></param>
        /// <returns></returns>
        // DELETE api/<UserInfoController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id, [FromBody] UserInfo userInfos)
        {
            _userInfoContext.UserInfos.Remove(userInfos);
            return _userInfoContext.SaveChanges() > 0 ? true : false;
        }
        
    }
}
