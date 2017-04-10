﻿using System;
using Autofac;
using Quidjibo.Resolvers;

namespace Quidjibo.Autofac.Resolvers
{
    public class AutofacPayloadResolver : IPayloadResolver
    {
        private readonly ILifetimeScope _lifetimeScope;
        private ILifetimeScope _nestedLifetimeScope;

        public IDisposable Begin()
        {
            _nestedLifetimeScope = _lifetimeScope.BeginLifetimeScope();
            return _nestedLifetimeScope;
        }

        public object Resolve(Type type)
        {
            return _nestedLifetimeScope.Resolve(type);
        }

        public void Dispose()
        {
            _nestedLifetimeScope?.Dispose();
        }

        public AutofacPayloadResolver(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }
    }
}