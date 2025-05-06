using System.Net.Mail;
using System.Net;
using WebApplication1.Models;
using WebApplication1.Dto.Classes;

namespace WebApplication1.BL
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SendNewEmailAsync(UpdateMatriculationDataRequest matriculationDataRequest, TMatriculationDto matriculationDto,string region)
        {
            try
            {
                string? smtpServer = _configuration["Smtp:Server"];
                int port = int.Parse(_configuration["Smtp:Port"] ?? "25");
                string? senderEmail = _configuration["Smtp:FromAddress"];
                string? userName = _configuration["Smtp:UserName"];
                string? password = _configuration["Smtp:Password"];
                string? recipientEmail = "dassydayan@gmail.com";//matriculationDataRequest.CoordinatorEmail

                using MailMessage message = new MailMessage();
                message.From = new MailAddress(senderEmail ?? "noreply@yourcompany.com");
                message.To.Add(recipientEmail);
                message.Subject = $"נרשמה בגרות חדשה: {matriculationDto.NvMatriculationName}";

                var teachersList = string.Join("<br/>", matriculationDataRequest.AccompanyingTeachers.Select(t => $"- {t}"));

                var body = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif; direction: rtl; text-align: right;'>
                    <h2 style='color: #2e6c80;'>נרשמת בהצלחה לבחינת הבגרות</h2>
                    <p>להלן פרטי הרישום:</p>
                    <ul>
                    <li><strong>מספר נבחנים בוקר:</strong> {matriculationDataRequest.MorningTesters}</li>
                    <li><strong>מספר נבחנים צהריים:</strong> {matriculationDataRequest.EveningTesters}</li>
                    <li><strong>הנתונים יגיעו אליכם ל:</strong> {region}</li>
                    <li><strong>פרטי הרכז:</strong><br/>
                        שם: {matriculationDataRequest.CoordinatorName}<br/>
                        מייל: {matriculationDataRequest.CoordinatorEmail}<br/>
                        טלפון: {matriculationDataRequest.CoordinatorPhone}
                    </li>
                    <li><strong>מספר חדרי מעבדה:</strong> {matriculationDataRequest.LaboratoryRooms}</li>
                    <li><strong>שמות מורים מלווים:</strong><br/>{teachersList}</li>
                    </ul>
                    <hr/>
                    <p style='color: red;'><strong>לתשומת ליבכם:</strong></p>
                    <p>
                    בחינת הבגרות בתאריך <strong>{matriculationDto.DtMatriculationDate:dd/MM/yyyy}</strong><br/>
                    ניתן להירשם עד תאריך <strong>{matriculationDto.DtStudentsLastUpdateDate:dd/MM/yyyy}</strong><br/>
                    בכל בעיה יש לפנות בווצאפ בלבד במספר: <strong>054-600-3413</strong>
                </p>
                </body>
                </html>";

                message.Body = body;
                message.IsBodyHtml = true;

                using var client = new SmtpClient(smtpServer, port);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(senderEmail, password);

                await client.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}