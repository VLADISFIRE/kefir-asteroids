using System;

namespace Infrastructure.ECS
{
    public sealed partial class World : IDisposable
    {
        public void Dispose()
        {
            DisposeEntity();
            DisposeComponents();
            DisposeMasks();
        }
    }
}