using System.ComponentModel.DataAnnotations;

namespace TaskManager.Enums;

public enum TaskState
{
    [Display(Name = "Pendente")]
    Pendente = 0,

    [Display(Name = "Em andamento")]
    EmAndamento = 1,

    [Display(Name = "Concluída")]
    Concluida = 2,

    [Display(Name = "Atrasada")]
    Atrasada = 3,

    [Display(Name = "Cancelada")]
    Cancelada = 4
}