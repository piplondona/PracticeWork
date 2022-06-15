﻿using System;
using System.Text.RegularExpressions;

namespace MercadoLibre.Mutant
{
    class Program
    {
        
        static void Main(){

            Console.WriteLine("Please, type your dna, input next format: {\"valor1\",\"valor2\",..}:");
            string? input = Console.ReadLine();
            
            try
            {
                if (ValidateInputString(input))
                {
                    if (IsMutant(input.Split(',')))
                        ShowMessage("Information: Dna is positive for be a Mutant");
                    else
                        ShowMessage("Information: Dna is negative for be a Mutant");
                }
                else
                {
                    ShowMessage("Error: The input format is wrong");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(String.Format("Error: description: {0}", ex.ToString()));       
                throw;
            }
        }

        static bool ValidateInputString(string? inputString)
        {
            if (!String.IsNullOrEmpty(inputString))
            {
                string patternInput = @"^\{\""[ACGT]*\""([\,]?(\""[ACGT]*\""))*\}$/i";
                Match matchRegExpr = Regex.Match(inputString, patternInput);
                if (matchRegExpr.Success)
                {
                    return true;
                }
                else
                {
                    ShowMessage("Error: The input format is wrong");
                }
            }
            else
            {
                ShowMessage("Error: Input empty, could you input someone value");
            }

            return false;
        }

        static bool IsMutant(string[] dna)
        {
            return ((FindSequenceHorizontally(dna) + FindSequenceVertically(dna) + FindSequenceSide(dna)) > 1);
        }

        static int FindSequenceHorizontally(string[] dna)
        {
            return dna.Count(p => p.ToString().Contains(((Sequences)Sequences.AAAA).ToString()) || p.ToString().Contains(((Sequences)Sequences.TTTT).ToString()) 
            || p.ToString().Contains(((Sequences)Sequences.CCCC).ToString()) || p.ToString().Contains(((Sequences)Sequences.GGGG).ToString()));
        }

        static int FindSequenceVertically(string[] dna)
        {
            return 0;
        }

        static int FindSequenceSide(string[] dna)
        {
            return 0;
        }

        static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        enum NitrogenBase{
            C,
            A,
            T,
            G
        };

         enum Sequences{
            AAAA,
            CCCC,
            TTTT,
            GGGG
        };
    }
}
