using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TrackEase.Abstraction;


public class BaseService<T> where T : class
{
    protected static List<T> GetAll(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return [];
        }

        var json = File.ReadAllText(filePath);

        var result = JsonSerializer.Deserialize<List<T>>(json);
        
        return result ?? [];
    }

    protected static void SaveAll(List<T> entity,string filePath)
    {
       
        var json = JsonSerializer.Serialize(entity);

        File.WriteAllText(filePath, json);
    }

    protected static int GetIdValue(List<T> entity)
    {
        if (entity.Count == 0) return 1;

        return entity.Count + 1;
    }
}