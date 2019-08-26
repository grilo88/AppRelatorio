using Microsoft.Data.Sqlite;
using AppRelatorio.Atributos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AppRelatorio.Banco
{
    public static class Extensao
    {
        private static string TypeDB(PropertyInfo prop)
        {
            Type dataType = prop.PropertyType;
            Type underlingType = Nullable.GetUnderlyingType(dataType);
            bool pk = Attribute.GetCustomAttribute(prop, typeof(KeyAttribute)) is KeyAttribute;

            string nullable = "";
            if (underlingType != null) // Anulável?
            {
                dataType = underlingType;
                nullable = "NULL";
            }
            else
            {
                // Não é PK?
                if (!pk) nullable = "NOT NULL"; // Então não é permitido NOT NULL com Primary Key
            }

            string typeDB;
            if (dataType.BaseType == typeof(Enum))
            {
                var p = dataType.BaseType.GetProperty("DeclaredFields");

                // TODO: Obter o primeiro item da array que contém o tipo de dados do enum
                // e jogar no datatype lá do começo do método

                //dataType.DeclaredFields
                typeDB = "";
            }
            else if (dataType == typeof(string))
                typeDB = "Varchar";
            else if (dataType == typeof(Int16))
                typeDB = pk ? "Integer" : "Smallint";
            else if (dataType == typeof(Int32))
                typeDB = pk ? "Integer" : "Int";
            else if (dataType == typeof(Int64))
                typeDB = pk ? "Integer" : "Bigint";
            else if (dataType == typeof(DateTime))
                typeDB = "Datetime";
            else
            {
                throw new NotImplementedException(dataType.GetType().Name);
            }

            return $" {typeDB} {nullable}".TrimEnd();
        }

        private static string PK(PropertyInfo prop)
        {
            string attr = "";

            if (Attribute.GetCustomAttribute(prop, typeof(KeyAttribute)) is KeyAttribute)
            {
                attr = "PRIMARY KEY";

                if (Attribute.GetCustomAttribute(
                prop, typeof(AutoIncrementAttribute)) is AutoIncrementAttribute ai)
                {
                    //switch (ai.DatabaseGeneratedOption)
                    //{
                    //    case DatabaseGeneratedOption.Identity:
                            attr += " AUTOINCREMENT";
                    //        break;
                    //    default:
                    //        throw new NotImplementedException(ai.DatabaseGeneratedOption.ToString());
                    //}
                }
            }

            return $" {attr}";
        }

        public static int CriarTabela<T>(this SqliteConnection con)
        {
            Type table = typeof(T);
            string table_name = table.Name;
            PropertyInfo[] props = table.GetProperties();

            using (SqliteCommand com = con.CreateCommand())
            {
                com.CommandText = $"CREATE TABLE {table_name}";
                com.CommandText += $"({string.Join(",", props.Select(x => $"{x.Name}{TypeDB(x)}{PK(x)}"))}";

                #region Importante para Chaves Primárias Compostas
                //var pk_cols = props.Where(x => x.CustomAttributes.Cast<CustomAttributeData>().Any(z => z.AttributeType == typeof(KeyAttribute))).ToArray();
                //if (pk_cols.Length > 0)
                //{
                //    com.CommandText += $",CONSTRAINT primary_key PRIMARY KEY ({string.Join(",", pk_cols.Select(x => x.Name))})";
                //}
                #endregion

                com.CommandText += ")";
                return com.ExecuteNonQuery();
            }

            //CREATE UNIQUE INDEX idx_contacts_email ON contacts (email);
        }

        public static bool TabelaExiste<T>(this SqliteConnection con)
        {
            string table_name = typeof(T).Name;
            using (SqliteCommand com = con.CreateCommand())
            {
                com.CommandText = $"SELECT 1 FROM sqlite_master WHERE type='table' AND name='{table_name}';";
                return com.ExecuteScalar() != null;
            }
        }

        public static void ExcluirTabela<T>(this SqliteConnection con)
        {
            string table_name = typeof(T).Name;
            using (SqliteCommand com = con.CreateCommand())
            {
                com.CommandText = $"DROP TABLE '{table_name}';";
                com.ExecuteScalar();
            }
        }

        public static void TruncarTabela<T>(this SqliteConnection con)
        {
            string table_name = typeof(T).Name;
            using (SqliteCommand com = con.CreateCommand())
            {
                com.CommandText = $"DELETE FROM '{table_name}';";
                com.ExecuteScalar();
            }
        }
    }
}
