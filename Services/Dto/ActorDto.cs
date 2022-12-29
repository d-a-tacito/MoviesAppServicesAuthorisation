using System;
using System.ComponentModel.DataAnnotations;
using MoviesApp.Filters;

namespace MoviesApp.Services.Dto;

public class ActorDto
{
    public int? Id { get; set; }
    
    [Required]
    [StringLength(32, ErrorMessage = "Firstname length can't be more than 32.")]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(32, ErrorMessage = "Lastname length can't be more than 32.")]
    public string LastName { get; set; }
    
    [Required]
    [AgeOfActor(4,99)]
    public DateTime BirthDate { get; set; }
}