public class Api_Todo
{
    public int Id { get; set; } // L'ID est généralement généré par la base de données
    public string Task { get; set; } // Propriété requise
    public bool Completed { get; set; }
    public DateTime? Deadline { get; set; }
    //public int? AgendaId { get; set; } // Id de l'agenda associé, optionnel
    //public Agenda Agenda { get; set; } // Relation avec Agenda
}
