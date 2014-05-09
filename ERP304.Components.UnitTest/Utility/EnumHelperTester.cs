using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP304.Components.Enums;
using NUnit.Framework;

using ERP304.Components.Utility;

namespace ERP304.Components.UnitTest.Utility {
    [TestFixture]
    public class EnumHelperTester {
        [Test]
        public void Run() {

            List<string> enumNames = EnumHelper<MapControlType>.GetNames();
            foreach (var enumName in enumNames) {
                System.Console.WriteLine(enumName);
            }


            MapControlType[] enumValues = EnumHelper<MapControlType>.GetValues();
            foreach (var enumValue in enumValues) {
                System.Console.WriteLine(enumValue);
            }
        }
    }
}
