﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROS.Implement.Repository
{
    public interface IAuthRepository
    {
        string Login();
    }
    public class AuthRepository : IAuthRepository
    {
        public string Login()
        {
            return "Login";
        }
    }
}
