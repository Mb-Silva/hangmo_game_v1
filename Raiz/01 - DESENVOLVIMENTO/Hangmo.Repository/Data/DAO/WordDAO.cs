using Hangmo.Repository.Data.DAO;
using Hangmo.Repository.Data.Entities;
using Hangmo.Repository.Data;
using Hangmo.Repository.Data.DAO.Interfaces;
using Microsoft.EntityFrameworkCore;

public class WordDAO : BaseDAO<Word>
{
    protected readonly AppDbContext _context;
    public WordDAO(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public List<Word> ListByDate(DateTime date)
    {
        var dateToSearch = date.Date;

        var utcDateToSearch = dateToSearch.ToUniversalTime();

        var items = _context.Set<Word>()
                        .Where(w => w.Date.ToUniversalTime().Year == utcDateToSearch.Year &&
                                    w.Date.ToUniversalTime().Month == utcDateToSearch.Month &&
                                    w.Date.ToUniversalTime().Day == utcDateToSearch.Day);



        return items.ToList();
    }
}