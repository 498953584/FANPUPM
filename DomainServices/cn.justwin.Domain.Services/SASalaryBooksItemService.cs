namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SASalaryBooksItemService : Repository<SASalaryBooksItem>
    {
        public SASalaryBooksItem GetById(string id)
        {
            SASalaryBooksItem item;
            try
            {
                item = (from sbi in this
                    where sbi.Id == id
                    select sbi).FirstOrDefault<SASalaryBooksItem>();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return item;
        }

        public List<string> GetItems(string booksId)
        {
            List<string> list;
            try
            {
                list = (from sbi in this
                    where sbi.BooksId == booksId
                    select sbi.ItemId).ToList<string>();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }
    }
}

