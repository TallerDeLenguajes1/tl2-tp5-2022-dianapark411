using NLog;
public static class Funciones{
    

        static public int ingresar_positivo()
        {
            NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
            int num, flag = 0;
            do
            {
                num = Convert.ToInt32(Console.ReadLine());
                if(num>=0){
                    flag = 1;
                }
            } while (flag == 0);
            return num;
        }

        static public string ingresar_string(){

            NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

            string str = "";
            int flag = 0;

            do{
                try{
                    str = Console.ReadLine();
                    if(str == "" || str == null){
                        throw new ArgumentNullException(nameof(str));
                    }else{
                        flag = 1;
                    }
                }
                catch(Exception ex){
                    Logger.Error(ex);
                    Console.WriteLine("Error message: " + ex.Message);
                }
            }while(flag == 0);
            
            return str;
        }
    }