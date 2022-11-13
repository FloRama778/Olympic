using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olympic.Model;

namespace Olympic.Controller
{
    class OlympicsController
    {
        public static string StringaDiConnessione
        {
            get { return ConfigurationManager.ConnectionStrings["Database"].ConnectionString; }
        }
        public static List<OlympicsModel> GetAll()
        {
            List<OlympicsModel> Lista_Ritorno = new List<OlympicsModel>();
            OlympicsModel O = new OlympicsModel();
            using (SqlConnection connection = new SqlConnection(StringaDiConnessione))

            {
                try
                {

                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT * FROM athletes";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            Lista_Ritorno.Add(ReadToOlympic(reader));


                        }

                    }

                    return Lista_Ritorno;
                }
                catch (Exception)
                {

                    throw;

                }

            }
        }
        public static List<string> GetSex()
        {
            List<string> Lista_Ritorno = new List<string>();
            using (SqlConnection connection = new SqlConnection(StringaDiConnessione))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "Select Distinct Sex from athletes";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(reader.GetOrdinal("Sex"))) continue;
                            Lista_Ritorno.Add((string)reader["Sex"]);

                        }
                        return Lista_Ritorno;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public static List<string> GetGames()
        {
            List<string> Lista_Ritorno = new List<string>();
            using (SqlConnection connection = new SqlConnection(StringaDiConnessione))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "Select Distinct Games from athletes";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(reader.GetOrdinal("Games"))) continue;
                            Lista_Ritorno.Add((string)reader["Games"]);

                        }
                        return Lista_Ritorno;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public static List<string> GetMedal()
        {
            List<string> Lista_Ritorno = new List<string>();
            using (SqlConnection connection = new SqlConnection(StringaDiConnessione))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "Select Distinct Medal from athletes";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(reader.GetOrdinal("Medal"))) continue;              
                            Lista_Ritorno.Add((string)reader["Medal"]);

                        }
                        return Lista_Ritorno;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public static List<string> GetSport(string Games)
        {
            if (Games != null)
            {

                List<string> Lista_Ritorno = new List<string>();
                using (SqlConnection connection = new SqlConnection(StringaDiConnessione))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = connection;
                        cmd.CommandText = "Select Distinct Sport from athletes Where Games = @Games";
                        cmd.Parameters.AddWithValue("@Games", $"{Games}");
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.IsDBNull(reader.GetOrdinal("Sport"))) continue;
                                Lista_Ritorno.Add((string)reader["Sport"]);
                            
                            }
                            return Lista_Ritorno;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }



            }
            return null;

        }
        public static List<string> GetEvent(string Games, string Sport)
        {
            if (Sport!= null)
            {

                List<string> Lista_Ritorno = new List<string>();
                using (SqlConnection connection = new SqlConnection(StringaDiConnessione))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = connection;
                        cmd.CommandText = "Select Distinct Event from athletes Where Games = @Games AND Sport= @Sport";
                        cmd.Parameters.AddWithValue("@Games", $"{Games}");
                        cmd.Parameters.AddWithValue("@Sport", $"{Sport}");
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.IsDBNull(reader.GetOrdinal("Event"))) continue;
                                Lista_Ritorno.Add((string)reader["Event"]);

                            }
                            return Lista_Ritorno;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }



            }
            return null;

        }
//
        public static List<OlympicsModel> Find(string Sex,string Medal, string Games, string Name, string Sport, string Event, int Pagina_Corrente, int Righe_Pag, ref int PaginaRif)
        {
            List<OlympicsModel> Lista_Ritorno = new List<OlympicsModel>();
            OlympicsModel O = new OlympicsModel();

            using (SqlConnection connection = new SqlConnection(StringaDiConnessione))

            {
                try
                {

                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText= "SELECT * FROM athletes";
                    if (Sex == null && Medal == null && Games == null && Name == null) GetAll();
                    else if (Sex != null && Medal == null && Games == null && Name == null) cmd.CommandText += " WHERE Sex = @Sex";
                    else if (Sex == null && Medal != null && Games == null && Name == null) cmd.CommandText += " WHERE Medal = @Medal";
                    else if (Sex == null && Medal == null && Games != null && Name == null)
                    {
                        if (Sport == null && Event == null) cmd.CommandText += " WHERE Games = @Games";
                        if (Sport != null && Event == null) cmd.CommandText += " WHERE Games = @Games AND Sport = @Sport";
                        if (Sport != null && Event != null) cmd.CommandText += " WHERE Games = @Games AND Sport = @Sport AND Event= @Event";
                    }
                    else if (Sex == null && Medal == null && Games == null && Name != null) cmd.CommandText += " WHERE Name like @Name";
                    else if (Sex != null && Medal != null && Games == null && Name == null) cmd.CommandText += " WHERE Sex = @Sex AND Medal = @Medal";
                    else if (Sex == null && Medal != null && Games != null && Name == null)
                    {
                        if (Sport == null && Event == null) cmd.CommandText += " WHERE Games = @Games AND Medal = @Medal";
                        if (Sport != null && Event == null) cmd.CommandText += " WHERE Games = @Games AND Medal = @Medal AND Sport = @Sport";
                        if (Sport != null && Event != null) cmd.CommandText += " WHERE Games = @Games AND Medal = @Medal AND Sport = @Sport AND Event= @Event";
                    }
                    else if (Sex != null && Medal == null && Games != null && Name == null)
                    {
                        if (Sport == null && Event == null) cmd.CommandText += " WHERE Games = @Games AND Sex = @Sex";
                        if (Sport != null && Event == null) cmd.CommandText += " WHERE Games = @Games AND Sex = @Sex AND Sport = @Sport";
                        if (Sport != null && Event != null) cmd.CommandText += " WHERE Games = @Games AND Sex = @Sex AND Sport = @Sport AND Event= @Event";
                    }
                    else if (Sex != null && Medal == null && Games == null && Name != null) cmd.CommandText += " WHERE Name like @Name AND Sex = @Sex";
                    else if (Sex == null && Medal != null && Games == null && Name != null) cmd.CommandText += " WHERE Name like @Name AND Medal = @Medal";
                    else if (Sex == null && Medal == null && Games != null && Name != null)
                    {
                        if (Sport == null && Event == null) cmd.CommandText += " WHERE Name like @Name AND Games = @Games";
                        if (Sport != null && Event == null) cmd.CommandText += " WHERE Name like @Name AND Games = @Games AND Sport = @Sport";
                        if (Sport != null && Event != null) cmd.CommandText += " WHERE Name like @Name AND Games = @Games Sport = @Sport AND Event= @Event";
                    }
                    else if (Sex != null && Medal != null && Games == null && Name != null) cmd.CommandText += " WHERE Name like @Name AND Sex = @Sex AND Medal = @Medal";
                    else if (Sex == null && Medal != null && Games != null && Name != null)
                    {
                        if (Sport == null && Event == null) cmd.CommandText += " WHERE Name like @Name AND Medal = @Medal AND Games = @Games";
                        if (Sport != null && Event == null) cmd.CommandText += " WHERE Name like @Name AND Medal = @Medal AND Games = @Games AND Sport = @Sport";
                        if (Sport != null && Event != null) cmd.CommandText += " WHERE Name like @Name AND Medal = @Medal AND Games = @Games AND Sport = @Sport AND Event= @Event";
                    }

                    else if (Sex != null && Medal == null && Games != null && Name != null)
                    {
                        if (Sport == null && Event == null) cmd.CommandText += " WHERE Name like @Name AND Games = @Games AND Medal = @Medal";
                        if (Sport != null && Event == null) cmd.CommandText += " WHERE Name like @Name AND Games = @Games AND Medal = @Medal AND Sport = @Sport";
                        if (Sport != null && Event != null) cmd.CommandText += " WHERE Name like @Name AND Games = @Games AND Medal = @Medal AND Sport = @Sport AND Event= @Event";
                    }
                    else if (Sex != null && Medal != null && Games != null && Name == null)
                    {
                        if (Sport == null && Event == null) cmd.CommandText += " WHERE Medal= @Medal AND Sex= @Sex AND Games= @Games ";
                        if (Sport != null && Event == null) cmd.CommandText += " WHERE Medal= @Medal AND Sex= @Sex AND Games= @Games AND Sport = @Sport";
                        if (Sport != null && Event != null) cmd.CommandText += " WHERE Medal= @Medal AND Sex= @Sex AND Games= @Games AND Sport = @Sport AND Event= @Event";
                    }
                    else if (Sex != null && Medal != null && Games != null && Name != null)
                    {
                        if (Sport == null && Event == null) cmd.CommandText += " WHERE Medal= @Medal AND Sex= @Sex AND Games= @Games AND Name like @Name ";
                        if (Sport != null && Event == null) cmd.CommandText += " WHERE Medal= @Medal AND Sex= @Sex AND Games= @Games AND Name like @Name AND Sport = @Sport";
                        if (Sport != null && Event != null) cmd.CommandText += " WHERE Medal= @Medal AND Sex= @Sex AND Games= @Games AND Name like @Name AND Sport = @Sport AND Event= @Event";
                    }                    
                    
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = connection; 
                    cmd2.CommandText = cmd.CommandText.Replace("*","Count(*)");
                    cmd2.Parameters.AddWithValue("@Sex", $"{Sex}");
                    cmd2.Parameters.AddWithValue("@Medal", $"{Medal}");
                    cmd2.Parameters.AddWithValue("@Games", $"{Games}");
                    cmd2.Parameters.AddWithValue("@Name", $"%{Name}%");
                    cmd2.Parameters.AddWithValue("@Sport", $"{Sport}");
                    cmd2.Parameters.AddWithValue("@Event", $"{Event}");
                    PaginaRif = (int)cmd2.ExecuteScalar() + 1;                     


                    cmd.Parameters.AddWithValue("@Alfa", (Pagina_Corrente - 1) * Righe_Pag);
                    cmd.Parameters.AddWithValue("@Beta", Righe_Pag);
                    cmd.Parameters.AddWithValue("@Sex", $"{Sex}");
                    cmd.Parameters.AddWithValue("@Medal", $"{Medal}");
                    cmd.Parameters.AddWithValue("@Games", $"{Games}");
                    cmd.Parameters.AddWithValue("@Name", $"%{Name}%");
                    cmd.Parameters.AddWithValue("@Sport", $"{Sport}");
                    cmd.Parameters.AddWithValue("@Event", $"{Event}");



                    cmd.CommandText += " ORDER by Id";
                    cmd.CommandText += " OFFSET @Alfa Rows";
                    cmd.CommandText += " FETCH NEXT @Beta Rows Only";
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lista_Ritorno.Add(ReadToOlympic(reader));
                        }

                    }

                    return Lista_Ritorno;
                }
                catch (Exception)
                {

                    throw;

                }

            }



        }
        public static List<int> GetRighe()
        {
            List<int> Lista_Ritorno = new List<int>();
            Lista_Ritorno.Add(10);
            Lista_Ritorno.Add(20);
            Lista_Ritorno.Add(50);
            return Lista_Ritorno;   
        }
        private static OlympicsModel ReadToOlympic(SqlDataReader reader)
        {

            return new OlympicsModel()
            {
           
                Id = (long)reader["Id"],
                IdAthlete = (long?)(reader.IsDBNull(reader.GetOrdinal("IdAthlete")) ? null : reader["IdAthlete"]),
                Name = (string)reader["Name"],
                Sex = (string)(reader.IsDBNull(reader.GetOrdinal("Sex")) ? null : reader["Sex"]),
                Age = (int?)(reader.IsDBNull(reader.GetOrdinal("Age")) ? null : reader["Age"]),
                Height = (int?)(reader.IsDBNull(reader.GetOrdinal("Height")) ? null : reader["Height"]),
                Weight = (int?)(reader.IsDBNull(reader.GetOrdinal("Weight")) ? null : reader["Weight"]),
                NOC = (string)(reader.IsDBNull(reader.GetOrdinal("NOC")) ? null : reader["NOC"]),
                Games = (string)(reader.IsDBNull(reader.GetOrdinal("Games")) ? null : reader["Games"]),
                Year = (int?)(reader.IsDBNull(reader.GetOrdinal("Year")) ? null : reader["Year"]),
                Season = (string)(reader.IsDBNull(reader.GetOrdinal("Season")) ? null : reader["Season"]),
                City = (string)(reader.IsDBNull(reader.GetOrdinal("City")) ? null : reader["City"]),
                Sport = (string)(reader.IsDBNull(reader.GetOrdinal("Sport")) ? null : reader["Sport"]),
                Event = (string)(reader.IsDBNull(reader.GetOrdinal("Event")) ? null : reader["Event"]),
                Medal = (string)(reader.IsDBNull(reader.GetOrdinal("Medal")) ? null : reader["Medal"]),

        };
           
        }
    }
}
