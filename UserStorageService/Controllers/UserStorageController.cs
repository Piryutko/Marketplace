using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserStorageService.Interfaces;
using UserStorageService.Models;

namespace UserStorageService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserStorageController : ControllerBase
    {
        private readonly IUserFacade _userFacade;

        public UserStorageController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [HttpGet("GetAllUsers")]
        public IEnumerable<User> GetAllUsers()
        {
            return _userFacade.GetAllUsers();
        }

        [HttpGet("UserExists/{id}")]
        public bool UserExists(Guid Id)
        {
            return _userFacade.UserExists(Id);
        }

        [HttpGet("GetUserByNickname/{Nickname}")]
        public User GetUserByNickname(string Nickname)
        {
           return _userFacade.GetUserByNickname(Nickname);
        }

        [HttpGet("GetUserByEmail/{Email}")]
        public User GetUserByEmail(string Email)
        {
            return _userFacade.GetUserByEmail(Email);
        }

        [HttpGet("GetUserById/{id}")]
        public User GetUserById(Guid Id)
        {
           return _userFacade.GetUserById(Id);
        }

        [HttpGet("DeleteUserById/{id}")]
        public bool TryDeleteUserById(Guid Id)
        {
           return _userFacade.TryDeleteUserById(Id);
        }


    }
}