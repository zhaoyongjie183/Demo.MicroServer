using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/27 9:30:41                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Utils                              
*│　类    名： DateTimeOffsetJsonConverter                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Utils
{
    public class DateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
    {
        private TimeZoneInfo chinaZoneInfo = TimeZoneInfo.CreateCustomTimeZone("zh", TimeSpan.FromHours(8), "中国时区", "China time zone");
        private TimeZoneInfo indiaZoneInfo = TimeZoneInfo.CreateCustomTimeZone("en-IN", TimeSpan.FromHours(5), "印度时区", "India time zone");

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var currentZoneInfo = Thread.CurrentThread.CurrentCulture.Name.Contains("zh") ? chinaZoneInfo : indiaZoneInfo;
            var time1 = new DateTimeOffset(DateTime.Parse(reader.GetString()), currentZoneInfo.BaseUtcOffset);
            var time2 = time1.ToUniversalTime();

            return time2;
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            var currentZoneInfo = Thread.CurrentThread.CurrentCulture.Name.Contains("zh") ? chinaZoneInfo : indiaZoneInfo;
            writer.WriteStringValue(value.ToOffset(currentZoneInfo.BaseUtcOffset).ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
