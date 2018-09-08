using System;
using System.Collections.Generic;
using System.Text;

using PMDCP.Core;
using PMDCP.DatabaseConnector;
using PMDCP.DatabaseConnector.MySql;


namespace DataManager.Parties {
    public class PartyDataManager {

        public static List<string> LoadPartyIDList(PMDCP.DatabaseConnector.MySql.MySql database) {
            List<string> idList = new List<string>();

            string query = "SELECT parties.PartyID " +
                "FROM parties;";
            List<DataColumnCollection> rows = database.RetrieveRows(query);
            if (rows != null) {
                for (int i = 0; i < rows.Count; i++) {
                    if (!idList.Contains(rows[i]["PartyID"].ValueString)) {
                        idList.Add(rows[i]["PartyID"].ValueString);
                    }
                }
            }

            return idList;
        }

        public static PartyData LoadParty(PMDCP.DatabaseConnector.MySql.MySql database, string partyID) {
            PartyData partyData = new PartyData();
            partyData.PartyID = partyID;
            string query = "SELECT parties.CharID " +
                "FROM parties " +
                "WHERE parties.PartyID = \'" + partyData.PartyID + "\';";
            List<DataColumnCollection> rows = database.RetrieveRows(query);
            if (rows != null) {
                for (int i = 0; i < rows.Count; i++) {
                    partyData.Members.Add(rows[i]["CharID"].ValueString);
                }
            }
            return partyData;
        }

        public static PartyData LoadCharacterParty(PMDCP.DatabaseConnector.MySql.MySql database, string charID) {
            PartyData partyData = new PartyData();

            string query = "SELECT parties.PartyID " +
                "FROM parties " +
                "WHERE parties.CharID = \'" + charID + "\';";
            List<DataColumnCollection> rows = database.RetrieveRows(query);
            if (rows != null) {
                for (int i = 0; i < rows.Count; i++) {
                    partyData.PartyID = rows[i]["PartyID"].ValueString;
                }
            } else {
                return null;
            }

            query = "SELECT parties.CharID " +
                "FROM parties " +
                "WHERE parties.PartyID = \'" + partyData.PartyID + "\';";
            rows = database.RetrieveRows(query);
            if (rows != null) {
                for (int i = 0; i < rows.Count; i++) {
                    partyData.Members.Add(rows[i]["CharID"].ValueString);
                }
            }
            return partyData;
        }

        public static void LoadParty(PMDCP.DatabaseConnector.MySql.MySql database, PartyData partyData) {
            string query = "SELECT parties.CharID " +
                "FROM parties " +
                "WHERE parties.PartyID = \'" + partyData.PartyID + "\';";
            List<DataColumnCollection> rows = database.RetrieveRows(query);
            if (rows != null) {
                for (int i = 0; i < rows.Count; i++) {
                    partyData.Members.Add(rows[i]["CharID"].ValueString);
                }
            }

        }
        
        public static void SaveParty(PMDCP.DatabaseConnector.MySql.MySql database, PartyData partyData) {
            database.ExecuteNonQuery("DELETE FROM parties WHERE PartyID = \'" + partyData.PartyID + "\'");
            //database.DeleteRow("friends", "CharID = \'" + playerData.CharID + "\'");

            for (int i = 0; i < partyData.Members.Count; i++) {
                database.UpdateOrInsert("parties", new IDataColumn[] {
                    database.CreateColumn(false, "PartyID", partyData.PartyID),
                    database.CreateColumn(false, "PartySlot", i.ToString()),
                    database.CreateColumn(false, "CharID", partyData.Members[i])
                });
            }

        }

        public static void DeleteParty(PMDCP.DatabaseConnector.MySql.MySql database, PartyData partyData) {
            database.ExecuteNonQuery("DELETE FROM parties WHERE PartyID = \'" + partyData.PartyID + "\'");
        }
    }
}
