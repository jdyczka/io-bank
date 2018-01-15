using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOMail.Interfaces;

namespace IOMail
{
    public class Parser : ITemplateParser
    {
        public Parser()
        {

        }

        public string Parse(string template, dynamic data)
        {
            string result = "";
            //string template = GetTemplateByName(templatename);

            foreach (var p in data.GetType().GetProperties())
            {
                template = template.Replace("@" + p.Name, p.GetValue(data));
            }
            result = template;

            return result;
        }

        /*private string GetTemplateByName(string templatename)
        {
            // TODO: Add DB implementation
            string template = "Hello, @Name @Surname. Welcome to the IOBank";
            return template;
        }*/
    }
}
