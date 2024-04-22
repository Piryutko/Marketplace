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
using Grpc.Net.Client;

namespace UserRegistrationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;
        private readonly IUserClient _userClient;

        public UserController(IUserRepository userRepository, IMapper mapper, 
        IMessageBusClient messageBusClient, IUserClient userClient)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _messageBusClient = messageBusClient;
            _userClient = userClient;
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

                
                if(_userClient.GetResultRequestById(user.Id.ToString(), out string result))
                {
                    if(result == "True")
                    {
                        return Ok("Вы успешно зарегистрировались!");
                    }
                }

                return BadRequest("Вы не прошли регистрацию! Никнейм или почта уже занят другим пользователем");
            }
            else
            {
                return BadRequest("Вы не прошли регистрацию! Проверьте правильность вводимых данных");
            }
                
            }
            catch (Exception ex)
            {
                 return BadRequest($"Вы не прошли регистрацию! Обратитесь в техническую поддержку магазина.");
            }
            
        }

    }
}