using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace AnimalShelter.Models
{
  public class Animal
  {
    private string _name;
    private string _type;
    private string _breed;
    private string _sex;
    private DateTime _intake;
    private int _id;

    public Animal(string name, string type, string breed, string sex, int animalId = 0)
    {
      _name = name;
      _type = type;
      _breed = breed;
      _sex = sex;
      // _intake = intake;

      _id = animalId;
    }

    public string Name {get => _name;}
    public string Type {get => _type;}
    public string Breed {get => _breed;}
    public string Sex {get => _sex;}
    public DateTime Intake {get => _intake; set=> _intake = value;}
    public int Id {get => _id;}


    public static List<Animal> GetAll()
    {
      List<Animal> allAnimals = new List<Animal> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM animals;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int animalId = rdr.GetInt32(0);
        string type = rdr.GetString(1);
        string breed = rdr.GetString(2);
        string sex = rdr.GetString(3);
        string name = rdr.GetString(4);
        DateTime intake = rdr.GetDateTime(5) ;

        Animal newAnimal = new Animal(name, type, breed, sex, animalId);
        newAnimal.Intake = intake;
        allAnimals.Add(newAnimal);
      }

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allAnimals;
    }

      public void Save()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO animals (name, type, breed, sex) VALUES (@name, @type, @breed, @sex);";
        MySqlParameter name = new MySqlParameter();
        name.ParameterName = "@name";
        name.Value = this._name;

        MySqlParameter type = new MySqlParameter();
        type.ParameterName = "@type";
        type.Value = this._type;

        MySqlParameter breed = new MySqlParameter();
        breed.ParameterName = "@breed";
        breed.Value = this._breed;

        // MySqlParameter intake = new MySqlParameter();
        // intake.ParameterName = "@intake";
        // intake.Value = this._intake;

        MySqlParameter sex = new MySqlParameter();
        sex.ParameterName = "@sex";
        sex.Value = this._sex;
        Console.WriteLine(_name);


        cmd.Parameters.Add(name);
        cmd.Parameters.Add(type);
        cmd.Parameters.Add(breed);
        cmd.Parameters.Add(sex);
        // cmd.Parameters.Add(intake);
        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;
        // more logic will go here in a moment

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

    // public static Animal FindAnimalByType(string searchType)
    // {
    //   return foundAnimals;
    // }
    //
    // public static Animal FindAnimalByBreed(string searchBreed)
    // {
    //   return foundAnimals;
    // }
    //
    //
    // public static Animal FindAnimalById(int searchId)
    // {
    //   return foundAnimal;
    // }
  }
}
