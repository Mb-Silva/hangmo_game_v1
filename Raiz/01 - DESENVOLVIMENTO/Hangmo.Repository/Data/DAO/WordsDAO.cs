using Hangmo.Repository.Data.DAO;
using Hangmo.Repository.Data.Entities;
using Hangmo.Repository.Data;
using Hangmo.Repository.Data.DAO.Interfaces;
using Microsoft.EntityFrameworkCore;

public class WordsDAO : BaseDAO<Words>
{
    protected readonly AppDbContext _context;
    public WordsDAO(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public List<Words> ListByDate(DateTime date)
    {
        var dateToSearch = date.Date;

        var utcDateToSearch = dateToSearch.ToUniversalTime();

        var items = _context.Set<Words>()
                        .Where(w => w.Date.ToUniversalTime().Year == utcDateToSearch.Year &&
                                    w.Date.ToUniversalTime().Month == utcDateToSearch.Month &&
                                    w.Date.ToUniversalTime().Day == utcDateToSearch.Day);



        return items.ToList();
    }
}