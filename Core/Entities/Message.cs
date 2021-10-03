using System;
using System.Text.RegularExpressions;

namespace Core.Entities
{
    public class Message : BaseEntity
    {
        public string FirstName {get; protected set;}
        public string LastName {get; protected set;}
        public string EmailTo {get; protected set;}
        public string EmailCc {get; set;}
        public Topic Topic {get; protected set;}
        public int TopicId {get; protected set;}
        public string MessageText {get; protected set;}
        public DateTime SendDateTime { get; set;}

        private static readonly Regex EmailRegex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

        protected Message()
        {
        }

        protected Message(string firstName, string lastName, string emailTo, string emailCc, int topicId, string messageText)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            SetEmailTo(emailTo);
            EmailCc = emailCc;
            TopicId = topicId;
            MessageText = messageText;
        }

        public void SetFirstName(string firstName)
        {
            if (String.IsNullOrEmpty(firstName))
            {
                throw new Exception("FirstName is invalid.");
            }
            FirstName = firstName;
        }

        public void SetLastName(string lastName)
        {
            if (String.IsNullOrEmpty(lastName))
            {
                throw new Exception("LastName is invalid.");
            }
            LastName = lastName;
        }

        public void SetEmailTo(string email)
        {
            // if (!EmailRegex.IsMatch(email))
            // {
            //     throw new Exception("EmailTo is invalid.");
            // }

            if (String.IsNullOrEmpty(email))
            {
                throw new Exception("EmailTo is invalid.");
            }

            EmailTo = email.ToLowerInvariant();
        }


        public static Message Create(string firstName, string lastName, string emailTo, string emailCc, int topicId, string messageText)
            => new Message(firstName, lastName, emailTo, emailCc, topicId, messageText);

        public string ToString()
        {
            return $"{FirstName} {LastName} {EmailTo} { EmailCc} {TopicId} {MessageText} {SendDateTime}";
        }
    }
}