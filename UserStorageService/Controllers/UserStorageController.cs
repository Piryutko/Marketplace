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

        [HttpGet("GetUser{id}")]
        public bool UserExists(Guid id)
        {
            return _userRepository.UserExists(id);
        }


    }
}