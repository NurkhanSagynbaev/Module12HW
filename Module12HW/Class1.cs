using System;

uusing System;

// Базовый класс для всех типов автомобилей
public abstract class Car
{
    private string name;
    private int speed;
    private int distance;

    // Событие, срабатывающее при достижении финиша
    public event EventHandler<string> Finish;

    public string Name { get { return name; } }
    public int Speed { get { return speed; } }
    public int Distance { get { return distance; } }

    public Car(string name, int speed)
    {
        this.name = name;
        this.speed = speed;
        this.distance = 0;
    }

    // Метод для перемещения автомобиля
    public virtual void Move()
    {
        distance += speed;

        // Проверка на достижение финиша
        if (distance >= 100)
        {
            OnFinish();
        }
    }

    // Метод для вызова события Finish
    protected virtual void OnFinish()
    {
        Finish?.Invoke(this, $"{name} пришел к финишу!");
    }
}

// Класс для спортивных автомобилей
public class SportsCar : Car
{
    public SportsCar(string name) : base(name, new Random().Next(10, 20))
    {
    }
}

// Класс для легковых автомобилей
public class SedanCar : Car
{
    public SedanCar(string name) : base(name, new Random().Next(5, 15))
    {
    }
}

// Класс для грузовых автомобилей
public class Truck : Car
{
    public Truck(string name) : base(name, new Random().Next(3, 10))
    {
    }
}

// Класс для автобусов
public class Bus : Car
{
    public Bus(string name) : base(name, new Random().Next(2, 8))
    {
    }
}

// Класс игры
public class RaceGame
{
    // Делегат для методов из классов автомобилей
    public delegate void CarAction();

    // Событие, срабатывающее при старте гонок
    public event EventHandler<string> RaceStarted;

    // Метод для запуска гонок
    public void StartRace()
    {
        OnRaceStarted("Гонки начались!");

        SportsCar sportsCar = new SportsCar("Спортивный автомобиль");
        SedanCar sedanCar = new SedanCar("Легковой автомобиль");
        Truck truck = new Truck("Грузовик");
        Bus bus = new Bus("Автобус");

        CarAction startDelegate = sportsCar.Move;
        startDelegate += sedanCar.Move;
        startDelegate += truck.Move;
        startDelegate += bus.Move;

        while (sportsCar.Distance < 100 && sedanCar.Distance < 100 && truck.Distance < 100 && bus.Distance < 100)
        {
            startDelegate.Invoke();
        }
    }

    // Метод для вызова события RaceStarted
    protected virtual void OnRaceStarted(string message)
    {
        RaceStarted?.Invoke(this, message);
    }
}

// Класс для запуска игры
class Program
{
    static void Main()
    {
        RaceGame raceGame = new RaceGame();
        raceGame.RaceStarted += (sender, message) => Console.WriteLine(message);
        raceGame.StartRace();
    }
}
