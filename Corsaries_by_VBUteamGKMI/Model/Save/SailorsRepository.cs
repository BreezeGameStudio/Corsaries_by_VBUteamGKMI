using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using Dapper;


namespace Corsaries_by_VBUteamGKMI.Model.Save
{
    static class SailorsRepository
    {
        static string save_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Corsairs\\save.sqlite";

        public static void CreateTable()
        {
            using (SQLiteConnection conn = new SQLiteConnection($"Data Source={save_path};Version=3;"))
            {
                try
                {
                    conn.Query(@"CREATE TABLE Sailors (
    Id    INTEGER PRIMARY KEY
                  UNIQUE
                  NOT NULL,
    Value VARCHAR NOT NULL
);
                    ");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

    }
}
