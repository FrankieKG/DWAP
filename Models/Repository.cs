using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;
using System.Collections;
using WebApplication5.Models.POCO.Utilities;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication5.Models;

public class Repository : IRepository
{
    
    public Repository(ApplicationDbContext context)
    {
      this.context = context;
    }


    private readonly ApplicationDbContext context;

    public IQueryable<ApplicationAndEvaluation> ApplicationAndEvaluations => context.ApplicationAndEvaluations;
    public IQueryable<Organization> Organizations => context.Organizations;
    public IQueryable<Participant> Participants => context.Participants;
    public IQueryable<Payment> Payments => context.Payments;
    public IQueryable<PreviousApplication> PreviousApplications => context.PreviousApplications;
    public IQueryable<Program> Programs => context.Programs;
    public IQueryable<ReportAndReclaim> ReportAndReclaims => context.ReportAndReclaims;
    public IQueryable<ScholarshipAndGrant> ScholarshipAndGrants => context.ScholarshipAndGrants;


    public List<ApplicationAndEvaluation> ApplicationAndEvaluationList { get; set; }
    public List<Organization> OrganizationList { get; set; }
    public List<Participant> ParticipantList { get; set; }
    public List<Payment> PaymentList { get; set; }
    public List<PreviousApplication> PreviousApplicationList { get; set; }
    public List<Program> ProgramList { get; set; }
    public List<ReportAndReclaim> ReportAndReclaimList { get; set; }
    public List<ScholarshipAndGrant> ScholarshipAndGrantList { get; set; }


    #region Filinläsning
    //Läser data från en Excel-fil och konverterar den till fält i POCO-klasser:
    public async Task ReadFile(IFormFile file)
    {

        //Läser in en nedsparad JSON-sträng med Dictionary-data:
        string jsonString = File.ReadAllText("columnMappings.json");

        //Packar upp JSON-strängen till ett Dictionary:
        Dictionary<string, (string, Type)> columnMappings = JsonConvert.DeserializeObject<Dictionary<string, (string, Type)>>(jsonString);

        await ExcelImporter(file, columnMappings);
    }


    private async Task ExcelImporter(IFormFile file, Dictionary<string, (string, Type)> columnMappings)
    {
        
        ApplicationAndEvaluationList = new List<ApplicationAndEvaluation>();
        OrganizationList = new List<Organization>();
        ParticipantList = new List<Participant>();
        PaymentList = new List<Payment>();
        PreviousApplicationList = new List<PreviousApplication>();
        ProgramList = new List<Program>();
        ReportAndReclaimList = new List<ReportAndReclaim>();
        ScholarshipAndGrantList = new List<ScholarshipAndGrant>();


        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);

            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                // Matchar kolumnnamn i Excelfilen mot Dictionary-values och returnerar en lista med rätt
                // kolumnnamn för tabellerna i databasen:
                Dictionary<string, Type> HeaderProperties = GetHeaderProperties(worksheet, columnMappings);

                for (int row = 2; row <= rowCount; row++) // Börjar på rad 2 för att hoppa över rubrikerna
                {

                    Dictionary<Type, object> modelInstances = new Dictionary<Type, object>
                    {
                        { typeof(ApplicationAndEvaluation), new ApplicationAndEvaluation() },
                        { typeof(Organization), new Organization() },
                        { typeof(Participant), new Participant() },
                        { typeof(Payment), new Payment() },
                        //{ typeof(PreviousApplication), new PreviousApplication() },
                        { typeof(Program), new Program() },
                        { typeof(ReportAndReclaim), new ReportAndReclaim() },
                        { typeof(ScholarshipAndGrant), new ScholarshipAndGrant() }
                    };


                    //Felkontroll för att vara säker på att kolumnerna i Excel finns i Dictionaryt:
                    CheckIfNoOfColumnsMatch(HeaderProperties, colCount);


                    for (int col = 1; col <= colCount; col++)
                    {
                        //Kolumnnamnet i databasen som ska skrivas till:
                        var colName = HeaderProperties.ElementAt(col-1).Key.ToString();

                        //Datat i den aktuella cellen:
                        var cellValue = worksheet.Cells[row, col].Value;
                        string cellData = cellValue != null ? cellValue.ToString() : string.Empty;


                        foreach (var model in modelInstances)
                        {
                            var modelType = model.Key;
                            var modelInstance = model.Value;

                            if (HeaderProperties.ElementAt(col - 1).Value == modelType || colName == "Dnr")
                            {
                                PropertyInfo prop = modelType.GetProperty(colName);

                                if (prop != null)
                                {
                                    var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                                    var propValue = FormatCellValue(propType, cellData, colName, row, col);
                                    prop.SetValue(modelInstance, propValue);
                                }
                            }
                        }
                    }
                    AddObjectsToLists(modelInstances);
                }
            }

            context.ApplicationAndEvaluations.AddRange(ApplicationAndEvaluationList);
            context.Organizations.AddRange(OrganizationList);
            context.Participants.AddRange(ParticipantList);
            context.Payments.AddRange(PaymentList);
            context.PreviousApplications.AddRange(PreviousApplicationList);
            context.Programs.AddRange(ProgramList);
            context.ReportAndReclaims.AddRange(ReportAndReclaimList);
            context.ScholarshipAndGrants.AddRange(ScholarshipAndGrantList);

            context.SaveChanges();
        }
    }




    private void AddObjectsToLists(Dictionary<Type, object> modelInstances)
    {
        foreach (var model in modelInstances)
        {
            var modelType = model.Key;
            var modelInstance = model.Value;

            PropertyInfo listProp = GetType().GetProperty(modelType.Name + "List");

            if (listProp != null)
            {
                IList list = (IList)listProp.GetValue(this);

                if (HasNonDefaultProperties(modelInstance))
                {
                    list.Add(modelInstance);
                }
            }
        }
    }


    private bool HasNonDefaultProperties(object obj)
    {
        var type = obj.GetType();
        var properties = type.GetProperties();

        foreach (var prop in properties)
        {
            // Skip the Dnr property:
            if (prop.Name == "Dnr")
                continue;

            var defaultValue = prop.PropertyType.IsValueType ? Activator.CreateInstance(prop.PropertyType) : null;
            var value = prop.GetValue(obj);

            if (value == null)
            {
                continue;
            }

            if (!value.Equals(defaultValue))
            {
                return true;
            }
        }

        return false;
    }


    private object FormatCellValue(Type propType, string cellData, string colName, int row, int col)
    {
        //Meddelande som kan användas för felsökning av NULL-värden osv
        //Här ser vi vilken data som skrivs till vilken kolumn i databasen,
        //vad kolumnen heter i Excel-filen och vilken rad och vilket kolumnnummer på den raden

        //Vi skulle kunna logga detta på något vis i den färdiga applikationen, det hade varit snyggt
        Console.WriteLine($"Formatting cell data: {cellData} to type: {propType.Name} in column: {colName} on row {row}, column {col}" );

        switch (Type.GetTypeCode(propType))
        {
            case TypeCode.Int32:
                int intValue;
                return int.TryParse(cellData, NumberStyles.Any, CultureInfo.InvariantCulture, out intValue) ? intValue : null;
            case TypeCode.DateTime:
                if (string.IsNullOrWhiteSpace(cellData))
                    return null;
                DateTime dateValue;
                return DateTime.TryParseExact(cellData, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue) ? dateValue : null;
            case TypeCode.Single:
                float floatValue;
                return float.TryParse(cellData, NumberStyles.Any, CultureInfo.InvariantCulture, out floatValue) ? floatValue : null;
            default:
                return Convert.ChangeType(cellData, propType, CultureInfo.InvariantCulture);
        }
    }


    private void CheckIfNoOfColumnsMatch(Dictionary<string, Type> HeaderProperties, int colCount)
    {
        if (HeaderProperties.Count < colCount)
        {
            throw new Exception($"HeaderProperties has {HeaderProperties.Count} items, but col is {colCount}");
        }
    }


    private Dictionary<string, Type> GetHeaderProperties(ExcelWorksheet worksheet, Dictionary<string, (string, Type)> columnMappings)
    {
        ExcelRange headerCells = worksheet.Cells[1, 1, 1, worksheet.Dimension.Columns];
        List<string> columnNames = headerCells.Select(cell => cell.Value?.ToString().Trim()).ToList();

        Dictionary<string, Type> headerProperties = new();
        foreach (string columnName in columnNames)
        {
            if (columnMappings.TryGetValue(columnName, out var mapping))
            {
                headerProperties[mapping.Item1] = mapping.Item2;
            }
            else
            {
                throw new Exception("Column " + columnName + " mismatch");
            }
        }

        return headerProperties;
    }
    #endregion


    #region API-Metoder
    //Färdiga:

    #region Atlas Partnerskap

    public IQueryable<AtlasPartnerskapData> GetAtlasPartnerskapDnr(string dnr)
    {
        var query = from ae in context.ApplicationAndEvaluations
                    join p in context.Payments on ae.Dnr equals p.Dnr
                    join pa in context.Participants on ae.Dnr equals pa.Dnr
                    join pr in context.Programs on ae.Dnr equals pr.Dnr
                    where ae.Dnr == dnr && p.Dnr == dnr && pa.Dnr == dnr
                    && pr.ProgramName == "Atlas partnerskap"
                    select new AtlasPartnerskapData
                    {
                        Dnr = ae.Dnr,
                        Period = ae.Period,
                        ApplicationStatus = ae.ApplicationStatus,
                        Total_Granted_Amount = p.Total_Granted_Amount,
                        Total_Approved_Amount = p.Total_Approved_Amount,
                        Applied_Student_Number = pa.Applied_Student_Number,
                        Approved_Student_Number = pa.Approved_Student_Number,
                        Granted_Participant_Number = pa.Granted_Participant_Number,
                        Reported_Participant_Number = pa.Reported_Participant_Number,
                        Reported_Women_Student_Number = pa.Reported_Women_Student_Number,
                        Reported_Men_Student_Number = pa.Reported_Men_Student_Number,
                        Reported_Women_Teacher_Number = pa.Reported_Women_Teacher_Number,
                        Reported_Men_Teacher_Number = pa.Reported_Men_Teacher_Number,
                        Reported_Women_SchoolLeader_Number = pa.Reported_Women_SchoolLeader_Number,
                        Reported_Men_SchoolLeader_Number = pa.Reported_Men_SchoolLeader_Number,
                        Reported_Women_AssociatedStaff_Number = pa.Reported_Women_AssociatedStaff_Number,
                        Reported_Men_AssociatedStaff_Number = pa.Reported_Men_AssociatedStaff_Number
                    };

        return query;
    }


    public IQueryable<AtlasPartnerskapData> GetPeriodAtlasPartnerskap(string fromPeriod, string toPeriod)
    {
        var query = from ae in context.ApplicationAndEvaluations
                    join p in context.Payments on ae.Dnr equals p.Dnr
                    join pa in context.Participants on ae.Dnr equals pa.Dnr
                    join pr in context.Programs on ae.Dnr equals pr.Dnr
                    where ae.Period.CompareTo(fromPeriod) >= 0 && ae.Period.CompareTo(toPeriod) <= 0
                                                               && pr.ProgramName == "Atlas partnerskap"
                    select new AtlasPartnerskapData
                    {
                        Dnr = ae.Dnr,
                        Period = ae.Period,
                        ApplicationStatus = ae.ApplicationStatus,
                        Total_Granted_Amount = p.Total_Granted_Amount,
                        Total_Approved_Amount = p.Total_Approved_Amount,
                        Applied_Student_Number = pa.Applied_Student_Number,
                        Approved_Student_Number = pa.Approved_Student_Number,
                        Granted_Participant_Number = pa.Granted_Participant_Number,
                        Reported_Participant_Number = pa.Reported_Participant_Number,
                        Reported_Women_Student_Number = pa.Reported_Women_Student_Number,
                        Reported_Men_Student_Number = pa.Reported_Men_Student_Number,
                        Reported_Women_Teacher_Number = pa.Reported_Women_Teacher_Number,
                        Reported_Men_Teacher_Number = pa.Reported_Men_Teacher_Number,
                        Reported_Women_SchoolLeader_Number = pa.Reported_Women_SchoolLeader_Number,
                        Reported_Men_SchoolLeader_Number = pa.Reported_Men_SchoolLeader_Number,
                        Reported_Women_AssociatedStaff_Number = pa.Reported_Women_AssociatedStaff_Number,
                        Reported_Men_AssociatedStaff_Number = pa.Reported_Men_AssociatedStaff_Number
                    };

        return query;
    }

    #endregion

    #region Atlas Praktik

    public IQueryable<AtlasPraktikData> GetAtlasPraktikDnr(string dnr)
    {
        var query = from ae in context.ApplicationAndEvaluations
                    join p in context.Payments on ae.Dnr equals p.Dnr
                    join pa in context.Participants on ae.Dnr equals pa.Dnr
                    where ae.Dnr == dnr && p.Dnr == dnr && pa.Dnr == dnr
                    select new AtlasPraktikData
                    {
                        Dnr = ae.Dnr,
                        Period = ae.Period,
                        ApplicationStatus = ae.ApplicationStatus,
                        Total_Granted_Amount = p.Total_Granted_Amount,
                        Total_Approved_Amount = p.Total_Approved_Amount,
                        Granted_Student_Number = pa.Granted_Student_Number,
                        Approved_Student_Number = pa.Approved_Student_Number
                    };

        return query;
    }


    public IQueryable<AtlasPraktikData> GetPeriodAtlasPraktik(string fromPeriod, string toPeriod)
    {
        var query = from ae in context.ApplicationAndEvaluations
                    join p in context.Payments on ae.Dnr equals p.Dnr
                    join pa in context.Participants on ae.Dnr equals pa.Dnr
                    join pr in context.Programs on ae.Dnr equals pr.Dnr
                    where ae.Period.CompareTo(fromPeriod) >= 0 && ae.Period.CompareTo(toPeriod) <= 0
                    && pr.ProgramName == "Atlas Praktik"
                    select new AtlasPraktikData
                    {
                        Dnr = ae.Dnr,
                        Period = ae.Period,
                        ApplicationStatus = ae.ApplicationStatus,
                        Total_Granted_Amount = p.Total_Granted_Amount,
                        Total_Approved_Amount = p.Total_Approved_Amount,
                        Granted_Student_Number = pa.Granted_Student_Number,
                        Approved_Student_Number = pa.Approved_Student_Number
                    };

        return query;
    }


    #endregion


    //Ej färdiga:

    #region MobilitetsstatistikMFSStipendier

    //WORK IN PROGRESS! (Funkar för all data inlagd fr.o.m 2019)
    public IQueryable<MobilitetsstatistikMFSStipendierData> GetMobilitetsstatistikMFSStipendierDnr(string dnr)
    {
        var query = from ae in context.ApplicationAndEvaluations
                    join r in context.ReportAndReclaims on ae.Dnr equals r.Dnr
                    join s in context.ScholarshipAndGrants on r.Dnr equals s.Dnr
                    join p in context.Participants on s.Dnr equals p.Dnr
                    join pr in context.Programs on ae.Dnr equals pr.Dnr
                    where ae.Dnr == dnr
                    group new { ae, r, s, p } by new { p.BirthData, p.FirstName } into g
                    select new MobilitetsstatistikMFSStipendierData
                    {
                        Dnr = dnr,
                        Period = g.First().ae.Period,
                        Report_Status = g.First().r.Report_Status,
                        NumberOfGrantedScholarships = g.First().s.NumberOfGrantedScholarships,
                        Gender = g.First().p.Gender
                    };

        return query;
    }


    public IQueryable<MobilitetsstatistikMFSStipendierData> GetPeriodMobilitetsstatistikMFSStipendier(string fromPeriod, string toPeriod)
    {
        var query = from ae in context.ApplicationAndEvaluations
                    join r in context.ReportAndReclaims on ae.Dnr equals r.Dnr
                    join s in context.ScholarshipAndGrants on r.Dnr equals s.Dnr
                    join p in context.Participants on s.Dnr equals p.Dnr
                    join pr in context.Programs on ae.Dnr equals pr.Dnr
                    where ae.Period.CompareTo(fromPeriod) >= 0 && ae.Period.CompareTo(toPeriod) <= 0
                                                               && pr.ProgramName == "Minor Field Studies Stipendier"
                    select new MobilitetsstatistikMFSStipendierData
                    {
                        Dnr = ae.Dnr,
                        Period = ae.Period,
                        Report_Status = r.Report_Status,
                        NumberOfGrantedScholarships = s.NumberOfGrantedScholarships,
                        Gender = p.Gender
                    };
        return query;
    }


    #endregion

    #region MFSStipendier


    public IQueryable<MFSStipendierData> GetMFSStipendierDnr(string dnr)
    {
        var query = from ae in context.ApplicationAndEvaluations
                    join p in context.Payments on ae.Dnr equals p.Dnr
                    where ae.Dnr == dnr && p.Dnr == dnr

                    select new MFSStipendierData
                    {
                        Dnr = ae.Dnr,
                        Period = ae.Period,
                        ApplicationStatus = ae.ApplicationStatus,
                        Total_Granted_Amount = p.Total_Granted_Amount,
                        Total_Approved_Amount = p.Total_Approved_Amount
                    };

        return query;
    }

    #endregion

    #endregion


    #region Hjälpmetoder
    public void GenerateNewDictionaries()
    {
        DictionarySetup dictionary = new();
    }


    #endregion
}