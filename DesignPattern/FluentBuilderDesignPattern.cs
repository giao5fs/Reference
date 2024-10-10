namespace DesignPattern;

// Step 3: Use the Fluent Builder
public static class FluentBuilderDesignPattern
{
    public static void Test()
    {
        HouseBuilder builder = new HouseBuilder();
        House house = builder
            .BuildFoundation("Concrete, brick, and stone")
            .BuildStructure("Wood and brick")
            .BuildRoof("Tile")
            .BuildInterior("Paint and wood")
            .Build();

        Console.WriteLine("House: ");
        Console.WriteLine(house);
    }
}

// Step 1: Define the product
//public class House
//{
//    public string Foundation { get; set; }
//    public string Structure { get; set; }
//    public string Roof { get; set; }
//    public string Interior { get; set; }
//    public override string ToString()
//    {
//        return $"Foundation: {Foundation}, Structure: {Structure}, Roof: {Roof}, Interior: {Interior}";
//    }
//}

// Step 2: Implement Fluent Builder

public interface IHouseFluentBuilder
{
    IHouseFluentBuilder BuildFoundation(string input);
    IHouseFluentBuilder BuildStructure(string input);
    IHouseFluentBuilder BuildRoof(string input);
    IHouseFluentBuilder BuildInterior(string input);
    House Build();
}
public class HouseBuilder : IHouseFluentBuilder
{
    private House _house = new House();

    public IHouseFluentBuilder BuildFoundation(string foundation)
    {
        _house.Foundation = foundation;
        return this;
    }

    public IHouseFluentBuilder BuildStructure(string structure)
    {
        _house.Structure = structure;
        return this;
    }

    public IHouseFluentBuilder BuildRoof(string roof)
    {
        _house.Roof = roof;
        return this;
    }

    public IHouseFluentBuilder BuildInterior(string interior)
    {
        _house.Interior = interior;
        return this;
    }

    public House Build()
    {
        return _house;
    }
}
