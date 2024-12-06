Car car;
Console.Write("Выберите тип машины:\n1)Electric car;\n2)Auto;\nВведите цифру >> ");
string playerKey = Console.ReadLine();
if (playerKey == "1") car = new Car("батареи");
else car = new Car("бака");
Console.Write("Машина готова!\n");
car.Travel();
class Car 
{
    private string make, model, year;
    private int odometer = 0;
    private Engine engine;
    public Car(string engineType)
    {
        Console.Write("Введите марку машины >> ");
        this.make = Console.ReadLine();
        Console.Write("Введите модель машины >> ");
        this.model = Console.ReadLine();
        Console.Write("Введите год выпуска машины >> ");
        this.year = Console.ReadLine();
        engine = new Engine(engineType);
    }
    private void IncrementOdometer(int d) => this.odometer += d;
    public void Travel() 
    {
        while (true)
        {
            Console.Write("Введите дистанцию(или q чтобы выйти) >> ");
            string d = Console.ReadLine();
            if (d == "q") break;
            else 
            {
                IncrementOdometer(Convert.ToInt32(d));
                this.engine.GetTravel(Convert.ToInt32(d));
            }
        }
        Console.WriteLine($"На одометре машины {this.make} {this.model} {this.year} насчитано {this.odometer} км.");
    }
    public class Engine
    {
        private int size, consumption, value;
        private string type;
        public Engine(string type)
        {
            this.type = type;
            Console.Write($"Введите максимальную ёмкость {this.type} >> ");
            this.size = Convert.ToInt32(Console.ReadLine());
            Console.Write($"Введите расход {this.type} на 100 км >> ");
            this.consumption = Convert.ToInt32(Console.ReadLine());
            do
            {
                Console.Write($"Введите объём {this.type} в настоящий момент >> ");
                this.value = Convert.ToInt32(Console.ReadLine());
            }
            while (value > size);
        }
        private void ChangeValue(int c) => this.value += c;
        private void Refueling()
        {
            Console.WriteLine("Введите дозаправку >> ");
            int t = Convert.ToInt32(Console.ReadLine());
            while (t + value > size)
            {
                Console.WriteLine("Ваша дозаправка превышает максимальную вместимось!\nВведите новую дозаправку >> ");
                t = Convert.ToInt32(Console.ReadLine());
            }
            ChangeValue(+t);
        }
        private int Waste(int distance)
        {
            if (distance >= 100)
            {
                distance -= 100;
                ChangeValue(-consumption);
                Console.WriteLine($"{distance} {value}");
            }
            else if (distance >= 10)
            {
                distance -= 10;
                ChangeValue(-consumption / 10);
                Console.WriteLine($"{distance} {value}");
            }
            else if (distance >= 1)
            {
                distance -= 1;
                ChangeValue(-consumption / 100);
                Console.WriteLine($"{distance} {value}");
            }
            return distance;
        }
        public void GetTravel(int distance)
        {
            int count = 0;
            int d = distance;
            while (distance != 0)
            {
                if (value < consumption)
                {
                    this.Refueling();
                    distance = this.Waste(distance);
                    count++;
                }
                else distance = this.Waste(distance);
            }
            if (count == 0) Console.WriteLine($"Автомобиль проехал {d} км без дозаправок.");
            else if (count == 1) Console.WriteLine($"Автомобиль проехал {d} км с одной дозаправкой.");
            else Console.WriteLine($"Автомобиль проехал {d} км с {count} дозаправками.");
        }
    }
}
/*
class ElectricCar : Car 
{
    public ElectricCar(string engineType) : base(engineType) { }
}
class Auto : Car 
{
    public Auto(string engineType) : base(engineType) { }
}
*/