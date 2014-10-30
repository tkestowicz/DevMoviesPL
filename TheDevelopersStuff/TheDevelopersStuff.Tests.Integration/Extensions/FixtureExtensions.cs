using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace TheDevelopersStuff.Tests.Integration.Extensions
{
    public static class FixtureExtensions
    {
        public static TResult Resolve<TResult>(this IFixture fixture)
            where TResult: class 
        {
            var context = new SpecimenContext(fixture);
            return context.Resolve(typeof(TResult)) as TResult;
        }
    }
}
