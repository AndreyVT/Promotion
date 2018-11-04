﻿using Microsoft.Extensions.DependencyInjection;
using Promotion.DataBase;

namespace Promotion.Core.Component
{
    public class PBaseComponent: IBaseComponent
    {
        public virtual void Initialize()
        {
        }

        public virtual void PreInitialize()
        {

        }

        public virtual void PostInitialize()
        {

        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            throw new System.NotImplementedException();
        }
    }
}
