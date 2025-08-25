using System.Buffers;

namespace High_GC_Allocation;

public static class TestMethods
{
    public static void HighGCAllocationMethod()
    {
        for (int i = 0; i < 500000; i++)
        {
            var data = new long[100000];
    
            for (int j = 0; j < 100; j++)
            {
                data[j] = j;
            }
        }
    }
    
    public static void Resolve_High_GC_Allocation_Method_Using_List()
    {
        for (int i = 0; i < 500000; i++)
        {
            var data = new List<long>();
            for (int j = 0; j < 100; j++)
            {
                data.Add(j);
            }
            data.TrimExcess();
        }
    }
    
    public static void Resolve_High_GC_Allocation_Method_Using_ArrayPool()
    {
        var pool = ArrayPool<long>.Shared;
        for (int i = 0; i < 500000; i++)
        {
            var data = pool.Rent(100);
            try
            {
                for (int j = 0; j < 100; j++)
                {
                    data[j] = j;
                }
            }
            finally
            {
                pool.Return(data);
            }
        }
    }
}