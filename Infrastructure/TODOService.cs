namespace Infrastructure;
using Dapper;
using Domain.Models;
using Domain.Wrapper;
using Npgsql;


public class TODOService
{
     private string _connectionString;

    public TODOService()
    {
        _connectionString = "Server=127.0.0.1;Port=5432;Database=TODOdb;User Id=postgres;Password=masik00787737";
    }

     public Responce<TODO> AddTODO(TODO todo)
    {
        try
        {
             using (NpgsqlConnection connection  = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            string sql = $"insert into TODO (Title, Status) VALUES (@Title, @Status) returning Id";
            var response  = connection.ExecuteScalar<int>(sql, new{todo.Title, todo.Status});
            todo.Id = response;
            return new Responce<TODO>(todo);
        }
        }
        catch (Exception e)
        {
            
          return new Responce<TODO>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }   
    }

     public Responce<TODO> UpdateTODO(TODO todo)
    {
        try
        {
             using(NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            string sql = $"update TODO set Title = @Title, Status = @Status where Id = @Id returning Id";
            var response  = connection.ExecuteScalar<int>(sql, new{todo.Title, todo.Status, todo.Id});
            todo.Id = response;
            return new Responce<TODO>(todo);
        }
        }
         catch (Exception e)
        {     
           return new Responce<TODO>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }  
       
    }

    public Responce<string> DeleteTODO(int id)
    {
        try
        {
             using(NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            string sql = $"delete from TODO where Id = {id}";
            var response  = connection.ExecuteScalar<int>(sql);
            id = response;
            return new Responce<string>("Success");
        }
        }
         catch (Exception e)
        {
           return new Responce<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public Responce<List<TODO>> GetAllTODOs()
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            var todos = connection.Query<TODO>("Select * From TODO").ToList();
            return new Responce<List<TODO>>(todos);
        }
    }
}
