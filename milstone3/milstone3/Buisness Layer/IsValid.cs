using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace milstone3
{
    class IsValid
    {
        static log4net.ILog log = ILog.getlogger();

        public static Boolean isValidUser(String username, String pass)
        {
            if (!isValidEmail(username))
            {
                Console.WriteLine("username not valid: " + username);
                log.Error("username not valid: " + username);
                return false;
            }
            else
            {
                if (!isValidPass(pass))
                {
                    Console.WriteLine("password not valid: " + pass);
                    log.Error("password not valid: " + pass);
                    return false;
                }
                else
                    return true;
            }



        }

        public static Boolean isValidEmail(string email)
        {
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                try
                {
                    // Normalize the domain
                    email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                          RegexOptions.None, TimeSpan.FromMilliseconds(200));

                    // Examines the domain part of the email and normalizes it.
                    string DomainMapper(Match match)
                    {
                        // Use IdnMapping class to convert Unicode domain names.
                        var idn = new IdnMapping();

                        // Pull out and process domain name (throws ArgumentException on invalid)
                        var domainName = idn.GetAscii(match.Groups[2].Value);

                        return match.Groups[1].Value + domainName;
                    }
                }
                catch (RegexMatchTimeoutException e)
                {
                    return false;
                }
                catch (ArgumentException e)
                {
                    return false;
                }

                try
                {
                    return Regex.IsMatch(email,
                        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                        RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }
                
            }
            /* try
             {
                 var addr = new System.Net.Mail.MailAddress(email);
                 return addr.Address == email;
             }
             catch
             {
                 return false;
             }*/
        }

        public static Boolean isValidPass(string pass)
        {
            if (pass == null || pass.Length < 4 | pass.Length > 20)
            {
                return false;
            }
            int numLet = 0;
            int smallLet = 0;
            int capLet = 0;
            for (int i = 0; i < pass.Length; i++)
            {
                if (pass[i] >= 'A' & pass[i] <= 'Z')
                {
                    capLet++;
                }
                else
                {
                    if (pass[i] >= 'a' & pass[i] <= 'z')
                    {
                        smallLet++;
                    }
                    else
                    {
                        if (pass[i] >= '0' & pass[i] <= '9')
                        {
                            numLet++;
                        }
                    }
                }
            }
            if (numLet > 0 & smallLet > 0 & capLet > 0) return true;
            return false;
        }

        public static Boolean isValidTask(String title, String description)
        {
            if (!isValidTitle(title))
            {
                Console.WriteLine("title not valid");
                log.Error("title not valid");
                return false;
            }
            else
            {
                if (!isValidDescription(description))
                {
                    Console.WriteLine("description not valid");
                    log.Error("description not valid");
                    return false;
                }
            }
            return true;
        }

        public static Boolean isValidTitle(String title)
        {
            if (title.Length == null || title.Length <= 0 | title.Length > 50) return false;
            return true;

        }
        public static Boolean isValidDescription(String description)
        {
            if (description.Length == null || description.Length <= 0 | description.Length > 300) return false;
            return true;
        }

    }


}

    
