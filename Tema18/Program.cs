using Tema18.CarData;
using Tema18.Models;
using Tema18;


using (var context = new CarDataContext())
{
    context.Database.EnsureCreated();
    var carService = new CarService(context);
    Seed(context, carService);

    //carService.DeleteCar(5);
    
    carService.AddCarWithDetails(
        carName: "Corolla",
        manufacturerName: "Toyota",
        manufacturerAddress: "Japan",
        keyAccessCode: Guid.NewGuid(),
        cylinderCapacity: 1800,
        yearOfManufacture: 2022,
        chassisNumber: "DEF456"
    );
    carService.AddCarWithDetails(
        carName: "1300",
        manufacturerName: "Dacia",
        manufacturerAddress: "Romania",
        keyAccessCode: Guid.NewGuid(),
        cylinderCapacity: 1200,
        yearOfManufacture: 1970,
        chassisNumber: "DC001"
    );
    carService.AddCarWithDetails(
        carName: "Duster",
        manufacturerName: "Dacia",
        manufacturerAddress: "Romania",
        keyAccessCode: Guid.NewGuid(),
        cylinderCapacity: 1490,
        yearOfManufacture: 2000,
        chassisNumber: "DU001"
    );
    carService.DisplayAllCars();
}

static void Seed(CarDataContext context, CarService carService)
{
    if (!context.Manufacturers.Any())
    {
        var manufacturer1 = new Manufacturer { Name = "Toyota", Address = "Japan" };
        var manufacturer2 = new Manufacturer { Name = "Ford", Address = "USA" };
        carService.AddManufacturer(manufacturer1);
        carService.AddManufacturer(manufacturer2);

        var car1 = new Car { Name = "Camry", ManufacturerId = manufacturer1.Id };
        var car2 = new Car { Name = "Mustang", ManufacturerId = manufacturer2.Id };
        carService.AddCar(car1);
        carService.AddCar(car2);
        
        var key1 = new Key { AccessCode = Guid.NewGuid(), CarId = car1.Id };
        var key2 = new Key { AccessCode = Guid.NewGuid(), CarId = car1.Id };
        var key3 = new Key { AccessCode = Guid.NewGuid(), CarId = car2.Id };
        carService.AddKeyToCar(car1.Id, key1);
        carService.AddKeyToCar(car1.Id, key2);
        carService.AddKeyToCar(car2.Id, key3);

        var technicalBook1 = new TechnicalBook { CylinderCapacity = 2000, YearOfManufacture = 2020, ChassisNumber = "ABC123", CarId = car1.Id };
        var technicalBook2 = new TechnicalBook { CylinderCapacity = 5000, YearOfManufacture = 2021, ChassisNumber = "XYZ789", CarId = car2.Id };
        carService.ReplaceTechnicalBook(car1.Id, technicalBook1);
        carService.ReplaceTechnicalBook(car2.Id, technicalBook2);
    }
}
