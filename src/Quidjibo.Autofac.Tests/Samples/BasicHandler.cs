using System;
using System.Threading;
using System.Threading.Tasks;
using Quidjibo.Handlers;
using Quidjibo.Models;

namespace Quidjibo.Autofac.Tests.Samples
{
    public class BasicHandler : IQuidjiboHandler<BasicCommand>
    {
        public Task ProcessAsync(BasicCommand command, IQuidjiboProgress progress, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}