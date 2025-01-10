using System.IO;
using Microsoft.Maui.Storage;

namespace TrackEase.Util;

public class Util
{

    public static string GetAppUsersFilePath()
    {
        return Path.Combine(FileSystem.AppDataDirectory, "users.json");
    }

    public static string GetAppTransactionsFilePath()
    {
        return Path.Combine(FileSystem.AppDataDirectory, "transactions.json");
    }
    
    public static string GetAppTagsFilePath()
    {
        return Path.Combine(FileSystem.AppDataDirectory, "tags.json");
    }
    
    public static string GetAppDebtsFilePath()
    {
        return Path.Combine(FileSystem.AppDataDirectory, "debts.json");
    }
}