using System;
using System.Collections.Generic;

namespace Hunting_for_fish
{
    class Observer
    {
        private List<ISubject> subjects = new List<ISubject>();

        public void AddObserver(IObserver observer)
        {
            for (int i = 0; i < subjects.Count; i++)
            {
                subjects[i].AddObserver(observer);
            }
        }

        public void AddSubject(ISubject subject)
        {
            subjects.Add(subject);
        }
    }
}
