using System.Collections.Generic;

public class Root
{
    public List<Item> Items { get; set; }
}

public class Item
{
    public string ResourceType { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public EntityType EntityType { get; set; }
    public Project Project { get; set; }
    public Assignments Assignments { get; set; }
    public List<CustomField> CustomFields { get; set; }
}

public class EntityType
{
    public string ResourceType { get; set; }
    public int Id { get; set; }
}

public class Project
{
    public string ResourceType { get; set; }
    public int Id { get; set; }
    public Process Process { get; set; }
}

public class Process
{
    public string ResourceType { get; set; }
    public int Id { get; set; }
}

public class Assignments
{
    public List<Assignment> Items { get; set; }
}

public class Assignment
{
    public string ResourceType { get; set; }
    public int Id { get; set; }
    public GeneralUser GeneralUser { get; set; }
    public Role Role { get; set; }
}

public class GeneralUser
{
    public string ResourceType { get; set; }
    public string Kind { get; set; }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Login { get; set; }
    public string FullName { get; set; }
}

public class Role
{
    public string ResourceType { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
}

public class CustomField
{
    public string Name { get; set; }
    public string Type { get; set; }
    public object Value { get; set; } // object because Value can be different types: string, bool, null
}