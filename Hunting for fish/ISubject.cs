using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunting_for_fish
{
    interface ISubject
    {
        void AddObserver(IObserver observer);
        void Notify(string option);
    }
}
