namespace NPocoTestDrive.ConsoleApp.Views.UserControl
{
    public static class ConsoleMessage
    {
        public enum TypeMessage
        {
            ALERT
            , INFORMATION
            , DANGER
            , ERROR
            , NOK
            , OK
            , SUCCESS
            , WARNING
        }

        public static void Show(string message, TypeMessage typeMessage, bool breakLine = true, bool stop = true)
        {
            switch (typeMessage)
            {
                case TypeMessage.INFORMATION:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;

                case TypeMessage.WARNING:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case TypeMessage.ALERT:
                case TypeMessage.DANGER:
                case TypeMessage.ERROR:
                case TypeMessage.NOK:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case TypeMessage.OK:
                case TypeMessage.SUCCESS:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
            }

            if (breakLine)
                Console.WriteLine(message);
            else
                Console.Write(message);

            Console.ForegroundColor = ConsoleColor.Gray;

            if (stop)
                Console.ReadKey();
        }
    }
}
