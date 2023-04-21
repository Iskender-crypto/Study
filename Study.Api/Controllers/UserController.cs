using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Npgsql;
using Study.Domain.Entities;

namespace Study.Domain.Entities;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
  private readonly string _connectionString =
    "Host=localhost;Database=Study;Username=iskender-crypto;Password=Iskndr-2003";
  [HttpGet]
  public IEnumerable<User> Get()
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open(); 
      var command = new NpgsqlCommand($"SELECT * FROM users", connection);
      var reader = command.ExecuteReader();  
      var users = new List<User>();
      while (reader.Read())
      {
        var user = new User
      {
        Id = Convert.ToInt32(reader[0]),
        name = (string)reader[1],
        username = (string)reader[2],
        email = (string)reader[3],
        address = (string)reader[4],
        phone = (string)reader[5],
        website = (string)reader[6],
        company = (string)reader[7]
      };
     users.Add(user);
      }
      connection.Close();
    
      return users;
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }
  }

  [HttpPost]
  public User Add([FromBody] User user)
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open();
      var command = new NpgsqlCommand($@"
      INSERT INTO users(
            name,
            username,
            email,
            address,
            phone,
            website,
            company
     ) values (
               '{user.name}',
               '{user.username}',
               '{user.email}',
               '{user.address}',
               '{user.phone}',
               '{user.website}',
               '{user.company}'
     );

     select currval('users_id_seq')
",connection);

     var id =  (long)command.ExecuteScalar();
     var returnCommandTxt = $"SELECT * from users where id = {id}";
     var returnCommand = new NpgsqlCommand(returnCommandTxt,connection);
     returnCommand.ExecuteReader();
      connection.Close();
      
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }

    return user;
  }
  
  [HttpDelete ("{Id}")]
  public void Delete(long Id)
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open();
      var command = new NpgsqlCommand($@"
      DELETE FROM users where id={Id};
",connection);

      command.ExecuteNonQuery();
        connection.Close();
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }

  }

  [HttpPut ("{Id}")]
  public User Update(long Id,[FromBody] User user)
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open();
      var command = new NpgsqlCommand($@"
      UPDATE users SET
             name = '{user.name}',
             username = '{user.username}',
             email = '{user.email}',
             address = '{user.address}',
             phone = '{user.phone}',
             website = '{user.website}',
             company = '{user.company}'
             where id={Id}

     select currval('users_id_seq')
",connection);

      var id =  (long)command.ExecuteScalar();
      var returnCommandTxt = $"SELECT * from users where id = {id}";
      var returnCommand = new NpgsqlCommand(returnCommandTxt,connection);
      returnCommand.ExecuteReader();
         connection.Close();
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }

    return user;
  }
  
}

