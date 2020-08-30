using HongMouer.EHR.Models;
using HongMouer.EntityRelationalCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HongMouer.CloudPlatform.Services
{
    public class UserAuthorizeService : IUserAuthorize
    {

        public string Name { get { return "个人身份验证"; } }

        public string Module { get { return "登录"; } }

        public string GroupName { get { return "登录"; } }

        public string Remark { get { return "个人身份验证"; } }

        public string Version { get { return "V1.0"; } }

        private readonly IRepository _Repository;

        public UserAuthorizeService(IRepository repository)
        {
            _Repository = repository;
        }

        public ResponseMessage<UserInfo> UserVerification(string userNum, string userPwd)
        {

            return new ResponseMessage<UserInfo>(1,new  UserInfo());
        }

        public void Dispose()
        {
            _Repository.Dispose();
        }


    }
}
