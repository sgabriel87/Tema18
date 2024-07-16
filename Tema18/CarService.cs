using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tema18.CarData;
using Tema18.Models;

namespace Tema18
{
    public class CarService
    {
        private readonly CarDataContext _context;

        public CarService(CarDataContext context)
        {
            _context = context;
        }
        public void AddCarWithDetails(string carName, string manufacturerName, string manufacturerAddress,
                                      Guid keyAccessCode, int cylinderCapacity, int yearOfManufacture,
                                      string chassisNumber)
        {
            var existingManufacturer = _context.Manufacturers
                .FirstOrDefault(m => m.Name == manufacturerName && m.Address == manufacturerAddress);

            if (existingManufacturer != null)
            {
                var existingCar = _context.Cars
                    .FirstOrDefault(c => c.Name == carName && c.ManufacturerId == existingManufacturer.Id);

                if (existingCar != null)
                {
                    Console.WriteLine("A car with the same name and manufacturer already exists.");
                    return;
                }
            }
            var manufacturer = existingManufacturer ?? new Manufacturer { Name = manufacturerName, Address = manufacturerAddress };
            if (existingManufacturer == null)
            {
                _context.Manufacturers.Add(manufacturer);
                _context.SaveChanges();
                Console.WriteLine($"Manufacturer '{manufacturer.Name}' has been added.");
            }
            var car = new Car { Name = carName, ManufacturerId = manufacturer.Id };
            _context.Cars.Add(car);
            _context.SaveChanges();
            Console.WriteLine($"Car '{car.Name}' has been added.");

            var key = new Key { AccessCode = keyAccessCode, CarId = car.Id };
            car.Keys.Add(key);
            var technicalBook = new TechnicalBook { CylinderCapacity = cylinderCapacity, YearOfManufacture = yearOfManufacture, ChassisNumber = chassisNumber, CarId = car.Id };
            car.TechnicalBook = technicalBook;

            _context.SaveChanges();
            Console.WriteLine($"Key and technical book have been added to the car '{car.Name}'.");
        }
        public void AddManufacturer(Manufacturer manufacturer)
        {
            var existingManufacturer = _context.Manufacturers
                .FirstOrDefault(m => m.Name == manufacturer.Name && m.Address == manufacturer.Address);

            if (existingManufacturer != null)
            {
                Console.WriteLine("Manufacturer already exists.");
            }
            else
            {
                _context.Manufacturers.Add(manufacturer);
                _context.SaveChanges();
                Console.WriteLine($"Manufacturer '{manufacturer.Name}' has been added.");
            }
        }
        public void AddCar(Car car)
        {
            var existingCar = _context.Cars
                .FirstOrDefault(c => c.Name == car.Name && c.ManufacturerId == car.ManufacturerId);

            if (existingCar != null)
            {
                Console.WriteLine("Car already exists.");
            }
            else
            {
                _context.Cars.Add(car);
                _context.SaveChanges();
                Console.WriteLine($"Car '{car.Name}' has been added.");
            }
        }
        public void AddKeyToCar(int carId, Key key)
        {
            var car = _context.Cars.Find(carId);
            if (car != null)
            {
                var existingKey = car.Keys.FirstOrDefault(k => k.AccessCode == key.AccessCode);
                if (existingKey != null)
                {
                    Console.WriteLine($"Key already exists for the car '{car.Name}'.");
                }
                else
                {
                    car.Keys.Add(key);
                    _context.SaveChanges();
                    Console.WriteLine($"Key has been added to the car '{car.Name}'.");
                }
            }
            else
            {
                Console.WriteLine("Car not found.");
            }
        }
        public void ReplaceTechnicalBook(int carId, TechnicalBook technicalBook)
        {
            var car = _context.Cars.Find(carId);
            if (car != null)
            {
                if (car.TechnicalBook != null && car.TechnicalBook.ChassisNumber == technicalBook.ChassisNumber)
                {
                    Console.WriteLine($"Technical book already exists for the car '{car.Name}'.");
                }
                else
                {
                    car.TechnicalBook = technicalBook;
                    _context.SaveChanges();
                    Console.WriteLine($"Technical book has been replaced for the car '{car.Name}'.");
                }
            }
            else
            {
                Console.WriteLine("Car not found.");
            }
        }
        public void DeleteCar(int carId)
        {
            var car = _context.Cars.Find(carId);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
                Console.WriteLine($"Car with ID {carId} has been deleted.");
            }
            else
            {
                Console.WriteLine("Car not found.");
            }
        }
        public void DeleteManufacturer(int manufacturerId)
        {
            var manufacturer = _context.Manufacturers.Find(manufacturerId);
            if (manufacturer != null)
            {
                _context.Manufacturers.Remove(manufacturer);
                _context.SaveChanges();
                Console.WriteLine($"Manufacturer with ID {manufacturerId} has been deleted.");
            }
            else
            {
                Console.WriteLine("Manufacturer not found.");
            }
        }
        public void DeleteKey(int keyId)
        {
            var key = _context.Keys.Find(keyId);
            if (key != null)
            {
                _context.Keys.Remove(key);
                _context.SaveChanges();
                Console.WriteLine($"Key with ID {keyId} has been deleted.");
            }
            else
            {
                Console.WriteLine("Key not found.");
            }
        }
        public void DisplayAllCars()
        {
            var cars = _context.Cars.Include(c => c.Manufacturer).ToList();

            foreach (var car in cars)
            {
                Console.WriteLine($"ID: {car.Id}, Name: {car.Name}, Manufacturer: {car.Manufacturer.Name}");
            }
        }
    }
}
