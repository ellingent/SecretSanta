﻿using Autofac;
using SecretSanta.Domain.Interfaces.Facades;
using SecretSanta.Domain.Mappers;

namespace Santa {
    class Program
    {
        static void Main(string[] args)
        {
            MapperConfig.Init();
            var container = IocConfig.Build();
            using (var scope = container.BeginLifetimeScope()) {
                var santa = scope.Resolve<ISantaFacade>();
                santa.DistributeHolidayCheer();
            }
        }
    }
}
