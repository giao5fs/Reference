using System.Runtime.Serialization.Formatters.Binary;

namespace DesignPattern;

// Step 3: Use
public static class PrototypeDesignPattern
{
    public static void Test()
    {
        // Original object
        Person originalPerson = new Person
        {
            Name = "John Doe",
            Age = 30,
            Address = new Address { Street = "123 Main St", City = "New York" }
        };

        // Clone the original object
        Person clonedPerson = originalPerson.Clone();

        // Modify the clone
        clonedPerson.Name = "Jane Doe";
        clonedPerson.Address.City = "Los Angeles";

        // Display the original and cloned objects
        Console.WriteLine("Original Person: ");
        Console.WriteLine(originalPerson);

        Console.WriteLine("Cloned Person: ");
        Console.WriteLine(clonedPerson);
    }
}

// Step 1: Define prototy interface
public interface IPrototype<T>
{
    T Clone();
}

// Step 2: Define concrete prototype
//[Serializable]
public class Person : IPrototype<Person>
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Address Address { get; set; }

    public Person Clone()
    {
        //var cloned = (Person)this.MemberwiseClone();
        //cloned.Address = new Address { Street = this.Address.Street, City = this.Address.City };

        //return cloned;

        return DeepClone2.DeepClone(this);

        //return (Person)this.MemberwiseClone();
    }

    public override string ToString()
    {
        return $"Name: {Name}, Age: {Age}, Address: {Address}";
    }
}

//[Serializable]
public class Address
{
    public string Street { get; set; }
    public string City { get; set; }

    public override string ToString()
    {
        return $"{Street}, {City}";
    }
}

public static class DeepClone1
{
    public static T DeepClone<T>(T obj)
    {
        using (var memoryStream = new MemoryStream())
        {
            var formatter = new BinaryFormatter(); // Consider using other formatters like JsonSerializer
            formatter.Serialize(memoryStream, obj);

            memoryStream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(memoryStream);
        }
    }
}

public static class DeepClone2
{
    public static T? DeepClone<T>(T obj)
    {
        if (obj is null) return default;

        // Handle primitive types directly
        if (obj is ValueType || obj is string)
        {
            return obj;
        }

        // Handle complex types (recursive call)
        var newObj = (T)Activator.CreateInstance(obj.GetType());
        var properties = obj.GetType().GetProperties();
        foreach (var property in properties)
        {
            property.SetValue(newObj, DeepClone(property.GetValue(obj)));
        }
        return newObj;
    }
}