namespace HOSAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int? GrupId { get; set; }
        public string User_Name { get; set; }
        public string User_Pwd { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string CNP { get; set; }
        public string CI_Seria { get; set; }
        public string CI_Numarul { get; set; }
        public decimal? Data_Activare { get; set; }
        public decimal? Data_Dezactivare { get; set; }
        public bool? Windows_Role { get; set; }
        public bool? Web_Role { get; set; }
        public int? Account_StateId { get; set; }
        public string State_Reason { get; set; }
        public decimal? Data_Blocare { get; set; }
        public int? Durata_Blocare { get; set; }
        public string Email { get; set; }
        public string PaginaStart { get; set; }
    }
}
