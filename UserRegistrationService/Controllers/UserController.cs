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
using UserRegistrationService.Exceptions;

namespace UserRegistrationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserFacade _userFacade;

        public UserController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }


        [HttpPost("UserRegistration")] //http://localhost:5000/api/user/UserRegistration
        public ActionResult UserRegistration(User user)
        {
            try
            {
                var result = _userFacade.TryUserRegistration(user);

                return Ok(result);
            }
            catch (GrpcServerUnavailableException)
            {
                return BadRequest();
            }
        }

    }
}