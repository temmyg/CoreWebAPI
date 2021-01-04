using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApi.Services
{
    public class TransientDisposable : IDisposable
    {
        public void Dispose() => Console.WriteLine($"{nameof(TransientDisposable)}.Dispose()");
    }
}
