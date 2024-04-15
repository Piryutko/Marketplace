using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using UserStorageService.Dtos;
using UserStorageService.Enums;
using UserStorageService.Interfaces;
using UserStorageService.Models;

namespace UserStorageService.Event
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
            switch (eventType)
            {
                case UserEvents.UserPublisher:
                AddUser(message);
                break;

                default:
                    break;
            }
        }

        private UserEvents DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");
            
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case UserEvents.UserPublisher:
                Console.WriteLine("--> User Published Event Detected");
                return UserEvents.UserPublisher;
                
                default:
                Console.WriteLine("--> Could not determine the event type");
                return UserEvents.Undetermined;
            }
        }

        private void AddUser(string userPublisherMessage)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                
                var userPublishedDto = JsonSerializer.Deserialize<UserPublishedDto>(userPublisherMessage);

                try
                {
                    var user = _mapper.Map<User>(userPublishedDto);

                    repo.AddUser(user);
                    repo.SaveChange();
                    
                    Console.WriteLine("Success!");
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine($"--> Could not add User to DB {ex.Message}");
                }

            }
        }



    }
}