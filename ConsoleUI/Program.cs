using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;

//CarAddTest();
//CarDetailTest();


static void CarAddTest()
{
    CarManager carManager = new CarManager(new EfCarDal());

    var carsResult = carManager.GetAll(); 

    if (carsResult.Success) 
    {
        foreach (var car in carsResult.Data)
        {
            Console.WriteLine(car.ModelName);
        }
    }
    else
    {
        Console.WriteLine(carsResult.Message); 
    }

    carManager.Add(new Car { CarId = 3, BrandId = 4, ColorId = 3, DailyPrice = 1200, ModelName = "520", ModelYear = 2023 });

    var updatedCarsResult = carManager.GetAll(); 

    if (updatedCarsResult.Success) 
    {
        foreach (var car in updatedCarsResult.Data) 
        {
            Console.WriteLine(car.ModelName);
        }
    }
    else
    {
        Console.WriteLine(updatedCarsResult.Message); 
    }
}

static void CarDetailTest()
{
    CarManager carManager = new CarManager(new EfCarDal());
    
    var carDetailsResult = carManager.GetCarDetails();

    if (carDetailsResult.Success)
    {
        foreach (var car in carDetailsResult.Data)
        {
            Console.WriteLine(car.ModelName + "/" + car.BrandName + "/" + car.ColorName);
            Console.WriteLine(carDetailsResult.Message);
        }
    }
    else
    {
        Console.WriteLine("An error occurred: " + carDetailsResult.Message);
    }
}