using IND.klasser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace IND
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            var schoolService = new SchoolService();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Välj ett alternativ:");
                Console.WriteLine("1) Visa personalöversikt");
                Console.WriteLine("2) Lägg till ny personal");
                Console.WriteLine("3) Visa elever och deras klasser");
                Console.WriteLine("4) Lägg till betyg för en elev");
                Console.WriteLine("5) Lägg till ny elev");
                Console.WriteLine("6) Lägg till ny kurs"); // Nytt alternativ
                Console.WriteLine("7) Visa lärare och avdelningar");
                Console.WriteLine("8) Visa aktiva kurser");
                Console.WriteLine("9) Visa lön per avdelning");
                Console.WriteLine("10) Visa medellön per avdelning");
                Console.WriteLine("11) Hämta elevinformation via ID");
                Console.WriteLine("12) Uppdatera personalens lön");
                Console.WriteLine("13) Hämta elevinformation via stored procedure");
                Console.WriteLine("0) Avsluta");
                Console.Write("\r\nVälj ett alternativ: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        schoolService.VisaPersonalOversikt();
                        break;
                    case "2":
                        Console.Write("Ange namn: ");
                        string namn = Console.ReadLine();
                        Console.Write("Ange befattning: ");
                        string befattning = Console.ReadLine();
                        Console.Write("Ange anställningsdatum (YYYY-MM-DD): ");
                        DateTime anstallningsDatum = DateTime.Parse(Console.ReadLine());
                        Console.Write("Ange lön: ");
                        decimal lonBelopp = decimal.Parse(Console.ReadLine());

                        var newPersonal = new Personal
                        {
                            Namn = namn,
                            Befattning = befattning,
                            AnstallningsDatum = anstallningsDatum
                        };
                        var newLon = new Lon
                        {
                            LonBelopp = lonBelopp,
                            Avdelning = befattning // Assuming department is the same as position
                        };

                        schoolService.LäggTillNyPersonal(newPersonal, newLon);
                        break;
                        
                    case "3":
                        schoolService.VisaEleverOchKlasser();
                        break;
                    case "4":
                        Console.Write("Ange elev ID: ");
                        int elevID = int.Parse(Console.ReadLine());
                        Console.Write("Ange kurs ID: ");
                        int kursID = int.Parse(Console.ReadLine());
                        Console.Write("Ange lärare ID: ");
                        int larareID = int.Parse(Console.ReadLine());
                        Console.Write("Ange betyg: ");
                        string betyg = Console.ReadLine();

                        var newBetyg = new Betyg
                        {
                            ElevID = elevID,
                            KursID = kursID,
                            LarareID = larareID,
                            BetygValue = betyg,
                            Datum = DateTime.Now
                        };

                        schoolService.SattBetygMedTransaction(newBetyg);
                        break;
                    case "5":
                        Console.Write("Ange namn: ");
                        string elevNamn = Console.ReadLine();
                        Console.Write("Ange klass: ");
                        string klass = Console.ReadLine();

                        var newElev = new Elev
                        {
                            Namn = elevNamn,
                            Klass = klass
                        };
                        schoolService.LäggTillNyElev(newElev);
                        break;
                    case "6": // Lägg till ny kurs
                        Console.Write("Ange kursnamn: ");
                        string kursNamn = Console.ReadLine();
                        Console.Write("Är kursen aktiv? (true/false): ");
                        bool aktiv = bool.Parse(Console.ReadLine());

                        var newKurs = new Kurs
                        {
                            KursNamn = kursNamn,
                            Aktiv = aktiv
                        };
                        schoolService.LäggTillNyKurs(newKurs);
                        break;
                    case "7":
                        schoolService.GetLararAvdelningar();
                        break;
                    case "8":
                        schoolService.VisaAktivaKurser();
                        break;
                    case "9":
                        schoolService.VisaLonPerAvdelning();
                        break;
                    case "10":
                        schoolService.VisaMedellonPerAvdelning();
                        break;
                    case "11":
                        Console.Write("Ange elev ID: ");
                        int elevIDInfo = int.Parse(Console.ReadLine());
                        schoolService.GetElevInfo(elevIDInfo);
                        break;
                    case "12":
                        Console.Write("Ange personalens ID: ");
                        int personID = int.Parse(Console.ReadLine());
                        Console.Write("Ange ny lön: ");
                        decimal nyLon = decimal.Parse(Console.ReadLine());
                        schoolService.UppdateraLon(personID, nyLon);
                        break;
                    case "13":
                        Console.Write("Ange elev ID: ");
                        int elevIDSP = int.Parse(Console.ReadLine());
                        schoolService.GetElevInfo(elevIDSP);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
                Console.WriteLine("\r\nTryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        public class SchoolService
        {
            public void VisaPersonalOversikt()
            {
                using (var context = new SchoolContext())
                {
                    var personalList = context.Personals
                        .Include(p => p.Lons) // Include Lons to get salary information
                        .Select(p => new
                        {
                            p.Namn,
                            p.Befattning,
                            ArPaSkolan = EF.Functions.DateDiffYear(p.AnstallningsDatum, DateTime.Now),
                            Lon = p.Lons.FirstOrDefault() // Assume one salary per personal
                        }).ToList();

                    personalList.ForEach(p =>
                    {
                        var lonBelopp = p.Lon?.LonBelopp ?? 0; // Handle cases where there might be no salary record
                        Console.WriteLine($"Namn: {p.Namn}, Befattning: {p.Befattning}, År på skolan: {p.ArPaSkolan}, Lön: {lonBelopp}");
                    });
                }
            }

            public void UppdateraLon(int personID, decimal nyLon)
            {
                using (var context = new SchoolContext())
                {
                    var personal = context.Personals
                        .Include(p => p.Lons)
                        .FirstOrDefault(p => p.PersonID == personID);

                    if (personal != null)
                    {
                        // Antag att det bara finns en lön per personal
                        var lon = personal.Lons.FirstOrDefault();
                        if (lon != null)
                        {
                            lon.LonBelopp = nyLon;
                            context.SaveChanges();
                            Console.WriteLine("Lön uppdaterades framgångsrikt.");
                        }
                        else
                        {
                            Console.WriteLine("Ingen lön hittades för denna personal.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ingen personal hittades med detta ID.");
                    }
                }
            }
            public void LäggTillNyPersonal(Personal newPersonal, Lon newLon)
            {
                using (var context = new SchoolContext())
                {
                    try
                    {
                        context.Personals.Add(newPersonal);
                        context.SaveChanges();

                        
                        newLon.PersonID = newPersonal.PersonID;
                        context.Lons.Add(newLon);
                        context.SaveChanges();

                        Console.WriteLine("Personal och lön lades till framgångsrikt.");
                    }
                    catch (DbUpdateException dbEx)
                    {
                        Console.WriteLine($"Ett databasuppdateringsfel uppstod: {dbEx.InnerException?.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ett fel uppstod: {ex.Message}");
                        Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                    }
                }
            }
            public void LäggTillNyElev(Elev newElev)
            {
                using (var context = new SchoolContext())
                {
                    try
                    {
                        context.Elevers.Add(newElev);
                        context.SaveChanges();
                        Console.WriteLine("Elev added successfully.");
                    }
                    catch (DbUpdateException dbEx)
                    {
                        Console.WriteLine($"A database update error occurred: {dbEx.InnerException?.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                    }
                }
            }
            public void LäggTillNyKurs(Kurs newKurs) 
            {
                using (var context = new SchoolContext())
                {
                    try
                    {
                        context.Kursers.Add(newKurs);
                        context.SaveChanges();
                        Console.WriteLine("Kurs added successfully.");
                    }
                    catch (DbUpdateException dbEx)
                    {
                        Console.WriteLine($"A database update error occurred: {dbEx.InnerException?.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                    }
                }
            }

            public void VisaEleverOchKlasser()
            {
                using (var context = new SchoolContext())
                {
                    var elever = context.Elevers.ToList();
                    elever.ForEach(e => Console.WriteLine($"ID: {e.ElevID}, Namn: {e.Namn}, Klass: {e.Klass}"));
                }
            }

            public void LäggTillBetyg(Betyg newBetyg)
            {
                using (var context = new SchoolContext())
                {
                    context.Betygs.Add(newBetyg);
                    context.SaveChanges();
                }
            }
            

            public void GetLararAvdelningar()
            {
                using (var context = new SchoolContext())
                {
                    var result = context.Personals.GroupBy(p => p.Befattning)
                        .Select(g => new { Befattning = g.Key, AntalLarare = g.Count() })
                        .ToList();

                    result.ForEach(r => Console.WriteLine($"Avdelning: {r.Befattning}, Antal Lärare: {r.AntalLarare}"));
                }
            }

            public void VisaAktivaKurser()
            {
                using (var context = new SchoolContext())
                {
                    var kurser = context.Kursers.Where(k => k.Aktiv).ToList();
                    kurser.ForEach(k => Console.WriteLine($"KursID: {k.KursID}, KursNamn: {k.KursNamn}"));
                }
            }

            public void VisaLonPerAvdelning()
            {
                using (var context = new SchoolContext())
                {
                    var lonPerAvdelning = context.Lons
                        .GroupBy(l => l.Avdelning)
                        .Select(g => new { Avdelning = g.Key, TotalLon = g.Sum(l => l.LonBelopp) })
                        .ToList();

                    lonPerAvdelning.ForEach(l => Console.WriteLine($"Avdelning: {l.Avdelning}, Total Lön: {l.TotalLon}"));
                }
            }

            public void VisaMedellonPerAvdelning()
            {
                using (var context = new SchoolContext())
                {
                    var medellonPerAvdelning = context.Lons
                        .GroupBy(l => l.Avdelning)
                        .Select(g => new { Avdelning = g.Key, Medellon = g.Average(l => l.LonBelopp) })
                        .ToList();

                    medellonPerAvdelning.ForEach(l => Console.WriteLine($"Avdelning: {l.Avdelning}, Medellön: {l.Medellon}"));
                }
            }

            public void GetElevInfo(int elevID)
            {
                using (var context = new SchoolContext())
                {
                    var elevInfo = context.ElevInfos.FromSqlRaw("EXEC GetElevInfoById @p0", elevID).ToList();

                    if (elevInfo.Any())
                    {
                        foreach (var info in elevInfo)
                        {
                            Console.WriteLine($"Elev ID: {info.ElevID}, Namn: {info.Namn}, Klass: {info.Klass}, Betyg: {info.BetygValue}, Kurs: {info.KursNamn}, Lärare: {info.LarareNamn}, Lön: {info.LonBelopp}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ingen elev hittades med detta ID.");
                    }
                }
            }


            public void SattBetygMedTransaction(Betyg newBetyg)
            {
                using (var context = new SchoolContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Betygs.Add(newBetyg);
                            context.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
        }
    }
}