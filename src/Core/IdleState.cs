﻿using Serilog;

namespace Notadesigner.Pomodour.Core
{
    public abstract class IdleState : IEngineState
    {
        protected readonly ILogger _logger = Log.ForContext<IdleState>();

        protected readonly NotificationsQueue _queue;

        protected readonly SemaphoreSlim _moveNextSignal = new(0);

        private readonly int _roundCounter;

        public IdleState(int roundCounter, NotificationsQueue queue)
        {
            _roundCounter = roundCounter;
            _queue = queue;
        }

        public abstract Task EnterAsync(CancellationToken cancellationToken);

        public void MoveNext()
        {
            _moveNextSignal.Release();
        }

        public int RoundCounter
        {
            get { return _roundCounter; }
        }

        public abstract EngineState State { get; }
    }
}