using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using Dapper;

namespace Corsaries_by_VBUteamGKMI.Model.Save
{
    static class PropsRepository
    {

        static string save_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Corsairs\\save.sqlite";

        public static void CreateTable()
        {
            using (SQLiteConnection conn = new SQLiteConnection($"Data Source={save_path};Version=3;"))
            {
                try
                {
                    conn.Query(@"CREATE TABLE Props (
    Id                    INTEGER PRIMARY KEY
                                  NOT NULL
                                  UNIQUE,
    gameTime              VARCHAR NOT NULL,
    captain                       NOT NULL,
    ship_type             VARCHAR NOT NULL,
    name                  VARCHAR NOT NULL,
    price                 INTEGER NOT NULL,
    current_count_sailors INTEGER NOT NULL,
    max_count_sailors     INTEGER NOT NULL,
    max_capacity          INTEGER NOT NULL,
    current_capacity      INTEGER NOT NULL,
    max_hp                INTEGER NOT NULL,
    current_hp            INTEGER NOT NULL,
    speed                 DECIMAL NOT NULL,
    cannon                VARCHAR NOT NULL,
    count_cannon          INTEGER NOT NULL,
    protection            INTEGER NOT NULL,
    dodge_chance          INTEGER NOT NULL,
    position_x            DECIMAL NOT NULL,
    position_y            DECIMAL NOT NULL
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
