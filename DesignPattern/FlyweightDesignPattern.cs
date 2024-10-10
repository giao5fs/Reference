using System.Data;

namespace DesignPattern;

public static class FlyweightDesignPattern
{
    public static void Test()
    {
        UserRolePermissionsFactory rolePermissionsFactory = new UserRolePermissionsFactory();

        // Common roles and permissions
        var rolesAdmin = new List<string> { "Admin", "Editor" };
        var permissionsAdmin = new Dictionary<string, bool> { { "Read", true }, { "Write", true }, { "Delete", true } };
        var rolePermissionsAdmin = rolePermissionsFactory.GetRolePermissions(rolesAdmin, permissionsAdmin);

        var rolesUser = new List<string> { "User" };
        var permissionsUser = new Dictionary<string, bool> { { "Read", true }, { "Write", false }, { "Delete", false } };
        var rolePermissionsUser = rolePermissionsFactory.GetRolePermissions(rolesUser, permissionsUser);

        // User-specific settings
        var userSettings1 = new Dictionary<string, string> { { "Theme", "Dark" }, { "Language", "English" } };
        var userSettings2 = new Dictionary<string, string> { { "Theme", "Light" }, { "Language", "French" } };

        // Creating user sessions
        UserSession session1 = new UserSession("user1", rolePermissionsAdmin, userSettings1);
        UserSession session2 = new UserSession("user2", rolePermissionsUser, userSettings2);

        // Displaying user sessions
        session1.Display();
        session2.Display();
    }
}

public class UserRolePermissions
{
    public List<string> Roles { get; private set; }
    public Dictionary<string, bool> Permissions { get; private set; }

    public UserRolePermissions(List<string> roles, Dictionary<string, bool> permissions)
    {
        Roles = roles;
        Permissions = permissions;
    }
}

// Encapsulation
// Inheritance
// Abstraction
// Polymopholism

public class UserRolePermissionsFactory
{
    private Dictionary<string, UserRolePermissions> _rolePermissions = new Dictionary<string, UserRolePermissions>();

    public UserRolePermissions GetRolePermissions(List<string> roles, Dictionary<string, bool> permissions)
    {
        string key = string.Join(",", roles) + "|" + string.Join(",", permissions.Select(p => p.Key + "=" + p.Value));

        if (!_rolePermissions.ContainsKey(key))
        {
            _rolePermissions[key] = new UserRolePermissions(roles, permissions);
        }
        return _rolePermissions[key];
    }
}

public class UserSession
{
    public string UserId { get; private set; }
    public UserRolePermissions RolePermissions { get; private set; }
    public Dictionary<string, string> UserSettings { get; private set; }

    public UserSession(string userId, UserRolePermissions rolePermissions, Dictionary<string, string> userSettings)
    {
        UserId = userId;
        RolePermissions = rolePermissions;
        UserSettings = userSettings;
    }

    public void Display()
    {
        Console.WriteLine($"User: {UserId}, Roles: {string.Join(", ", RolePermissions.Roles)}, Permissions: {string.Join(", ", RolePermissions.Permissions.Select(p => p.Key + "=" + p.Value))}, Settings: {string.Join(", ", UserSettings.Select(s => s.Key + "=" + s.Value))}");
    }
}

//Before Using the Flyweight Pattern
//Memory Usage: Each user session creates its own instances of roles and permissions, leading to high memory consumption.
//Redundancy: Many objects may contain the same roles and permissions data, leading to redundancy.

//After Using the Flyweight Pattern
//Memory Efficiency: Shared role and permission objects mean that roles and permissions are stored once and reused across multiple sessions.
//Reduced Redundancy: Only one instance of each unique set of roles and permissions is created and shared.
//Extrinsic State Management: Unique user settings are managed outside the flyweight object and passed in as needed, reducing the overall memory footprint.


// Original code: Without the Flyweight Pattern.

//public class UserSession
//{
//    public string UserId { get; private set; }
//    public List<string> Roles { get; private set; }
//    public Dictionary<string, bool> Permissions { get; private set; }

//    public UserSession(string userId, List<string> roles, Dictionary<string, bool> permissions)
//    {
//        UserId = userId;
//        Roles = roles;
//        Permissions = permissions;
//    }

//    public void Display()
//    {
//        Console.WriteLine($"User: {UserId}, Roles: {string.Join(", ", Roles)}, Permissions: {string.Join(", ", Permissions.Select(p => p.Key + "=" + p.Value))}");
//    }
//}
