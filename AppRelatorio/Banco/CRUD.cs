using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.ComponentModel;
using AppRelatorio.Atributos;

namespace AppRelatorio.Banco
{
    public class CRUD<T> where T : class
    {
        private static object ValorDB(PropertyInfo prop, object obj)
        {
            object valor = prop.GetValue(obj);

            if (valor is string)
            {
                valor = $"'{valor}'";
            }
            else if (valor is DateTime)
            {
                valor = $"'{((DateTime)valor).ToString(new System.Globalization.CultureInfo("en-us"))}'";
            }
            return valor;
        }

        private static bool IsComputed(PropertyInfo prop)
        {
            if (Attribute.GetCustomAttribute(prop, typeof(AutoIncrementAttribute)) is AutoIncrementAttribute ai && ai.Computed)
                return true;
            else
                return false;
        }

        private static bool IsAI(PropertyInfo prop)
        {
            if (Attribute.GetCustomAttribute(prop, typeof(AutoIncrementAttribute)) is AutoIncrementAttribute)
                return true;
            else
                return false;
        }

        public static int Inserir(T dados)
        {
            Type table = typeof(T);
            PropertyInfo[] props = table.GetProperties();

            var cells = props
                .Where(p => !IsAI(p) && !IsComputed(p)) // Ignora Colunas AutoIncremento e Computadas
                .Select(p => new { column = p.Name, value = ValorDB(p, dados) }).ToArray();

            using (SqliteConnection con = new SqliteConnection(Database.ConnectionString))
            {
                con.Open();
                using (SqliteCommand com = new SqliteCommand("", con))
                {
                    com.CommandText = $"INSERT INTO {table.Name}({string.Join(",", cells.Select(x => x.column))})VALUES";
                    com.CommandText += $"({string.Join(",", cells.Select(x => x.value))})";
                    return Convert.ToInt32(com.ExecuteScalar());
                }
            }
        }

        public static int Excluir(T dados)
        {
            using (SqliteConnection con = new SqliteConnection(Database.ConnectionString))
            {
                return 0;
            }
        }

        public static int Atualizar(T dados)
        {
            using (SqliteConnection con = new SqliteConnection(Database.ConnectionString))
            {
                return 0;
            }
        }

        public static bool AlgumRegistro()
        {
            using (SqliteConnection con = new SqliteConnection(Database.ConnectionString))
            {
                con.Open();
                using (SqliteCommand com = new SqliteCommand("", con))
                {
                    com.CommandText = $"SELECT CASE WHEN(SELECT 1 FROM {typeof(T).Name} LIMIT 1) = 1 THEN 1 ELSE 0 END;";
                    // Verifica se o primeiro registro existe evitando o uso do count 
                    // pois se existir muitos registros o count ficará lento
                    byte result = Convert.ToByte(com.ExecuteScalar());
                    return result == 1;
                }
            }
        }
    }
}
