using CsvHelper.Configuration;

namespace HomeTask1
{
    public class MyCsvClassMap : ClassMap<Payer>
    {
        public MyCsvClassMap()
        {
            Map(m => m.FirstName).Name("first_name");
            Map(m => m.LastName).Name("last_name");
            Map(m => m.Address).Name("address");
            Map(m => m.Payment).Name("payment");
            Map(m => m.Date).Name("date").TypeConverterOption.Format("yyyy-dd-mm");
            Map(m => m.AccountNumber).Name("account_number");
            Map(m => m.Service).Name("service");
        }
    }
}
