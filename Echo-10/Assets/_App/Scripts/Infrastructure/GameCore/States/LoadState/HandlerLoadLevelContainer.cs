using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _App.Scripts.Infrastructure.GameCore.States.LoadState
{
    public class HandlerLoadLevelContainer : IHandlerLoadLevel
    {
        private readonly List<IHandlerLoadLevel> _handlers;

        public HandlerLoadLevelContainer(IEnumerable<IHandlerLoadLevel> handlerSetupLevels)
        {
            _handlers = handlerSetupLevels.ToList();
        }

        public async Task Process()
        {
            foreach (var handler in _handlers) await handler.Process();
        }
    }
}