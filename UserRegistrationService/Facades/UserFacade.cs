using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using UserRegistrationService.Dtos;
using UserRegistrationService.Enums;
using UserRegistrationService.Exceptions;
using UserRegistrationService.Interfaces;
using UserRegistrationService.Models;
using UserRegistrationService.Repositories;

namespace UserRegistrationService.Facades
{
    public class UserFacade : IUserFacade
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        private readonly IUserClient _userClient;

        private readonly IMessageBusClient _messageBusClient;

        public UserFacade(IUserRepository userRepository, IMapper mapper, IUserClient userClient, IMessageBusClient messageBusClient)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userClient = userClient;
            _messageBusClient = messageBusClient;
        }
        public Response TryUserRegistration(User user)
        {
            try
            {
                if (_userRepository.TryCreateUser(user))
                {
                    var userPublishedDto = _mapper.Map<UserPublishedDto>(user);
                    userPublishedDto.Event = UserEvents.User_Published;
                    _messageBusClient.PublishNewUser(userPublishedDto);


                    if (_userClient.GetResultRequestById(user.Id.ToString(), out string result))
                    {
                        if (result == "True")
                        {
                            return new Response { Status = "Success", Message = "Вы зарегистрировались!" };
                        }
                    }

                    return new Response { Status = "Fail", Message = "Никнейм или почта уже занят другим пользователем" };
                }
                else
                {
                    return new Response { Status = "Fail", Message = "Проверьте правильность вводимых данных" };
                }

            }
            catch (RpcException)
            {
                throw new GrpcServerUnavailableException();
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException();
            }
        }

    }
}