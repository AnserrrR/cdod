using cdod.Services;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Forms.v1;
using cdods.s;

namespace cdod.Schema.Mutations
{
    public class Mutation
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets, FormsService.Scope.FormsResponsesReadonly };
        static readonly string ApplicationNameSH = "CdodRecordsForCourseTable";
        static readonly string ApplicationNameForm = "CdodRecordsForCourseForm";
        static readonly string SpreadsheeetId = "1RhS6WlvtPcL3mSK1O7p7NITxrY48iNSWH5WcxDklhyw";
        static readonly string FormId = "1GgoxS3vSjQ9pILVhT0Vsy2rEh5AaiqYtBgqY-wvWEUw";

        static readonly string sheet = "records";
        static SheetsService service;
        static FormsService formsService;

        [UseDbContext(typeof(CdodDbContext))]
        private async void CreateTrialUsersFromGoogleTable()
        {
            GoogleCredential credential;
            using (var stream = new FileStream("./google_api_sec.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(Scopes);
            }
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationNameSH
            });
            /*            formsService = new FormsService(new Google.Apis.Services.BaseClientService.Initializer()
                        {
                            HttpClientInitializer = credential,
                            ApplicationName = ApplicationNameForm
                        });*/
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> UpdateFromGoogleTable([ScopedService] CdodDbContext dbContext)
        {
            CreateTrialUsersFromGoogleTable();
            var range = $"{sheet}!A6:J";
            var request = service.Spreadsheets.Values.Get(SpreadsheeetId, range);
            var response = request.Execute();
            var values = response.Values;
            if (values is not null && values.Count > 0)
            {
                foreach(var value in values)
                {
                    // 1 - 6;
                    var dateCreateRecord = value[0].ToString().Split(' ');
                    var email = value[1].ToString();
                    var FIO = value[2].ToString().Split(' ');
                    if (FIO.Count() < 2) FIO.Append("Фамилия");
                    var school = value[3].ToString();
                    var dateBirthStudent = value[5].ToString();
                    var FIOParent = value[6].ToString().Split(' ');
                    if (FIO.Count() < 2) FIO.Append("Фамилия");
                    var coursesArr = value[7].ToString().Split(',');
                    List<string> courses = new List<string>();
                    foreach (var course in coursesArr)
                    {
                        string str = course;
                        if (course[0] == ' ') str = course.Trim(' ');
                        courses.Add(str);
                    }

                    var phone = value[9].ToString();
                    User user = new User()
                    {
                        Email = email,
                        Firstname = FIOParent[1],
                        Lastname = FIOParent[0],
                        Patronymic = FIOParent.Count() == 2 ? null : FIOParent[2],
                        PhoneNumber = phone,
                        Password = "guest"
                    };
                    User? bufUs = dbContext.Users.FirstOrDefault(u => u.Email == email);
                    if (bufUs is null)
                    {

                        await dbContext.Users.AddAsync(user);
                        await dbContext.SaveChangesAsync();

                    }

                    Parent parent = new Parent()
                    {
                        UserId = user.Id
                    };
                    var bufPar = dbContext.Parents.FirstOrDefault(p => p.UserId == (bufUs == null ? user.Id : bufUs.Id));
                    if (bufPar is null)
                    {
                        await dbContext.Parents.AddAsync(parent);
                        await dbContext.SaveChangesAsync();
                    }

                    var schoolBuf = dbContext.Schools.FirstOrDefault(s => s.Name == school);
                    School schoolAdd = new School()
                    {
                        Name = school,
                        District = District.Central
                    };
                    if (schoolBuf is null)
                    {
                        await dbContext.Schools.AddAsync(schoolAdd);
                        await dbContext.SaveChangesAsync();
                    }
                    var studentBuf = dbContext.Students.FirstOrDefault(s => ((s.ParentId == (bufUs == null ? user.Id : bufUs.Id))
                                                                            && (s.LastName == FIO[0])
                                                                            && (s.FirstName == FIO[1])
                                                                            && (s.Patronymic == (FIO.Count() == 2 ? null : FIO[2]))
                                                                            && (s.BirthDate == DateOnly.Parse(dateBirthStudent))));
                    Student student = new Student()
                    {
                        FirstName = FIO[1],
                        LastName = FIO[0],
                        Patronymic = FIO.Count() == 2 ? null : FIO[2],
                        BirthDate = DateOnly.Parse(dateBirthStudent),
                        SchoolId = (schoolBuf == null ? schoolAdd.Id : schoolBuf.Id),
                        ParentId = (bufPar == null ? parent.UserId : bufPar.UserId)
                    };
                    if (studentBuf is null)
                    {
                        await dbContext.Students.AddAsync(student);
                        await dbContext.SaveChangesAsync();
                    }

                    foreach (var course in courses)
                    {
                        if (course is not null)
                        {
                            int idCourse = dbContext.Courses.FirstOrDefault(c => c.Name == course).Id;
                            var studentTOCourseBuf = dbContext.StudentToCourses.FirstOrDefault(stc => ((stc.StudentId == (studentBuf == null ? student.Id : studentBuf.Id)) && (stc.CourseId == idCourse)));
                            var studentToCourse = new StudentToCourse()
                            {
                                CourseId = idCourse,
                                StudentId = (studentBuf == null ? student.Id : studentBuf.Id),
                                ContractStateId = 1,
                                SignDate = DateOnly.Parse(dateCreateRecord[0]),
                                ContractUrl = null,
                                Debt = 0.0,
                                EquipmentPriceWithRobot = false
                            };
                            if (studentTOCourseBuf is null)
                            {
                                await dbContext.StudentToCourses.AddAsync(studentToCourse);
                                await dbContext.SaveChangesAsync();
                            }
                        }

                    }
                }
                

                return true;
            }
            else
            {
                Console.WriteLine("No data found");

                return false;
            }
        }

/*        [UseDbContext(typeof(CdodDbContext))]
        public async Task<string> UpdateFromGoogleForm([ScopedService] CdodDbContext dbContext)
        {
            CreateTrialUsersFromGoogleTable();
            string l = " ";
            var req = formsService.Forms.Get(FormId);
            var res = req.Execute();
            Console.WriteLine(res);
            return l;
        }*/
    }
}
