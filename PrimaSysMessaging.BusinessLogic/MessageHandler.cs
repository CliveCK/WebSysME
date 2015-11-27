using System;
using System.Data;
using System.Configuration;
using PrimaSysMessaging.BusinessLogic;

namespace PrimaSysMessaging.BusinessLogic
{
    public class MessageHandler
    {
        MessageDb messageDb = null;

        public MessageHandler()
        {
            messageDb = new MessageDb();
        }

        public int GetMessageCount(int userID, int type)
        {
            return messageDb.GetMessageCount(userID, type);
        }

        public DataTable GetAllMessages(string userID)
        {
            return messageDb.GetAllMessages(userID);
        }

        public DataTable GetDeletedMessages(string userID)
        {
            return messageDb.GetDeletedMessages(userID);
        }

        public bool SendMessage(string userid, string sender, string subject, string body)
        {
            return messageDb.SendMessage(userid, sender, subject, body);
        }

        public Message GetMessageDetails(string readerId, int messageId, bool markasread)
        {
            DataTable table = messageDb.GetMessageDetails(readerId, messageId);

            if (table.Rows.Count == 0)
            {
                return null;
            }

            Message msg = new Message();

            msg.Date = Convert.ToDateTime(table.Rows[0]["datentime"].ToString());
            msg.MessageId = Convert.ToInt32(table.Rows[0]["MessageID"].ToString());
            msg.RecieverId = table.Rows[0]["recieverID"].ToString();
            msg.Status = table.Rows[0]["status"].ToString();
            msg.SenderId = table.Rows[0]["senderID"].ToString();
            msg.Subject = table.Rows[0]["subject"].ToString();
            msg.Body = table.Rows[0]["body"].ToString();
            msg.Receiver = table.Rows[0]["receiver"].ToString();
            msg.Sender = table.Rows[0]["sender"].ToString();

            //Before returning lets mark this message as read
            if (markasread == true) { 
                   messageDb.MarkMessageRead(messageId);
            }

            return msg;
        }

        public DataTable GetSentMessages(string userID)
        {
            return messageDb.GetSentMessages(userID);
        }
    }
}