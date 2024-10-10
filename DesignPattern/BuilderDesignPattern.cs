namespace DesignPattern;


// Step 5: Use the Builder Pattern
public static class BuilderDesignPattern
{
    public static void Test()
    {
        IHouseBuilder concreteHouseBuilder = new ConcreteHouseBuilder();
        ConstructionDirector director = new ConstructionDirector(concreteHouseBuilder);

        director.ConstructHouse();
        House concreteHouse = director.GetHouse();
        Console.WriteLine("Concrete House: ");
        Console.WriteLine(concreteHouse);

        IHouseBuilder woodenHouseBuilder = new WoodenHouseBuilder();
        director = new ConstructionDirector(woodenHouseBuilder);

        director.ConstructHouse();
        House woodenHouse = director.GetHouse();
        Console.WriteLine("Wooden House: ");
        Console.WriteLine(woodenHouse);
    }
}

// Step 1: Define the Product
public class House
{
    public string Foundation { get; set; }
    public string Structure { get; set; }
    public string Roof { get; set; }
    public string Interior { get; set; }
    public override string ToString()
    {
        return $"Foundation: {Foundation}, Structure: {Structure}, Roof: {Roof}, Interior: {Interior}";
    }
}

// Step 2: Define the Builder Interface
public interface IHouseBuilder
{
    void BuildFoundation();
    void BuildStructure();
    void BuildRoof();
    void BuildInterior();
    House GetHouse();
}

// Step 3: Implement Concrete Builders
public class ConcreteHouseBuilder : IHouseBuilder
{
    private House _house = new House();

    public void BuildFoundation()
    {
        _house.Foundation = "Concrete, brick, and stone";
    }

    public void BuildStructure()
    {
        _house.Structure = "Wood and brick";
    }

    public void BuildRoof()
    {
        _house.Roof = "Tile";
    }

    public void BuildInterior()
    {
        _house.Interior = "Paint and wood";
    }

    public House GetHouse()
    {
        return _house;
    }
}

// Step 3: Implement Concrete Builders
public class WoodenHouseBuilder : IHouseBuilder
{
    private House _house = new House();

    public void BuildFoundation()
    {
        _house.Foundation = "Wooden piles";
    }

    public void BuildStructure()
    {
        _house.Structure = "Wood and bamboo";
    }

    public void BuildRoof()
    {
        _house.Roof = "Thatch";
    }

    public void BuildInterior()
    {
        _house.Interior = "Wood and bamboo";
    }

    public House GetHouse()
    {
        return _house;
    }
}


// Step 4: Create the Director
public class ConstructionDirector
{
    private readonly IHouseBuilder _houseBuilder;

    public ConstructionDirector(IHouseBuilder houseBuilder)
    {
        _houseBuilder = houseBuilder;
    }

    public void ConstructHouse()
    {
        _houseBuilder.BuildFoundation();
        _houseBuilder.BuildStructure();
        _houseBuilder.BuildRoof();
        _houseBuilder.BuildInterior();
    }

    public House GetHouse()
    {
        return _houseBuilder.GetHouse();
    }
}