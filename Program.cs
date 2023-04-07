using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using System.Net.Http;

namespace AsyncServerBotTelegram
{
    internal static class Program
    {
        public static Thread ThMain = new Thread(ThreadMainBot);
        private static MonitoringForm Monit;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Monit = new MonitoringForm();
            ThMain.Start();
            Application.Run(Monit);
            
        }

        static void ThreadMainBot(object prm)
        {
            TelegramBotClient Bot = new TelegramBotClient("TOKEN BOT");
            var cts = new CancellationTokenSource();
            var CancelToken = cts.Token;
            //non use Receiver options
            Bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, null, CancelToken);
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            string phrase = "Hello World!";
            Monit.textBox1.Invoke(new Action(() =>
            {
                if (!string.IsNullOrWhiteSpace(Monit.textBox1.Text))
                    phrase = Monit.textBox1.Text;
            }));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                Monit.AddUser(update.Message.From.Username, update.Message.From.Id.ToString(), update.Message.Date.ToLongTimeString());
                    
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Bot Was Started");
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, phrase);
            }
            
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
    }
}
