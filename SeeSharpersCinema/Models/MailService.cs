﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;

namespace SeeSharpersCinema.Models
{
    public class EmailService
    {
        private const String cinemaEmail = "SeeSharpersCinema@gmail.com";
        /*public void email_send()
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.UseDefaultCredentials = false;

            mail.From = new MailAddress("wesleystudentavans@gmail.com");
            mail.To.Add("w.vanburik@student.avans.nl");
            mail.Subject = "Test Mail - 1";
            mail.Body = "mail with attachment";

            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment("C:/Users/WesleyB/Downloads/Microsoft.test-king.MB-500.v2020-06-02.by_.amelie.37q.pdf");
            mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("wesleystudentavans@gmail.com", "Wes4533b!");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }*/

        public void email_send()
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(cinemaEmail);
                //Mail TO customer email
                mail.To.Add("w.vanburik@student.avans.nl");
                //Add name and moviename from purchase information
                mail.Subject = "Thank you " + "(name)" + " for ordering a ticket for movie " + "(moviename)";
                mail.Body = "<h1>Hello</h1>";
                mail.IsBodyHtml = true;
                //Attach Generated PNG from QR code.
                mail.Attachments.Add(new Attachment("C:/Users/WesleyB/Downloads/MB-300_Braindumps_Collection.docx"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("SeeSharpersCinema@gmail.com", "SeeSharpersCinema1!");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}