using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using ToDo.Models;
using ToDo.Models.ViewModels;

namespace ToDo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var toDoListViewModel = GetAllToDos();
        return View(toDoListViewModel);
    }

    [HttpGet]
    public JsonResult PopulateForm(int id)
    {
        var toDo = GetById(id);
        return Json(toDo);
    }
    internal ToDoViewModel GetAllToDos()
    {
        List<ToDoItem> toDoList = new();

        using (SqliteConnection con =
            new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = "SELECT * FROM todo";

                using (var reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            toDoList.Add(new ToDoItem
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                    else
                    {
                        return new ToDoViewModel
                        {
                            ToDoList = toDoList
                        };
                    }
                }
            }
        }
        return new ToDoViewModel
        {
            ToDoList = toDoList
        };
    }

    internal ToDoItem GetById(int id)
    {
        ToDoItem toDo = new();

        using (var connection =
               new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = connection.CreateCommand())
            {
                connection.Open();
                tableCmd.CommandText = $"SELECT * FROM todo Where Id = '{id}'";

                using (var reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        toDo.Id = reader.GetInt32(0);
                        toDo.Name = reader.GetString(1);
                    }
                    else
                    {
                        return toDo;
                    }
                };
            }
        }

        return toDo;
    }



    public RedirectResult Insert(ToDoItem toDo)
    {
        using (SqliteConnection con =
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = $"INSERT INTO todo (name) VALUES ('{toDo.Name}')";
                try
                {
                    tableCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        return Redirect("https://localhost:5000/");
    }
    [HttpPost]
    public JsonResult Delete(int id)
    {
        using (SqliteConnection con =
               new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = $"DELETE from todo WHERE Id = '{id}'";
                tableCmd.ExecuteNonQuery();
            }
        }

        return Json(new { });
    }

    public RedirectResult Update(ToDoItem toDo)
    {
        using (SqliteConnection con =
               new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = $"UPDATE todo SET name = '{toDo.Name}' WHERE Id = '{toDo.Id}'";
                try
                {
                    tableCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        return Redirect("https://localhost:5000/");
    }



}