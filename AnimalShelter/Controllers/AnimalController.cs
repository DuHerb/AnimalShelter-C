using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AnimalShelter.Models;
using System;

namespace AnimalShelter.Controllers
{
  public class AnimalController : Controller
  {
    [HttpGet("/animal")]
    public ActionResult Index()
    {
      List<Animal> allAnimals = Animal.GetAll();
      return View(allAnimals);
    }

    [HttpGet("/animal/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/animal")]
    public ActionResult Create(string name, string type, string breed, string sex)
    {
      Animal newAnimal = new Animal(name, type, breed, sex);
      // List<Animal> testList = new List<Animal> {};
      // testList.Add(newAnimal);
      newAnimal.Save();
      return RedirectToAction("Index");
    }
  }
}
