using CsvHelper;
using CsvHelper.Configuration;
using HomeTask1.JSONModels;
using System.Globalization;

namespace HomeTask1
{
    internal class CsvProcessor
    {
        public string CsvFilePath { get; }
        public int InvalidLines { get; private set; }
        public int ParsedLines { get; private set; }

        public CsvProcessor(string csvFilePath)
        {
            CsvFilePath = csvFilePath;
            InvalidLines = 0;
            ParsedLines = 0;
        }


        public IEnumerable<CityObj> Process()
        {
            // I had no other choice...
            bool isRecordBad = false;
            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                TrimOptions = TrimOptions.Trim,
                BadDataFound = (_) =>
                {
                    isRecordBad = true;
                },
                HasHeaderRecord = Path.GetExtension(CsvFilePath).ToLower() == ".csv"
            };

            using StreamReader reader = new(CsvFilePath);
            using CsvReader csv = new(reader, config);
            Dictionary<string, CityObj> cityDictionary = new();

            csv.Context.RegisterClassMap<MyCsvClassMap>();


            while (csv.Read())
            {
                Payer? record;
                try
                {
                    record = csv.GetRecord<Payer>();
                    if (isRecordBad)
                    {
                        InvalidLines++;
                        isRecordBad = false;
                        continue;
                    }
                }
                catch (CsvHelperException)
                {
                    InvalidLines++;
                    continue;
                }

                if (record?.Address == null || record.Service == null) continue;
                string city = record.Address.Split(',')[0];
                if (!cityDictionary.ContainsKey(city))
                {
                    cityDictionary.Add(city, new CityObj
                    {
                        City = city,
                        Total = 0,
                        ServiceDictionary = new Dictionary<string, ServiceObj>()
                    });
                }

                cityDictionary[city].ServiceDictionary ??= new Dictionary<string, ServiceObj>();

                Dictionary<string, ServiceObj>? serviceDictionary = cityDictionary[city].ServiceDictionary;
                if (serviceDictionary != null && !serviceDictionary.ContainsKey(record.Service))
                {
                    cityDictionary[city].ServiceDictionary?.Add(record.Service, new ServiceObj()
                    {
                        Name = record.Service,
                        Total = 0,
                        Payers = new List<PayerObj>()
                    }
                    );
                    cityDictionary[city].Total++;
                }

                cityDictionary[city].ServiceDictionary?[record.Service].Payers?.Add(new PayerObj()
                {
                    Name = $"{record.FirstName} {record.LastName}",
                    AccountNumber = record.AccountNumber,
                    Date = record.Date.ToString("MM-dd-yyyy"),
                    Payment = record.Payment
                });
                serviceDictionary = cityDictionary[city].ServiceDictionary;
                if (serviceDictionary != null)
                    serviceDictionary[record.Service].Total++;
                ParsedLines++;
                isRecordBad = false;
            }

            return cityDictionary.Values.AsEnumerable();
        }
    }
}
