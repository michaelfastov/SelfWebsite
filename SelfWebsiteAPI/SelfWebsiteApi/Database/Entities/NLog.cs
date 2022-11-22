﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfWebsiteApi.Database.Entities;

public class NLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Message { get; set; }
    public string Level { get; set; }
    public string StackTrace { get; set; }
    public string Exception { get; set; }
    public string Logger { get; set; }
    public string Url { get; set; }
}
