namespace DesignPattern;

internal class FactoryDesignPattern
{
    private void Test()
    {
        IAnimal dog = AnimalFactory.CreateAnimal("dog");
        dog.Speak(); // Output: Woof!

        IAnimal cat = AnimalFactory.CreateAnimal("cat");
        cat.Speak(); // Output: Meow!
    }
}

public interface IAnimal
{
    void Speak();
}

public class Dog : IAnimal
{
    public void Speak()
    {
        Console.WriteLine("Woof!");
    }
}

public class Cat : IAnimal
{
    public void Speak()
    {
        Console.WriteLine("Meow!");
    }
}

public static class AnimalFactory
{
    public static IAnimal CreateAnimal(string animalType)
    {
        switch (animalType.ToLower())
        {
            case "dog":
                return new Dog();
            case "cat":
                return new Cat();
            default:
                throw new ArgumentException("Invalid animal type");
        }
    }
}