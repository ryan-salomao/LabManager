using Microsoft.Data.Sqlite;
using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;

var databaseConfig = new DatabaseConfig();
new DatabaseSetup(databaseConfig);

var computerRepository = new ComputerRepository(databaseConfig);

//routing
var modelName = args[0];
var modelAction = args[1];

if(modelName == "Computer")
{
    if (modelAction == "List")
    {
        Console.WriteLine("Computer List");
        foreach (var computer in computerRepository.GetAll())
        {
            Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
        }

    }

    if (modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processor = args[4];

        var computer = new Computer(id, ram, processor);
        computerRepository.Save(computer);
    }
    
    if(modelAction == "Delete")
    {
        var id = Convert.ToInt32(args[2]);

        computerRepository.Delete(id);
    }

    if(modelAction == "Update")
    {
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processor = args[4];

        var computer = new Computer(id, ram, processor);
        computerRepository.Update(computer);
    }

    if(modelAction == "Display")
    {
        var id = Convert.ToInt32(args[2]);

        var computer = computerRepository.Display(id);
        Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
    }
}

if(modelName == "Laboratory")
{
    if (modelAction == "List")
    {
        var connection = new SqliteConnection("Data Source = database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Laboratories";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetChar(3));
        }

        reader.Close();
        connection.Close();
    }

    if (modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var number = Convert.ToInt32(args[3]);
        var name = args[4];
        var block = args[5];

        var connection = new SqliteConnection("Data Source = database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Laboratories VALUES($id, $number, $name, $block);";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$number", number);
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$block", block);

        command.ExecuteNonQuery();

        connection.Close();

        // sem testes
    }
    
}