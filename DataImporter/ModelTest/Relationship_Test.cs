using System;
using System.Collections.Generic;
using System.IO;
using Model;
using NUnit.Framework;
using Serializer;

namespace ModelTest
{
    [TestFixture]
    public class Relationship_Test
    {
        [Test]
        public void Serialize()
        {
            Console.WriteLine(XmlSerialize.Serialize(
                new Relationships()
                {
                    new Relationship()
                    {
                        ForiegnKeyColumn = "ForiegnKey",
                        ForiegnKeyTable = "ForiegnKeyTable",
                        Name = "ForiegnKeyPrimaryKeyName",
                        PrimaryKeyColumn = "PrimaryKeyCol",
                        PrimaryKeyTable = "PrimaryKeyTable"
                    },
                    new Relationship()
                    {
                        ForiegnKeyColumn = "ForiegnKey1",
                        ForiegnKeyTable = "ForiegnKeyTable1",
                        Name = "ForiegnKeyPrimaryKeyName1",
                        PrimaryKeyColumn = "PrimaryKeyCol1",
                        PrimaryKeyTable = "PrimaryKeyTable1"
                    }
                }));
        }

        [Test]
        public void Deserialize()
        {
            Console.WriteLine(XmlSerialize.Deserialize<Relationships>(File.ReadAllText(@"RelationShip\DBRelationShips.xml")).Count);
        }
    }
}
