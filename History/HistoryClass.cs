using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TimeTrackerWPF.Data;
using TimeTrackerWPF.Shared;

namespace TimeTrackerWPF.History
{
    public class HistoryClass
    {
        public ObservableCollection<Record> Records { get; private set; }

        public HistoryClass()
        {
            using var db = new TimeTrackerDbContext();
            db.Database.EnsureCreated();

            var records = db.Records.OrderByDescending(r => r.StartDate).ToList();
            Records = new ObservableCollection<Record>(records);
        }

        public void AddRecord(Record record)
        {
            using var db = new TimeTrackerDbContext();
            db.Records.Add(record);
            db.SaveChanges();

            Records.Insert(0, record);
        }

        public void DeleteRecord(Record record)
        {
            using var db = new TimeTrackerDbContext();
            var entity = db.Records.Find(record.Id);
            if (entity != null)
            {
                db.Records.Remove(entity);
                db.SaveChanges();
            }

            Records.Remove(record);
        }

        public void EditRecord(Record updatedRecord)
        {
            using var db = new TimeTrackerDbContext();
            var existing = db.Records.Find(updatedRecord.Id);

            if (existing != null)
            {
                existing.Name = updatedRecord.Name;
                existing.StartDate = updatedRecord.StartDate;
                existing.EndDate = updatedRecord.EndDate;

                db.SaveChanges();

                var index = Records.IndexOf(Records.First(r => r.Id == updatedRecord.Id));
                if (index >= 0)
                    Records[index] = updatedRecord;
            }
        }
    }
}
