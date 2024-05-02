using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangmo.Services.Interfaces
{
    public interface IGameService
    {
        (bool, List<(int, char)>) FindLetter(string palavra, char letra);
    }
}
