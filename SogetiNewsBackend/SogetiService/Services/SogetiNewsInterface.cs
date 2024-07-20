using Refit;
using SogetiService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiNewsConsoleTest
{
    internal interface ISogetiNewsInterface
    {
         
        [Get("rest of your url")]
        Task<Rootobject> GetSogetiNews();
    }
}
