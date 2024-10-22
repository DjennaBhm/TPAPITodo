public static class SeedData
{
    public static void Initialize(Api_TodoContext context)
    {
        context.Api_Todo.RemoveRange(context.Api_Todo);

        // Création de l'instance de Todo avec les données à insérer.
        Api_Todo course = new Api_Todo
        {
            Task = "Course",
            Deadline = DateTime.Parse("2024-10-16"),
        };
        Api_Todo gossip = new Api_Todo
        {
            Task = "Gossip",
            Completed = false,

        };
        Api_Todo dormir = new Api_Todo
        {
            Task = "Dormir",
            Completed = true,
        };

        // Ajout de l'élément à la base de données.
        context.Api_Todo.Add(course);
        context.Api_Todo.Add(gossip);
        context.Api_Todo.Add(dormir);

        // Sauvegarde des changements dans la base de données.
        context.SaveChanges();
    }
}
