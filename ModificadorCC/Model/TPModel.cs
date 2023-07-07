using System.Collections.Generic;

namespace ModificadorCC.Model;

public class TPModel
{
    public string ResourceType { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string StartDate { get; set; }
    public string CreateDate { get; set; }
    public string ModifyDate { get; set; }
    public string LastCommentDate { get; set; }
    public string Tags { get; set; }
    public double NumericPriority { get; set; }
    public double Effort { get; set; }
    public double EffortCompleted { get; set; }
    public double EffortToDo { get; set; }
    public double Progress { get; set; }
    public double TimeSpent { get; set; }
    public double TimeRemain { get; set; }
    public string LastStateChangeDate { get; set; }
    public double InitialEstimate { get; set; }
    public string Units { get; set; }
    public EntityType EntityType { get; set; }
    public Project Project { get; set; }
    public LastEditor LastEditor { get; set; }
    public Owner Owner { get; set; }
    public Creator Creator { get; set; }
    public LastCommentedUser LastCommentedUser { get; set; }
    public TeamIteration TeamIteration { get; set; }
    public Team Team { get; set; }
    public Priority Priority { get; set; }
    public EntityState EntityState { get; set; }
    public ResponsibleTeam ResponsibleTeam { get; set; }
    public Feature Feature { get; set; }
    public List<CustomField> CustomFields { get; set; }
}

public class EntityType
{
    public string ResourceType { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Process
{
    public string ResourceType { get; set; }
    public int Id { get; set; }
}

public class Project
{
    public string ResourceType { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public Process Process { get; set; }
}

public class User
{
    public string ResourceType { get; set; }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Login { get; set; }
    public string FullName { get; set; }
}

public class LastEditor : User { }

public class Owner : User { }

public class Creator : User { }

public class LastCommentedUser : User { }

public class TeamIteration
{
    public string ResourceType { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Team
{
    public string ResourceType { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Priority
{
    public string ResourceType { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public int Importance { get; set; }
}

public class EntityState
{
    public string ResourceType { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public double NumericPriority { get; set; }
}

public class ResponsibleTeam
{
    public string ResourceType { get; set; }
    public int Id { get; set; }
}

public class Feature
{
    public string ResourceType { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
}

public class CustomField
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
}

public class Root
{
    public List<TPModel> Items { get; set; }
}
