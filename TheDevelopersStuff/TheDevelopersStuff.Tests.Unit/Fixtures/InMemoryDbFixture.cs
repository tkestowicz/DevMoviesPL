using System;
using Simple.Data;

namespace TheDevelopersStuff.Tests.Unit.Fixtures
{
    public class InMemoryDbFixture : IDisposable
    {

        public InMemoryDbFixture()
        {
            Database.UseMockAdapter(new InMemoryAdapter());
        }

        public void Dispose()
        {
            Database.StopUsingMockAdapter();
        }
    }
}