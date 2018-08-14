using System;
using CodeHelp.Common.SqlHelp;
using Xunit;

namespace CodeHelp.QueryService.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var model = new { FirstName = "Bar", City = "New York" };

            var builder = new SqlBuilder();
            var selector = builder.AddTemplate(
                @"SELECT ST.name TableName,SEG.value Description
                FROM sys.tables ST
                LEFT JOIN sys.extended_properties SEG ON ST.object_id = SEG.major_id AND SEG.minor_id = 0 "
            );

            if (!string.IsNullOrEmpty(model.FirstName))
            {
                builder.Where("FirstName = @FirstName", new { model.FirstName });
            }

            if (!string.IsNullOrEmpty(model.City))
            {
                builder.Where("City = @City", new { model.City });
            }

            var a = selector.RawSql;
            var b = selector.Parameters;

            Assert.True(selector.RawSql
                        == "select * from table WHERE FirstName = @FirstName AND City = @City");
        }
    }
}
