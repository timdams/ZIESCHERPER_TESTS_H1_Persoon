using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOPClassBasicsTesterLibrary;
using System;

namespace Persoon.Tests
{
    [TestClass]
    public class UnitTest1
    {
        TimsEpicClassAnalyzer tester = new TimsEpicClassAnalyzer(new Persoon());

        [TestMethod]
        public void BasisProps()
        {
            tester.CheckAutoProperty("Achternaam", typeof(string));
            tester.CheckAutoProperty("Voornaam", typeof(string));
            tester.CheckFullProperty("GeboorteDatum", typeof(DateTime));
        }

        [TestMethod]
        public void GeboorteDatumPropTest()
        {

            if (tester.CheckFullProperty("GeboorteDatum", typeof(DateTime)))
            {
                var dateTest = new DateTime(2000, 10, 8);
                tester.SetProp("GeboorteDatum", dateTest);
                Assert.AreEqual(dateTest.Date, ((DateTime)tester.GetProp("GeboorteDatum")).Date, $"Datum {dateTest.Date} ingevoed maar werd niet aanvaard");

                var dateTest2 = new DateTime(1970, 10, 8);
                tester.SetProp("GeboorteDatum", dateTest2);
                Assert.AreNotEqual(dateTest2.Date, ((DateTime)tester.GetProp("GeboorteDatum")).Date, $"Te oude datum {dateTest2.Date} ingevoerd maar werd toch aanvaard");
                Assert.AreEqual(DateTime.Today.Date, ((DateTime)tester.GetProp("GeboorteDatum")).Date, $"Te oude datum {dateTest2.Date} ingevoerd maar maar de datum werd vervolgens niet naar die van vandaag ingesteld");
            }
        }
        [TestMethod]
        public void BerekenLeeftijdTest()
        {
            if(tester.CheckMethod("BerekenLeeftijd", typeof(int),null))
            {
                for (int i = 1; i < 12; i++)
                {
                    var date = new DateTime(2000, i, 5);
                    tester.SetProp("GeboorteDatum", date);

                    int leeftijd = DateTime.Now.Year - date.Year;

                    if (DateTime.Now.Month < date.Month || (DateTime.Now.Month == DateTime.Now.Month && DateTime.Now.Day < date.Day))
                        leeftijd--;

                    tester.TestMethod("BerekenLeeftijd", null, leeftijd, $"Gebruikte geboorte datum {date.Date}.");
                }

            }
        }
    }
}
