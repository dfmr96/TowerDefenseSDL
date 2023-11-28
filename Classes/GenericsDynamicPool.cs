using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Classes
{
    public class GenericsDynamicPool<T> where T : class
    {
        public List<T> activeList = new List<T>();
        public List<T> notActiveList = new List<T>();

        public T GetNewT(T tBasic)
        {
            T newT = null;

            if (notActiveList.Count > 0)
            {
                newT = notActiveList[0];
                notActiveList.RemoveAt(0);
            }
            else
            {
                newT = tBasic;
            }
            return newT;
        }

        public void RecycleT(T tParameter)
        {
            activeList.Remove(tParameter);
            notActiveList.Add(tParameter);
        }

        public void PrintBullets()
        {
            Engine.Debug("Activadas " + activeList.Count);
            Engine.Debug("Desactivadas " + notActiveList.Count);
        }
    }
}
