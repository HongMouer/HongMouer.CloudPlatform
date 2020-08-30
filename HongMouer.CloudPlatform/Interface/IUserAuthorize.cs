using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HongMouer.EHR.Models;

namespace HongMouer.CloudPlatform
{
    public interface IUserAuthorize:IDenpendency
    {
        /// <summary>
        /// 根据用户名密码进行验证
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        ResponseMessage<UserInfo> UserVerification(string userNum, string userPwd);
    }
}
