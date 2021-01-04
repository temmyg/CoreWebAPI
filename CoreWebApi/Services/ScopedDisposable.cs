using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApi.Services
{
    public class ScopedDisposable : IDisposable
    {
        public void Dispose() => Console.WriteLine($"{nameof(ScopedDisposable)}.Dispose()");
    }
}
