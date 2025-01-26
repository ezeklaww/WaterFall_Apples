using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WaterFall_AppleToApple
{
    public class Card
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        [BsonElement("title")]
        public string Title { get; private set; }

        [BsonElement("description")]
        public string Description { get; private set; }

        [BsonElement("greenApple")]
        public bool GreenApple { get; private set; }


        //MongoDB example card json
        /*
        {
          "_id": {
            "$oid": "67957b1410b6d9a3c46cb83b"                       -- the unique id used above
          },
          "title": "Abundant",
          "description": "(plentiful, ample, numerous) ",
          "greenApple": true
        }
        */

    }
}
