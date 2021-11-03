using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwaggerAPI.DALServer;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace SwaggerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="schoolUserInfoContext"></param>
        public UserController(UserInfoServer schoolUserInfoContext)
        {
            _userInfoContext = schoolUserInfoContext;

        }
        private readonly UserInfoServer _userInfoContext;


        /// <summary>
        /// 查询视图
        /// </summary>
        /// <returns></returns>
        [HttpGet("{GetTable}/View_USERINFO")]
        public List<UserInfo> Get_View_Userinfo()
        {
            return _userInfoContext.UserInfos .FromSqlRaw("SELECT * FROM  View_UserInfo ").ToList();
        }

        
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("ProUser")]
        public List<UserInfo> ProUser(string username)
        {
            return _userInfoContext.UserInfos.FromSqlRaw("EXECUTE  UP_UserInfo {0}", username).ToList();
        }

        /// <summary>
        /// 传json 返回json
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        [HttpGet("{Json}")]
        public string Get_Json(string searchTerm)
        {

            /*
            List<UserInfo> jobInfoList = JsonSerializer.Deserialize<List<UserInfo>>(searchTerm);//将字符转化为List
            var searchTerm1 = $"where UserName ='{jobInfoList[0].UserName}'";
            */

            var testType = JsonSerializer.Deserialize<UserInfo>(searchTerm);//将字符转化为List

            var linqs = _userInfoContext.UserInfos.Where(x => x.UserName.Contains(testType.UserName)).Skip((1 - 1) * 5).Take(5).OrderByDescending(m => m.Id).ToList();//.OrderByDescending(m => m.Id).ToList();//
           
            List<UserInfo> list = linqs;

            return  JsonSerializer.Serialize(linqs,
            options: new System.Text.Json.JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }
            );
            //return JsonSerializer.Serialize("aaaaa");
        }


        /// <summary>
        ///  传字符串 返回json
        /// </summary>
        /// <param name="searchTerm">姓名</param>
        /// <param name="pick">第几页</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        [HttpGet("{SendstringToJson}")]
        public string Out_Json(string searchTerm,int pick,int size)
        {
            //var testType = JsonSerializer.Deserialize<UserInfo>(searchTerm);//将字符转化为List

            var linqs = _userInfoContext.UserInfos.Where(x => x.UserName.Contains(searchTerm))
                .Skip((pick - 1) * size).Take(size).OrderByDescending(m => m.Id).ToList();

            List<UserInfo> list = linqs;

            return JsonSerializer.Serialize(linqs,
            options: new System.Text.Json.JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }
            );
        }

    }
}
