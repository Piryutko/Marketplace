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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserStorageController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMessage()
        {
            return Ok("Успешно подключились");
        }

        [HttpGet("GetAllUsers")]
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        [HttpGet("UserExists/{id}")]
        public bool UserExists(Guid id)
        {
            return _userRepository.UserExists(id);
        }

        [HttpGet("GetUserByNickname/{Nickname}")]
        public User GetUserByNickname(string Nickname)
        {
           var result = _userRepository.TryFindUserByNickName(Nickname, out User user);

           if(result != null)
           {
                return user;
           }

           return user;
        }

        [HttpGet("GetUserByEmail/{Email}")]
        public User GetUserByEmail(string Email)
        {
           var result = _userRepository.TryFindUserByEmail(Email, out User user);

           if(result != null)
           {
                return user;
           }

           return user;
        }

        [HttpGet("GetUserById/{id}")]
        public User GetUserById(Guid Id)
        {
           var result = _userRepository.TryFindUserById(Id, out User user);

           if(result != null)
           {
                return user;
           }

           return user;
        }

        [HttpGet("DeleteUserById/{id}")]
        public bool DeleteUserById(Guid Id)
        {
           return _userRepository.TryDeleteUserById(Id);
        }


    }
}