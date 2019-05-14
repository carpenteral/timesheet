using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TimeSheetFramework
{
    public  class TimeFormatter
    {
        readonly   static  private TimeFormatter self = new TimeFormatter();
        private String[]  tokens;
        private readonly    static  string format = "00";

        public TimeFormatter()
        {
        }

        static  public  TimeFormatter   getInstance()
        {
            return self;
        }
    
        public  String  _format( String input ) // throws BadlyFormedTime
        {
            if( input.Length < 4 )
                throw new BadlyFormedTime("Badly formed time");
        
            tokeniseWithSplit(input, @":|\s+");
        
            if( tokens[0].Equals("ERR"))
                throw new BadlyFormedTime("Badly formed time");
        
            //int hours = new Integer( tokens[0] );
            int hours = Int32.Parse(tokens[0]);
            int mins = Int32.Parse(tokens[1]);
        
            if( tokens.Length > 2 )
            {
                if(tokens[2].Equals("PM") )
                {
                    if( hours < 12 )
                    {
                        hours += 12;
                        tokens[0] = hours.ToString(format);
                    }
                }
                if( tokens[2].Equals("AM") )
                {
                    if( hours == 12 )
                    {
                        tokens[0] = "00";
                    }
                    else
                        tokens[0] = hours.ToString(format);
                }
            }
            else
            {
                tokens[0] = hours.ToString(format);
                tokens[1] = mins.ToString(format);
            
            }
            return tokens[0]+":"+tokens[1];
        }

        public String[] getTokens()
        {
            return tokens;
        }

        public String[] tokenise(String input, String delims)
        {
            Console.WriteLine("StringTokenizer Example: \n");
            input = input.ToUpper();
            String[] st = Regex.Split(input, delims);
            List<String> stringArray = new List<String>();
            foreach (String item in st) 
            {
                stringArray.Add(item);
                Console.WriteLine("StringTokenizer Output: " + item );
            }
            tokens = new String[stringArray.Count];
            int idx = 0;
            foreach (String str in stringArray)
            {
                tokens[idx] = stringArray.ElementAt(idx++);
            }
        
            return tokens;
        }

        public String[] tokeniseWithSplit(String input, String regex)
        {
            Console.WriteLine("StringTokenizer with Split Example: \n");
            input = makeCanonicalTimeFormat(input);

            Console.WriteLine("\n\nSplit Example: \n");
        
            tokens = Regex.Split(input, regex);

            if( tokens.Length > 2 && tokens[2].Equals("AM") )
            {
                int hours = Int32.Parse(tokens[0]);
                if(hours > 12)
                {
                    tokens[0] = "ERR";
                    tokens[1] = "ERR";
                    tokens[2] = "ERR";
                }
            }
            
            int tokenCount = tokens.Length;
            for (int j = 0; j < tokenCount; j++) 
            {
                    Console.WriteLine("Split Output: "+ tokens[j]);
            }
        
            return tokens;
        }

        public String makeCanonicalTimeFormat(String input)
        {
            input = input.ToUpper();
            String result = input;
            String timeOfDay = "";
            int idx = 0;

            if (input.Contains("AM"))
            {
                idx = input.IndexOf("AM");
                timeOfDay = "AM";
            }
            else if (input.Contains("PM"))
            {
                idx = input.IndexOf("PM");
                timeOfDay = "PM";
            }

            if (idx > 0 && input[idx - 1] != ' ')
            {
                result = input.Substring(0, idx);
                result += " " + timeOfDay;
            }
            return result;
        }
    }
}
