using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityBot.Operations
{
    public class TextAction:IAction
    {
        public string GetText(string opt, string text)
        {
           string rez = "";
            try
            {
                
                switch (opt)
                {
                    case "count":
                        rez = "Количество символов в строке: " + text.Trim().Length.ToString();
                        break;
                    case "summ":
                        bool containsCheck = text.Trim().Contains(" ");
                        if (!containsCheck) throw new MyException("В сообщении нет разделителя!");
                        else
                        {

                            string[] strsumm = text.Split(" ");
                            if (strsumm.Length != 2) throw new MyException("Количество элементов не равно 2!");
                            else
                            {
                                bool parsecheck0 = int.TryParse(strsumm[0], out int a);
                                bool parsecheck1 = int.TryParse(strsumm[1], out int b);
                                if (parsecheck0 & parsecheck1) rez = (a + b).ToString();
                                else throw new MyException("Сообщение построено неправильно!");
                            }
                        }
                        break;
                }
                
            }
            catch (Exception ex)
            {
                rez = ex.Message;
            }
            return rez;
        }
    }
}
