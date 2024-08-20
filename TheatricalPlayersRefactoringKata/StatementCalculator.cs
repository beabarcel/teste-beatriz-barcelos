using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata;

public class StatementCalculator
{
    public Statement CalculateStatement(Invoice invoice, Dictionary<string, Play> plays)
    {
        var statementReturn = new Statement();

        var totalAmount = 0m;
        var volumeCredits = 0;
        statementReturn.Customer = invoice.Customer;

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            int itemCredits = 0;
            lines = SetTotalLines(lines);
            //A especificação ou o teste possui um erro onde indica dividir por 10 e não multiplicar por 10
            decimal thisAmount = SetAmountCalculation(lines);

            switch (play.Type)
            {
                case "tragedy":
                    thisAmount = CalculateTragedy(perf, thisAmount);
                    break;
                case "comedy":
                    thisAmount = CalculateComedy(perf, thisAmount);

                    break;
                case "history":
                    thisAmount = CalculateHistory(perf, thisAmount);
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            // add volume credits
            volumeCredits += SetVolumeCredits(volumeCredits, perf);

            if (play.Type == "comedy")
            {
                var comedyCredits = SetVolumeComedyCredits(volumeCredits, perf);
                volumeCredits += comedyCredits;
                itemCredits += SetVolumeCredits(itemCredits, perf) + comedyCredits;
            }
            else
            {
                itemCredits = SetVolumeCredits(itemCredits, perf);
            }


            statementReturn.Items.Add(new Item
            {
                PlayName = play.Name,
                AmountOwed = Convert.ToDecimal(thisAmount / 100),
                Seats = perf.Audience,
                EarnedCredits = itemCredits
            });

            totalAmount += thisAmount;
        }

        statementReturn.AmountOwed = Convert.ToDecimal(totalAmount / 100);
        statementReturn.EarnedCredits = volumeCredits;
        return statementReturn;
    }

    public string Print(Statement statementResult)
    {
        CultureInfo cultureInfo = new CultureInfo("en-US");
        var result = string.Format("Statement for {0}\n", statementResult.Customer);
        foreach (var item in statementResult.Items)
        {
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", item.PlayName, item.AmountOwed, item.Seats);
        }

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", statementResult.AmountOwed);
        result += String.Format("You earned {0} credits\n", statementResult.EarnedCredits);

        return result;
    }

    public string PrintXML(Statement statementResult)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Statement));

        var settings = new XmlWriterSettings
        {
            Encoding = new UTF8Encoding(true),
            Indent = true,
            OmitXmlDeclaration = false
        };

        string xmlString;
        using (var memoryStream = new MemoryStream())
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, settings))
            {
                serializer.Serialize(xmlWriter, statementResult);
            }

            xmlString = Encoding.UTF8.GetString(memoryStream.ToArray());
        }
        return xmlString;
    }

    private int SetVolumeComedyCredits(int volumeCredits, Performance perf)
    {
        volumeCredits = (int)Math.Floor((decimal)perf.Audience / 5);
        return volumeCredits;
    }

    private int SetVolumeCredits(int volumeCredits, Performance perf)
    {
        volumeCredits = Math.Max(perf.Audience - 30, 0);
        return volumeCredits;
    }

    private decimal SetAmountCalculation(int lines)
    {
        return lines * 10;
    }

    private int SetTotalLines(int lines)
    {
        if (lines < 1000)
            lines = 1000;
        if (lines > 4000)
            lines = 4000;

        return lines;
    }

    private decimal CalculateTragedy(Performance perf, decimal thisAmount)
    {
        if (perf.Audience > 30)
        {
            thisAmount += 1000 * (perf.Audience - 30);
        }

        return thisAmount;
    }

    private decimal CalculateComedy(Performance perf, decimal thisAmount)
    {
        if (perf.Audience > 20)
        {
            thisAmount += 10000 + 500 * (perf.Audience - 20);
        }
        thisAmount += 300 * perf.Audience;
        return thisAmount;
    }

    private decimal CalculateHistory(Performance perf, decimal thisAmount)
    {
        return CalculateComedy(perf, thisAmount) + CalculateTragedy(perf, thisAmount);
    }

}
