using System.Data;
using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class sqlTestScript : MonoBehaviour
{
    void Start() 
    {   
        //This module was only for testing
        //TODO make this static so everyone can Call it from everywhere bcs its stateless
        //TODO llamar a esto en cada funcion en 2 metodos reutilizables de open and close
        //string connection = "URI=file:" + Application.dataPath + "/GameDatabase.db";
        //IDbConnection dbcon = new SqliteConnection(connection);
        //dbcon.Open();
        //dbcon.Close();
    }

    private static void InsertValueToTable(IDbConnection dbcon) //crear record para una tabla
    {
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "INSERT INTO TEST(id, value) VALUES (3, 'Pablo')";
        cmnd.ExecuteNonQuery();

        //TODO for Player and Record
        //IDbCommand dbcmd = dbconn.CreateCommand();

        //string sqlInsert = "INSERT INTO lift_TB (Date,Time,Floor) VALUES (@currentdate,@currenttime,@currentfloor);";
        //dbcmd.Parameters.Add(new SqliteParameter("@currentdate", currentdate));
        //dbcmd.Parameters.Add(new SqliteParameter("@currenttime", currenttime));
        //dbcmd.Parameters.Add(new SqliteParameter("@currentfloor", currentfloor));

        //dbcmd.CommandText = sqlInsert;
        //dbcmd.ExecuteNonQuery();

    }

    private static void CreateTables(IDbConnection dbcon) //Aqui creas la tabla player y score
    {
        IDbCommand dbcmd;
        dbcmd = dbcon.CreateCommand();
        string q_createTable = "CREATE TABLE IF NOT EXISTS my_table (id INTEGER PRIMARY KEY, val INTEGER )";

        dbcmd.CommandText = q_createTable;
        dbcmd.ExecuteReader();
    }

    private static void ReadAllTable(IDbConnection dbcon)
    {   
        //Aqui se podra leer los jugadores para seleccionar 1
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM TEST";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        //while (reader.Read())
        //{
        //    Debug.Log("id: " + reader[0].ToString());
        //    Debug.Log("val: " + reader[1].ToString());
        //}
    }
}
