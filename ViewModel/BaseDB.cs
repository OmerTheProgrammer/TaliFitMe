using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ViewModel
{
    public abstract class BaseDB
    {

        //$"Provider=Microsoft.ACE.OLEDB.12.0;Data Source= " +
        //    $"\"C:\\Users\\mirit\\source\\repos\\titistunis2-stack\\TaliFitMe\\ViewModel\\talistu2506.accdb\"";
        protected static string connectionString = GetConnectionString();


        protected static OleDbConnection connection;
        protected static OleDbTransaction trans;
        protected OleDbCommand command;
        protected OleDbDataReader reader;
        public static string GetPath()
        {
            // 1. Get the directory of the executing assembly (e.g., bin/Debug/net8.0/)
            string assemblyPath = Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location);

            // 2. Navigate UP to the project root (assuming standard structure)
            // This might need adjustment based on your specific solution structure (e.g., 3 levels up: \bin\Debug\net8.0\)
            // Let's assume the DB file is in a known location relative to the solution file.
            // A more robust approach: Find the project path.

            // This navigates up three levels from the running DLL:
            // ViewModel.dll <- bin <- Debug <- net8.0 (1) <- ProjectName (2) <- SolutionFolder (3)
            string projectRoot = Path.GetFullPath(
                Path.Combine(assemblyPath, @"..\..\..\.."));

            // Assuming your .mdf file is located in the root of your ViewModel project folder:
            string ProjectPath = Path.Combine(projectRoot, "ViewModel");
            return ProjectPath;
        }

        private static string GetConnectionString()
        {
            // Assuming your .mdf file is located in the root of your ViewModel project folder:
            string dbFilePath = Path.Combine(GetPath(), "talistu2506.accdb");

            // 3. Construct the connection string using the correct, fully qualified path
            return "Provider=Microsoft.ACE.OLEDB.12.0;" +
                   "Data Source=\"" + dbFilePath + "\";";
        }

        public BaseDB()
        {
            connection ??= new OleDbConnection(connectionString);
            command = new OleDbCommand();
            command.Connection = connection;
        }
        public abstract BaseEntity NewEntity();
        protected List<BaseEntity> Select()
        {
            List<BaseEntity> list = new List<BaseEntity>();
            try
            {
                command.Connection = connection;
                if (trans != null)
                {
                    command.Transaction = trans;
                }
                else
                {
                    command.Transaction = null; // Clear old transaction references
                }

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BaseEntity entity = NewEntity();
                    list.Add(CreateModel(entity));
                }
            }
            catch (Exception e)
            {

                throw new Exception(
                    e.Message + "\nSQL:" + command.CommandText);
            }
            finally
            {
                if (reader != null) reader.Close();

            }
            return list;
        }
        protected async Task<List<BaseEntity>> SelectAsync(string sqlStr)
        {
            OleDbConnection connection = new OleDbConnection();
            OleDbCommand command = new OleDbCommand();
            List<BaseEntity> list = new List<BaseEntity>();

            try
            {
                command.Connection = connection;
                command.CommandText = sqlStr;
                connection.Open();
                this.reader = (OleDbDataReader)await command.ExecuteReaderAsync();


                while (reader.Read())
                {
                    BaseEntity entity = NewEntity();
                    list.Add(CreateModel(entity));
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL:" + command.CommandText);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (connection.State == ConnectionState.Open) connection.Close();
            }
            return list;
        }
        protected virtual BaseEntity CreateModel(BaseEntity entity)
        {
            entity.Id = (int)reader["id"];
            return entity;
        }
        protected abstract void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd);
        public static List<ChangeEntity> deleted = new List<ChangeEntity>();
        public virtual void Delete(BaseEntity entity)
        {
            BaseEntity reqEntity = this.NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }
        protected abstract void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd);
        public static List<ChangeEntity> inserted = new List<ChangeEntity>();
        public virtual void Insert(BaseEntity entity)
        {
            BaseEntity reqEntity = this.NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                inserted.Add(new ChangeEntity(this.CreateInsertdSQL, entity));
            }
        }
        protected abstract void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd);
        public static List<ChangeEntity> updated = new List<ChangeEntity>();
        public virtual void Update(BaseEntity entity)
        {
            BaseEntity reqEntity = this.NewEntity();
            if (entity != null && reqEntity.GetType().IsAssignableFrom(entity.GetType()))
            {
                updated.Add(new ChangeEntity(this.CreateUpdatedSQL, entity));
            }
        }
        public int SaveChanges()
        {
            trans = null;
            int records_affected = 0;

            try
            {
                command.Connection = connection;

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                trans = connection.BeginTransaction();
                command.Transaction = trans;
                foreach (var entity in inserted)
                {
                    command.Parameters.Clear();
                    entity.CreateSql(entity.Entity, command);
                    records_affected += command.ExecuteNonQuery();

                    command.CommandText = "Select @@Identity";
                    entity.Entity.Id = (int)command.ExecuteScalar();
                }
                foreach (var entity in updated)
                {
                    command.Parameters.Clear();
                    entity.CreateSql(entity.Entity, command);
                    records_affected += command.ExecuteNonQuery();
                }
                foreach (var entity in deleted)
                {
                    command.Parameters.Clear();
                    entity.CreateSql(entity.Entity, command); records_affected += command.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                trans = null;
                throw new Exception(ex.Message + "\n SQL:" + command.CommandText);
            }
            finally
            {
                inserted.Clear();

                updated.Clear();

                deleted.Clear();

            }
            return records_affected;
        }
    }
}

