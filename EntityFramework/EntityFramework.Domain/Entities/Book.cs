using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Domain.Entities;

public class Book
{
    public Guid BookId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid AuthorId { get; set; }
    public Author Author { get; set; }
    public Guid PublisherId { get; set; }
    public Publisher Publisher { get; set; }
}
