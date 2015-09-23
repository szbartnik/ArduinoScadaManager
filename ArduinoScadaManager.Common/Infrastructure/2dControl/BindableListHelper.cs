using System.Collections.Generic;

namespace ArduinoScadaManager.Common.Infrastructure._2dControl
{
    public static class BindableListHelper
    {
        public static List<List<Ref<T>>> GetBindable2DList<T>(List<List<T>> list)
        {
            List<List<Ref<T>>> refInts = new List<List<Ref<T>>>();

            for (int i = 0; i < list.Count; i++)
            {
                refInts.Add(new List<Ref<T>>());
                for (int j = 0; j < list[i].Count; j++)
                {
                    int a = i;
                    int b = j;
                    refInts[i].Add(new Ref<T>(() => list[a][b], z => { list[a][b] = z; }));
                }
            }
            return refInts;
        }
    }
}
