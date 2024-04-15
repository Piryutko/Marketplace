using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserRegistrationService.Models;
using UserRegistrationService.Interfaces;
using UserRegistrationService.Repositories;
using AutoMapper;
using UserRegistrationService.Dtos;
using UserRegistrationService.Enums;

namespace UserRegistrationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;

        public UserController(IUserRepository userRepository, IMapper mapper, IMessageBusClient messageBusClient)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _messageBusClient = messageBusClient;
        }
        

        [HttpGet("GetAllUsers")]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            Console.WriteLine("---> Get All Users");

            return Ok(_userRepository.GetAllUsers());
        }

        [HttpPost("TryCreateUser")]
        public ActionResult TryCreateUser(User user)
        {
            if(_userRepository.TryCreateUser(user))
            {
                return Ok("Вы успешно зарегистрировались!");
            }
            else
            {
                return BadRequest("Вы не прошли регистрацию! Проверьте правильность вводимых данных");
            }
        }

        [HttpPost("SendMessageTryCreateUser")]
        public ActionResult CreateUser(User user)
        {
            try
            {
            if(_userRepository.TryCreateUser(user))
            {
                var userPublishedDto = _mapper.Map<UserPublishedDto>(user);
                userPublishedDto.Event = UserEvents.User_Published;
                _messageBusClient.PublishNewUser(userPublishedDto);

                return Ok("Вы успешно зарегистрировались!");
            }
            else
            {
                return BadRequest("Вы не прошли регистрацию! Проверьте правильность вводимых данных");
            }
                
            }
            catch (Exception ex)
            {
                 return BadRequest($"Вы не прошли регистрацию! Ошибка: {ex.Message}");
            }
            
        }

        
    }
}