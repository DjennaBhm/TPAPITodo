public static class SeedData
{
    public static async Task InitializeAsync(Api_TodoContext context)
    {

        context.Api_Todo.RemoveRange(context.Api_Todo);
        //context.Agendas.RemoveRange(context.Agendas);

        // Création des agendas
        //Agenda chores = new Agenda { Name = "Chores" };
        //Agenda holidays = new Agenda { Name = "Holidays" };

        // Création des tâches et association avec un agenda
        Api_Todo course = new Api_Todo { Task = "Course", Deadline = DateTime.Parse("2024-10-16") };
        Api_Todo gossip = new Api_Todo { Task = "Gossip", Completed = false };
        Api_Todo dormir = new Api_Todo { Task = "Dormir", Completed = true };

        // Ajout des éléments à la base de données
        //context.Agendas.AddRange(chores, holidays);
        context.Api_Todo.AddRange(course, gossip, dormir);

        // Sauvegarde des changements dans la base de données
        await context.SaveChangesAsync();

    }
}
