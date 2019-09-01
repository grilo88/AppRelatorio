using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.ComponentModel;
using AppRelatorio.Atributos;
using System.Collections.ObjectModel;
using System.Data;

namespace AppRelatorio.Banco
{
    public class CRUD<T> where T : class
    {
        /// <summary>
        /// Converte o valor para o formato SQL.
        /// </summary>
        /// <param name="prop">Propriedade da classe</param>
        /// <param name="obj">Valor do objeto</param>
        /// <returns></returns>
        private static object ValorDB(PropertyInfo prop, object obj)
        {
            object valor = prop.GetValue(obj);

            if (valor is DBNull || valor is null)
            {
                valor = "NULL";
            }
            else if (valor is string)
            {
                valor = $"'{valor}'";
            }
            else if (valor is DateTime)
            {
                valor = $"'{((DateTime)valor).ToString(new System.Globalization.CultureInfo("en-us"))}'";
            }
            else if (valor is Enum)
            {
                Type vtype = valor.GetType();
                Type underlyingType = Enum.GetUnderlyingType(vtype);
                valor = Convert.ChangeType(valor, underlyingType);
            }
            return valor;
        }

        /// <summary>
        /// É computado?
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        private static bool IsComputed(PropertyInfo prop)
        {
            if (Attribute.GetCustomAttribute(prop, typeof(AutoIncrementAttribute)) is AutoIncrementAttribute ai && ai.Computed)
                return true;
            else
                return false;
        }

        /// <summary>
        /// É autoincremento?
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        private static bool IsAI(PropertyInfo prop)
        {
            if (Attribute.GetCustomAttribute(prop, typeof(AutoIncrementAttribute)) is AutoIncrementAttribute)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Insere o registro na tabela.
        /// </summary>
        /// <param name="dados"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Exclui o registro da tabela com base no id.
        /// </summary>
        /// <param name="dados"></param>
        /// <returns></returns>
        public static int Excluir(T dados)
        {
            using (SqliteConnection con = new SqliteConnection(Database.ConnectionString))
            {
                return 0;
            }
        }

        /// <summary>
        /// Atualiza o registro da tabela com base no id.
        /// </summary>
        /// <param name="dados"></param>
        /// <returns></returns>
        public static int Atualizar(T dados)
        {
            using (SqliteConnection con = new SqliteConnection(Database.ConnectionString))
            {
                return 0;
            }
        }

        /// <summary>
        /// Checa se possui algum registro na tabela de forma eficiente.
        /// </summary>
        /// <returns></returns>
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

        public static ObservableCollection<T> ToList(string sql)
        {
            using (SqliteConnection con = new SqliteConnection(Database.ConnectionString))
            {
                con.Open();
                using (SqliteCommand com = new SqliteCommand(sql, con))
                using (SqliteDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    ObservableCollection<T> collection = new ObservableCollection<T>();

                    while (dr.Read())
                    {
                        object item = Activator.CreateInstance(typeof(T));
                        PropertyInfo[] props = item.GetType().GetProperties();
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            string name = dr.GetName(i);
                            object value = dr.GetValue(i);

                            PropertyInfo prop;
                            if ((prop = props.Where(x => x.Name == name).FirstOrDefault()) != null)
                            {
                                if (value is DBNull)
                                    value = null;
                                else if (prop.PropertyType == typeof(DateTime))
                                    value = Convert.ToDateTime(value, new System.Globalization.CultureInfo("en-us"));
                                // Enumerador
                                else if (prop.PropertyType.BaseType == typeof(Enum))
                                {
                                    value = Enum.Parse(prop.PropertyType, value.ToString());
                                }

                                prop.SetValue(item, value);
                            }
                        }
                        collection.Add((T)item);
                    }

                    return collection;
                }
            }
        }
    }
}
